/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;

namespace NABarrierLocationEditor
{
	public partial class EditorForm : Form
	{
		#region Member Variables

		private readonly static string EDGE_ALONG = "Along Digitized";
		private readonly static string EDGE_AGAINST = "Against Digitized";

		IApplication m_app;
		INAContext m_context;
		IFeature m_barrier;

		#endregion

		#region Initialization

		public EditorForm(IApplication app, INAContext context, IFeature barrier)
		{
			m_barrier = barrier;
			m_app = app;
			m_context = context;

			InitializeComponent();
			LoadDatagrids();
		}

		/// <summary>
		/// Load both the Edge and Junction dataGrids with the information in the barrier feature
		/// <param name="barrier">The barrier being loaded into dataGrids</param>
		/// </summary>
		void LoadDatagrids()
		{
			// Populate the cell with the direction drop down
			((DataGridViewComboBoxColumn)dataGridViewEdges.Columns[1]).Items.AddRange(EDGE_ALONG, EDGE_AGAINST); ;

			// get the location ranges out of the barrier feature
			var naLocRangesObject = m_barrier as INALocationRangesObject;
			var naLocRanges = naLocRangesObject.NALocationRanges;
			if (naLocRanges == null)
				throw new Exception("Selected barrier has a null NALocationRanges value");

			// add all of the junctions included in the barrier to the Junctions dataGrid
			long junctionCount = naLocRanges.JunctionCount;
			int junctionEID = -1;
			for (int i = 0; i < junctionCount; i++)
			{
				naLocRanges.QueryJunction(i, ref junctionEID);
				int rowIndex = dataGridViewJunctions.Rows.Add();
				dataGridViewJunctions.Rows[rowIndex].SetValues(junctionEID);
			}

			// add all of the edges included in the barrier to the Edges dataGrid
			long edgeRangeCount = naLocRanges.EdgeRangeCount;
			int edgeEID = -1;
			double fromPosition, toPosition;
			fromPosition = toPosition = -1;
			esriNetworkEdgeDirection edgeDirection = esriNetworkEdgeDirection.esriNEDNone;
			for (int i = 0; i < edgeRangeCount; i++)
			{
				naLocRanges.QueryEdgeRange(i, ref edgeEID, ref edgeDirection, ref fromPosition, ref toPosition);

				string directionValue = "";
				if (edgeDirection == esriNetworkEdgeDirection.esriNEDAlongDigitized) directionValue = EDGE_ALONG;
				else if (edgeDirection == esriNetworkEdgeDirection.esriNEDAgainstDigitized) directionValue = EDGE_AGAINST;

				dataGridViewEdges.Rows.Add(edgeEID, directionValue, fromPosition, toPosition);
			}
		}

		#endregion

		#region Button Clicks

		/// <summary>
		/// Occurs when the user clicks the Cancel button.
		/// <param name="sender">The control raising this event</param>
		/// <param name="e">Arguments associated with the event</param>
		/// </summary>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Occurs when the user clicks the Save button.
		///   The barrier information is collected out of the junction and edge barrier
		///   dataGrids, then stored back into the original barrier feature as a replacement
		///   to the existing barrier information.  The original geometry of the barrier remains 
		///   unaltered.
		/// <param name="sender">The control raising this event</param>
		/// <param name="e">Arguments associated with the event</param>
		/// </summary>
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (!ValidateDataGrid(dataGridViewEdges)) return;
			if (!ValidateDataGrid(dataGridViewJunctions)) return;

			// The existing NALocationRanges for the barrier will be replaced with a new one
			INALocationRanges naLocRanges = new NALocationRangesClass();

			// First gather the edge ranges
			foreach (DataGridViewRow row in dataGridViewEdges.Rows)
			{
				// ignore the extra row in the dataGrid
				if (row.IsNewRow) continue;

				// gather the EID value for the new range
				int eid = Int32.Parse(row.Cells[0].Value.ToString());

				// gather the edge direction value for the new range
				string directionValue = row.Cells[1].Value.ToString();
				esriNetworkEdgeDirection direction = esriNetworkEdgeDirection.esriNEDNone;
				if (directionValue == EDGE_ALONG) direction = esriNetworkEdgeDirection.esriNEDAlongDigitized;
				else if (directionValue == EDGE_AGAINST) direction = esriNetworkEdgeDirection.esriNEDAgainstDigitized;

				// gather the from and to position values for the new range
				double fromPos = Double.Parse(row.Cells[2].Value.ToString());
				double toPos = Double.Parse(row.Cells[3].Value.ToString());

				// load the values for this range into the NALocationRanges object
				naLocRanges.AddEdgeRange(eid, direction, fromPos, toPos);
			}

			// Now gather the junctions to be included in the barrier
			foreach (DataGridViewRow row in dataGridViewJunctions.Rows)
			{
				// ignore the extra row in the dataGrid
				if (row.IsNewRow) continue;

				// gather the EID value for the junction to include
				int eid = Int32.Parse(row.Cells[0].Value.ToString());

				// load this junction into the NALocationRanges object
				naLocRanges.AddJunction(eid);
			}

			// Cast the barrier feature to INALocationRanges Object, then populate
			//   its NALocationRanges value with the new barrier that was created above.
			//   Then, save the new barrier with a call to Store()
			INALocationRangesObject naLocationRangesObject = m_barrier as INALocationRangesObject;
			naLocationRangesObject.NALocationRanges = naLocRanges;
			m_barrier.Store();

			this.Close();
		}

		/// <summary>
		/// Occurs when the user clicks the Zoom To Barrier Geometry button.
		///   The map will zoom to the extent of the Shape of the barrier.
		/// <param name="sender">The control raising this event</param>
		/// <param name="e">Arguments associated with the event</param>
		/// </summary>
		private void btnZoomToBarrier_Click(object sender, EventArgs e)
		{
			IMxDocument mxDoc = m_app.Document as IMxDocument;
			mxDoc.ActiveView.Extent = m_barrier.Extent;
			mxDoc.ActiveView.Refresh();
		}

		#endregion

		#region Validation

		/// <summary>
		/// ValidateDataGrid goes row by row and checks that the values in the grid
		///   are valid
		/// <param name="dgv">The dataGrid to validate</param>
		/// </summary>
		private bool ValidateDataGrid(DataGridView dgv)
		{
			// we do all of our validation when the save button is clicked
			foreach (DataGridViewRow row in dgv.Rows)
			{
				ValidateRow(row);
				if (row.ErrorText != "")
				{
					dgv.FirstDisplayedScrollingRowIndex = row.Index;
					System.Windows.Forms.MessageBox.Show("You cannot save until all row errors are cleared.", "Barrier Location Editor Warning");
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// ValidateRow checks the cell value in the passed-in row
		/// <param name="row">The row to validate</param>
		/// <param name="columns">The fields to be validated</param>
		/// <param name="datagridviewName">The name of the dataGrid whose row is being validated</param>
		/// </summary>
		void ValidateRow(DataGridViewRow row)
		{
			// the extra row to add values does not need to be validated
			if (row.IsNewRow) return;

			row.ErrorText = "";

			// validate each column
			foreach (DataGridViewColumn column in row.DataGridView.Columns)
			{
				// none of the column values can be empty
				if (row.Cells[column.Name].Value == null || row.Cells[column.Name].Value.ToString() == "")
				{
					row.ErrorText = "There cannot be any empty cells";
					return;
				}

				string value = row.Cells[column.Name].Value.ToString();

				switch (column.Name)
				{
					case "JunctionEID":
						if (!ValidateEID(value, esriNetworkElementType.esriNETJunction))
							row.ErrorText += "  Junction EID must correspond to a valid network junction";
						break;
					case "EdgeEID":
						if (!ValidateEID(value, esriNetworkElementType.esriNETEdge))
							row.ErrorText += "  Edge EID must correspond to a valid network edge";
						break;
					case "Direction":
						if (value != "Along Digitized" && value != "Against Digitized")
							row.ErrorText += "  Direction must be Along or Against Digitized";
						break;
					case "fromPos":
						double fromPos = -1;
						if (!Double.TryParse(value, out fromPos) || fromPos < 0 || fromPos > 1)
							row.ErrorText += "  FromPosition must be a positive number between zero and one";
						break;
					case "toPos":
						double toPos = -1;
						if (!Double.TryParse(value, out toPos) || toPos < 0 || toPos > 1)
							row.ErrorText += "  ToPosition must be a positive number between zero and one";
						break;
					default:
						throw new Exception("Unexpected Column");
				}
			}

			// Now, validate that the from position is always less than the two position
			// FromPosition and ToPosition only matter for edge barriers
			if (row.DataGridView.Name == "dataGridViewEdges")
			{
				if (row.Cells["FromPos"].Value == null || row.Cells["ToPos"].Value == null)
				{
					row.ErrorText += "  FromPosition and ToPosition must have valid decimal values between 0 and 1";
					return;
				}

				double fromPos = -1;
				Double.TryParse(row.Cells["FromPos"].Value.ToString(), out fromPos);

				double toPos = -1;
				Double.TryParse(row.Cells["ToPos"].Value.ToString(), out toPos);

				if (fromPos > toPos)
				{
					row.ErrorText += "  FromPosition must be equal to or less than the ToPosition";
					return;
				}
			}
		}

		/// <summary>
		/// Verify that the EID corresponds to a valid network element
		/// <param name="value">The EID passed as a string</param>
		/// <param name="elementType">The type of element to be verified</param>
		/// </summary>
		bool ValidateEID(string value, esriNetworkElementType elementType)
		{
			// validate that the EID is a valid integer
			int eid = -1;
			if (!Int32.TryParse(value, out eid) || eid < 1)
				return false;

			// QueryEdge and QueryJunction will throw exceptions if the EID doesn't match any elements
			var netQuery = m_context.NetworkDataset as INetworkQuery;
			try
			{
				switch (elementType)
				{
					case esriNetworkElementType.esriNETJunction:
						var junction = netQuery.CreateNetworkElement(esriNetworkElementType.esriNETJunction) as INetworkJunction;
						netQuery.QueryJunction(eid, junction);
						break;
					case esriNetworkElementType.esriNETEdge:
						var edge = netQuery.CreateNetworkElement(esriNetworkElementType.esriNETEdge) as INetworkEdge;
						netQuery.QueryEdge(eid, esriNetworkEdgeDirection.esriNEDAlongDigitized, edge);
						break;
					default:
						return false;
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		#endregion

		#region Flash the Geometry

		/// <summary>
		/// Occurs when a dataGrid row header is clicked.
		///   It is used here to flash the geometry of the newly selected row
		/// <param name="sender">The control raising this event</param>
		/// <param name="e">Arguments associated with the event</param>
		/// </summary>
		private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			// make sure none of the cells are in edit mode.  When in edit mode, the values obtained
			//  programmatically will not yet match the value the user has changed the cell to
			DataGridView dgv = (DataGridView)sender;
			dgv.EndEdit();

			// Only flash when there is one selected row
			if (dgv.SelectedRows.Count > 1) return;
			DataGridViewRow selectedRow = dgv.SelectedRows[0];

			// If it is the extra dataGrid row or has errors, then don't try to flash it 
			ValidateRow(selectedRow);
			if (selectedRow.IsNewRow || selectedRow.ErrorText != "") return;

			// also, if any of the row's cell have no value, then don't want to flash it
			foreach (DataGridViewCell cell in selectedRow.Cells)
				if (cell.Value == null) return;

			// use the EID to obtain the barrier's corresponding network element and source feature
			INetworkElement element = GetElementByEID(selectedRow.Cells[0].Value.ToString(), dgv.Name);
			if (element == null) return;
			IFeature sourceFeature = GetSourceFeature(element);

			// For an edge, get the part geometry of the barrier covered portion of the source feature
			//  that should be displayed
			INetworkEdge netEdge = element as INetworkEdge;
			esriNetworkEdgeDirection displayDirection = esriNetworkEdgeDirection.esriNEDNone;
			if (netEdge != null)
			{
				sourceFeature.Shape = GetBarrierSubcurve(netEdge, sourceFeature, selectedRow);
				displayDirection = GetDirectionValue(selectedRow);
			}

			// Draw
			FlashFeature(sourceFeature, displayDirection);
		}

		/// <summary>
		/// Determine the esriNetworkEdgeDirection from the value in the dataGridView cell
		/// <param name="selectedRow">The row containing the barrier's location range information</param>
		/// </summary>
		private esriNetworkEdgeDirection GetDirectionValue(DataGridViewRow selectedRow)
		{
			esriNetworkEdgeDirection direction = esriNetworkEdgeDirection.esriNEDNone;
			string textValue = selectedRow.Cells[1].Value.ToString();
			if (textValue == EDGE_ALONG) direction = esriNetworkEdgeDirection.esriNEDAlongDigitized;
			else if (textValue == EDGE_AGAINST) direction = esriNetworkEdgeDirection.esriNEDAgainstDigitized;

			return direction;
		}

		/// <summary>
		/// Take a network edge, a source feature, and a row from the edges dataGrid, and determine
		///  the geometry to be flashed on the map
		/// <param name="netEdge">The edge upon which the barrier resides</param>
		/// <param name="sourceFeature">The source feature corresponding to the network edge</param>
		/// <param name="selectedRow">The row containing the barrier's location range information</param>
		/// </summary>
		private ICurve GetBarrierSubcurve(INetworkEdge netEdge, IFeature sourceFeature, DataGridViewRow selectedRow)
		{
			// value for displaying the entire source feature
			double fromPosition = 0;
			double toPosition = 1;

			// Find the values for displaying only the element portion of the source feature
			double fromElementPosition, toElementPosition;
			netEdge.QueryPositions(out fromElementPosition, out toElementPosition);

			// due to the element possibly being in the against digitized direction,
			//   fromPosition could be greater than toPosition.  If that is the case, swap the values
			if (fromElementPosition > toElementPosition)
			{
				double tmp = fromElementPosition;
				fromElementPosition = toElementPosition;
				toElementPosition = tmp;
			}

			esriNetworkEdgeDirection direction = GetDirectionValue(selectedRow);
			if (direction == esriNetworkEdgeDirection.esriNEDNone) return null;

			// Flash the edge
			if (rbFlashElementPortion.Checked)
			{
				fromPosition = fromElementPosition;
				toPosition = toElementPosition;
			}
			// Flash the barrier portion of the edge
			else if (rbFlashBarrierPortion.Checked)
			{
				double fromBarrierPosition = -1;
				double toBarrierPosition = -1;

				// gather the from and to position values for the barrier
				fromBarrierPosition = Double.Parse(selectedRow.Cells[2].Value.ToString());
				toBarrierPosition = Double.Parse(selectedRow.Cells[3].Value.ToString());

				// for barriers in the against direction, we need to adjust that the element position is
				if (direction == esriNetworkEdgeDirection.esriNEDAgainstDigitized)
				{
					fromBarrierPosition = 1 - fromBarrierPosition;
					toBarrierPosition = 1 - toBarrierPosition;
				}

				// use the positioning along the element of the barrier
				//  to get the position along the original source feature
				fromPosition = fromElementPosition + (fromBarrierPosition * (toElementPosition - fromElementPosition));
				toPosition = fromElementPosition + (toBarrierPosition * (toElementPosition - fromElementPosition));
			}

			if (fromPosition > toPosition)
			{
				double tmp = fromPosition;
				fromPosition = toPosition;
				toPosition = tmp;
			}

			// get the subspan on the polyline that represents the from and to positions we specified
			ICurve displayCurve;
			ICurve sourceCurve = sourceFeature.Shape as ICurve;
			sourceCurve.GetSubcurve(fromPosition, toPosition, true, out displayCurve);
			return displayCurve;
		}

		/// <summary>
		/// Take an EID value as a string from one of the dataGridView controls and find
		///  the network element that corresponds to the EID
		/// <param name="eidString">The EID value as a string</param>
		/// <param name="datagridviewName">The name of the dataGrid that held the EID</param>
		/// </summary>
		private INetworkElement GetElementByEID(string eidString, string datagridviewName)
		{
			int eid = -1;
			if (!Int32.TryParse(eidString, out eid)) return null;

			INetworkQuery netQuery = m_context.NetworkDataset as INetworkQuery;
			INetworkEdge edge = netQuery.CreateNetworkElement(esriNetworkElementType.esriNETEdge) as INetworkEdge;
			INetworkJunction junction = netQuery.CreateNetworkElement(esriNetworkElementType.esriNETJunction) as INetworkJunction;

			INetworkElement element = null;
			try
			{
				// Populate the network element from the EID
				if (datagridviewName == "dataGridViewEdges")
				{
					netQuery.QueryEdge(eid, esriNetworkEdgeDirection.esriNEDAlongDigitized, edge);
					element = edge as INetworkElement;
				}
				else if (datagridviewName == "dataGridViewJunctions")
				{
					netQuery.QueryJunction(eid, junction);
					element = junction as INetworkElement;
				}
			}
			catch
			{
				// if the query fails, the element will not be displayed
			}

			return element;
		}

		/// <summary>
		/// Take a network element and return its corresponding source feature
		/// <param name="element">The return source feature corresponds to this element</param>
		/// </summary>
		private IFeature GetSourceFeature(INetworkElement element)
		{
			// To draw the network element, we will need its corresponding source feature information
			// Get the sourceID and OID from the element
			int sourceID = element.SourceID;
			int sourceOID = element.OID;

			// Get the source feature from the network source
			INetworkSource netSource = m_context.NetworkDataset.get_SourceByID(sourceID);
			IFeatureClassContainer fClassContainer = m_context.NetworkDataset as IFeatureClassContainer;
			IFeatureClass sourceFClass = fClassContainer.get_ClassByName(netSource.Name);
			return sourceFClass.GetFeature(sourceOID);
		}

		/// <summary>
		/// Flash the feature geometry on the map
		/// <param name="pFeature">The feature being flashed</param>
		/// <param name="pMxDoc">A hook to the application display</param>
		/// <param name="direction">The digitized direction of the barrier with respect to the underlying source feature</param>
		/// </summary>
		private void FlashFeature(IFeature pFeature, esriNetworkEdgeDirection direction)
		{
			IMxDocument pMxDoc = m_app.Document as IMxDocument;

			// Start drawing on screen. 
			pMxDoc.ActiveView.ScreenDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);

			// Switch functions based on Geometry type. 
			switch (pFeature.Shape.GeometryType)
			{
				case esriGeometryType.esriGeometryPolyline:
					FlashLine(pMxDoc.ActiveView.ScreenDisplay, pFeature.Shape, direction);
					break;
				case esriGeometryType.esriGeometryPolygon:
					// no network elements can be polygons
					break;
				case esriGeometryType.esriGeometryPoint:
					FlashPoint(pMxDoc.ActiveView.ScreenDisplay, pFeature.Shape);
					break;
				default:
					throw new Exception("Unexpected Geometry Type");
			}

			// Finish drawing on screen. 
			pMxDoc.ActiveView.ScreenDisplay.FinishDrawing();
		}

		/// <summary>
		/// Flash a line feature on the map
		/// <param name="pDisplay">The map screen</param>
		/// <param name="pGeometry">The geometry of the feature to be flashed</param>
		/// <param name="direction">The digitized direction of the barrier with respect to the underlying source feature</param>
		/// </summary>
		private void FlashLine(IScreenDisplay pDisplay, IGeometry pGeometry, esriNetworkEdgeDirection direction)
		{
			// The flash will be on a line symbol with an arrow on it
			ICartographicLineSymbol ipArrowLineSymbol = new CartographicLineSymbolClass();

			// the line color will be red
			IRgbColor ipRgbRedColor = new RgbColorClass();
			ipRgbRedColor.Red = 192;

			// the arrow will be black
			IRgbColor ipRgbBlackColor = new RgbColorClass();
			ipRgbBlackColor.RGB = 0;

			// set up the arrow that will be displayed along the line
			IArrowMarkerSymbol ipArrowMarker = new ArrowMarkerSymbolClass();
			ipArrowMarker.Style = esriArrowMarkerStyle.esriAMSPlain;
			ipArrowMarker.Length = 18;
			ipArrowMarker.Width = 12;
			ipArrowMarker.Color = ipRgbBlackColor;

			// set up the line itself
			ipArrowLineSymbol.Width = 4;
			ipArrowLineSymbol.Color = ipRgbRedColor;

			// Set up the Raster Op-Code to help the flash mechanism
			((ISymbol)ipArrowMarker).ROP2 = esriRasterOpCode.esriROPNotXOrPen;
			((ISymbol)ipArrowLineSymbol).ROP2 = esriRasterOpCode.esriROPNotXOrPen;

			// decorate the line with the arrow symbol
			ISimpleLineDecorationElement ipSimpleLineDecorationElement = new SimpleLineDecorationElementClass();
			ipSimpleLineDecorationElement.Rotate = true;
			ipSimpleLineDecorationElement.PositionAsRatio = true;
			ipSimpleLineDecorationElement.MarkerSymbol = ipArrowMarker;
			ipSimpleLineDecorationElement.AddPosition(0.5);
			ILineDecoration ipLineDecoration = new LineDecorationClass();
			ipLineDecoration.AddElement(ipSimpleLineDecorationElement);
			((ILineProperties)ipArrowLineSymbol).LineDecoration = ipLineDecoration;

			// the arrow is initially set to correspond to the digitized direction of the line
			//  if the barrier direction is against digitized, then we need to flip the arrow direction
			if (direction == esriNetworkEdgeDirection.esriNEDAgainstDigitized)
				ipSimpleLineDecorationElement.FlipAll = true;

			// Flash the line
			//  Two calls are made to Draw.  Since the ROP2 setting is NotXOrPen, the first call
			//  draws the symbol with our new symbology and the second call redraws what was originally 
			//  in the place of the symbol
			pDisplay.SetSymbol(ipArrowLineSymbol as ISymbol);
			pDisplay.DrawPolyline(pGeometry);
			System.Threading.Thread.Sleep(300);
			pDisplay.DrawPolyline(pGeometry);
		}

		/// <summary>
		/// Flash a point feature on the map
		/// <param name="pDisplay">The map screen</param>
		/// <param name="pGeometry">The geometry of the feature to be flashed</param>
		/// </summary>
		private void FlashPoint(IScreenDisplay pDisplay, IGeometry pGeometry)
		{
			// for a point, we only flash a simple circle
			ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
			pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

			// Set up the Raster Op-Code to help the flash mechanism
			ISymbol pSymbol = pMarkerSymbol as ISymbol;
			pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

			// Flash the point
			//  Two calls are made to Draw.  Since the ROP2 setting is NotXOrPen, the first call
			//  draws the symbol with our new symbology and the second call redraws what was originally 
			//  in the place of the symbol
			pDisplay.SetSymbol(pSymbol);
			pDisplay.DrawPoint(pGeometry);
			System.Threading.Thread.Sleep(300);
			pDisplay.DrawPoint(pGeometry);
		}

		#endregion
	}
}
