Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.NetworkAnalyst

Namespace NABarrierLocationEditor
	Partial Public Class EditorForm
		Inherits Form
#Region "Member Variables"

		Private Shared ReadOnly EDGE_ALONG As String = "Along Digitized"
		Private Shared ReadOnly EDGE_AGAINST As String = "Against Digitized"

		Private m_app As IApplication
		Private m_context As INAContext
		Private m_barrier As IFeature

#End Region

#Region "Initialization"

		Public Sub New(ByVal app As IApplication, ByVal context As INAContext, ByVal barrier As IFeature)
			m_barrier = barrier
			m_app = app
			m_context = context

			InitializeComponent()
			LoadDatagrids()
		End Sub

		''' <summary>
		''' Load both the Edge and Junction dataGrids with the information in the barrier feature
		''' <param name="barrier">The barrier being loaded into dataGrids</param>
		''' </summary>
		Private Sub LoadDatagrids()
			' Populate the cell with the direction drop down
			CType(dataGridViewEdges.Columns(1), DataGridViewComboBoxColumn).Items.AddRange(EDGE_ALONG, EDGE_AGAINST)


			' get the location ranges out of the barrier feature
			Dim naLocRangesObject As INALocationRangesObject = TryCast(m_barrier, INALocationRangesObject)
			Dim naLocRanges As INALocationRanges = naLocRangesObject.NALocationRanges
			If (naLocRanges Is Nothing) Then
				Throw New Exception("Selected barrier has a null NALocationRanges value")
			End If

			' add all of the junctions included in the barrier to the Junctions dataGrid
			Dim junctionCount As Integer = naLocRanges.JunctionCount
			Dim junctionEID As Integer
			For i As Integer = 0 To junctionCount - 1
				naLocRanges.QueryJunction(i, junctionEID)
				Dim rowIndex As Integer = dataGridViewJunctions.Rows.Add()
				dataGridViewJunctions.Rows(rowIndex).SetValues(junctionEID)
			Next i

			' add all of the edges included in the barrier to the Edges dataGrid
			Dim edgeRangeCount As Integer = naLocRanges.EdgeRangeCount
			Dim edgeEID As Integer
			Dim fromPosition, toPosition As Double
			Dim edgeDirection As esriNetworkEdgeDirection
			For i As Integer = 0 To edgeRangeCount - 1
				naLocRanges.QueryEdgeRange(i, edgeEID, edgeDirection, fromPosition, toPosition)

				Dim directionValue As String = ""
				If edgeDirection = esriNetworkEdgeDirection.esriNEDAlongDigitized Then
					directionValue = EDGE_ALONG
				ElseIf edgeDirection = esriNetworkEdgeDirection.esriNEDAgainstDigitized Then
					directionValue = EDGE_AGAINST
				End If

				dataGridViewEdges.Rows.Add(edgeEID, directionValue, fromPosition, toPosition)
			Next i
		End Sub

#End Region

#Region "Button Clicks"

		''' <summary>
		''' Occurs when the user clicks the Cancel button.
		''' <param name="sender">The control raising this event</param>
		''' <param name="e">Arguments associated with the event</param>
		''' </summary>
		Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
			Me.Close()
		End Sub

		''' <summary>
		''' Occurs when the user clicks the Save button.
		'''   The barrier information is collected out of the junction and edge barrier
		'''   dataGrids, then stored back into the original barrier feature as a replacement
		'''   to the existing barrier information.  The original geometry of the barrier remains 
		'''   unaltered.
		''' <param name="sender">The control raising this event</param>
		''' <param name="e">Arguments associated with the event</param>
		''' </summary>
		Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
			If (Not ValidateDataGrid(dataGridViewEdges)) Then
				Return
			End If
			If (Not ValidateDataGrid(dataGridViewJunctions)) Then
				Return
			End If

			' The existing NALocationRanges for the barrier will be replaced with a new one
			Dim naLocRanges As INALocationRanges = New NALocationRangesClass()

			' First gather the edge ranges
			For Each row As DataGridViewRow In dataGridViewEdges.Rows
				' ignore the extra row in the dataGrid
				If row.IsNewRow Then
					Continue For
				End If

				' gather the EID value for the new range
				Dim eid As Integer = Int32.Parse(row.Cells(0).Value.ToString())

				' gather the edge direction value for the new range
				Dim directionValue As String = row.Cells(1).Value.ToString()
				Dim direction As esriNetworkEdgeDirection = esriNetworkEdgeDirection.esriNEDNone
				If directionValue = EDGE_ALONG Then
					direction = esriNetworkEdgeDirection.esriNEDAlongDigitized
				ElseIf directionValue = EDGE_AGAINST Then
					direction = esriNetworkEdgeDirection.esriNEDAgainstDigitized
				End If

				' gather the from and to position values for the new range
				Dim fromPos As Double = Double.Parse(row.Cells(2).Value.ToString())
				Dim toPos As Double = Double.Parse(row.Cells(3).Value.ToString())

				' load the values for this range into the NALocationRanges object
				naLocRanges.AddEdgeRange(eid, direction, fromPos, toPos)
			Next row

			' Now gather the junctions to be included in the barrier
			For Each row As DataGridViewRow In dataGridViewJunctions.Rows
				' ignore the extra row in the dataGrid
				If row.IsNewRow Then
					Continue For
				End If

				' gather the EID value for the junction to include
				Dim eid As Integer = Int32.Parse(row.Cells(0).Value.ToString())

				' load this junction into the NALocationRanges object
				naLocRanges.AddJunction(eid)
			Next row

			' Cast the barrier feature to INALocationRanges Object, then populate
			'   its NALocationRanges value with the new barrier that was created above.
			'   Then, save the new barrier with a call to Store()
			Dim naLocationRangesObject As INALocationRangesObject = TryCast(m_barrier, INALocationRangesObject)
			naLocationRangesObject.NALocationRanges = naLocRanges
			m_barrier.Store()

			Me.Close()
		End Sub

		''' <summary>
		''' Occurs when the user clicks the Zoom To Barrier Geometry button.
		'''   The map will zoom to the extent of the Shape of the barrier.
		''' <param name="sender">The control raising this event</param>
		''' <param name="e">Arguments associated with the event</param>
		''' </summary>
		Private Sub btnZoomToBarrier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZoomToBarrier.Click
			Dim mxDoc As IMxDocument = TryCast(m_app.Document, IMxDocument)
			mxDoc.ActiveView.Extent = m_barrier.Extent
			mxDoc.ActiveView.Refresh()
		End Sub

#End Region

#Region "Validation"

		''' <summary>
		''' ValidateDataGrid goes row by row and checks that the values in the grid
		'''   are valid
		''' <param name="dgv">The dataGrid to validate</param>
		''' </summary>
		Private Function ValidateDataGrid(ByVal dgv As DataGridView) As Boolean
			' we do all of our validation when the save button is clicked
			For Each row As DataGridViewRow In dgv.Rows
				ValidateRow(row)
				If row.ErrorText <> "" Then
					dgv.FirstDisplayedScrollingRowIndex = row.Index
					System.Windows.Forms.MessageBox.Show("You cannot save until all row errors are cleared.", "Barrier Location Editor Warning")
					Return False
				End If
			Next row

			Return True
		End Function

		''' <summary>
		''' ValidateRow checks the cell value in the passed-in row
		''' <param name="row">The row to validate</param>
		''' <param name="columns">The fields to be validated</param>
		''' <param name="datagridviewName">The name of the dataGrid whose row is being validated</param>
		''' </summary>
		Private Sub ValidateRow(ByVal row As DataGridViewRow)
			' the extra row to add values does not need to be validated
			If row.IsNewRow Then
				Return
			End If

			row.ErrorText = ""

			' validate each column
			For Each column As DataGridViewColumn In row.DataGridView.Columns
				' none of the column values can be empty
				If row.Cells(column.Name).Value Is Nothing OrElse row.Cells(column.Name).Value.ToString() = "" Then
					row.ErrorText = "There cannot be any empty cells"
					Return
				End If

				Dim value As String = row.Cells(column.Name).Value.ToString()

				Select Case column.Name
					Case "JunctionEID"
						If (Not ValidateEID(value, esriNetworkElementType.esriNETJunction)) Then
							row.ErrorText &= "  Junction EID must correspond to a valid network junction"
						End If
					Case "EdgeEID"
						If (Not ValidateEID(value, esriNetworkElementType.esriNETEdge)) Then
							row.ErrorText &= "  Edge EID must correspond to a valid network edge"
						End If
					Case "Direction"
						If value <> "Along Digitized" AndAlso value <> "Against Digitized" Then
							row.ErrorText &= "  Direction must be Along or Against Digitized"
						End If
					Case "fromPos"
						Dim fromPos As Double = -1
						If (Not Double.TryParse(value, fromPos)) OrElse fromPos < 0 OrElse fromPos > 1 Then
							row.ErrorText &= "  FromPosition must be a positive number between zero and one"
						End If
					Case "toPos"
						Dim toPos As Double = -1
						If (Not Double.TryParse(value, toPos)) OrElse toPos < 0 OrElse toPos > 1 Then
							row.ErrorText &= "  ToPosition must be a positive number between zero and one"
						End If
					Case Else
						Throw New Exception("Unexpected Column")
				End Select
			Next column

			' Now, validate that the from position is always less than the two position
			' FromPosition and ToPosition only matter for edge barriers
			If row.DataGridView.Name = "dataGridViewEdges" Then
				If row.Cells("FromPos").Value Is Nothing OrElse row.Cells("ToPos").Value Is Nothing Then
					row.ErrorText &= "  FromPosition and ToPosition must have valid decimal values between 0 and 1"
					Return
				End If

				Dim fromPos As Double = -1
				Double.TryParse(row.Cells("FromPos").Value.ToString(), fromPos)

				Dim toPos As Double = -1
				Double.TryParse(row.Cells("ToPos").Value.ToString(), toPos)

				If fromPos > toPos Then
					row.ErrorText &= "  FromPosition must be equal to or less than the ToPosition"
					Return
				End If
			End If
		End Sub

		''' <summary>
		''' Verify that the EID corresponds to a valid network element
		''' <param name="value">The EID passed as a string</param>
		''' <param name="elementType">The type of element to be verified</param>
		''' </summary>
		Private Function ValidateEID(ByVal value As String, ByVal elementType As esriNetworkElementType) As Boolean
			' validate that the EID is a valid integer
			Dim eid As Integer = -1
			If (Not Int32.TryParse(value, eid)) OrElse eid < 1 Then
				Return False
			End If

			' QueryEdge and QueryJunction will throw exceptions if the EID doesn't match any elements
			Dim netQuery As INetworkQuery = TryCast(m_context.NetworkDataset, INetworkQuery)
			Try
				Select Case elementType
					Case esriNetworkElementType.esriNETJunction
						Dim junction As INetworkJunction = TryCast(netQuery.CreateNetworkElement(esriNetworkElementType.esriNETJunction), INetworkJunction)
						netQuery.QueryJunction(eid, junction)
					Case esriNetworkElementType.esriNETEdge
						Dim edge As INetworkEdge = TryCast(netQuery.CreateNetworkElement(esriNetworkElementType.esriNETEdge), INetworkEdge)
						netQuery.QueryEdge(eid, esriNetworkEdgeDirection.esriNEDAlongDigitized, edge)
					Case Else
						Return False
				End Select
			Catch
				Return False
			End Try

			Return True
		End Function

#End Region

#Region "Flash the Geometry"

		''' <summary>
		''' Occurs when a dataGrid row header is clicked.
		'''   It is used here to flash the geometry of the newly selected row
		''' <param name="sender">The control raising this event</param>
		''' <param name="e">Arguments associated with the event</param>
		''' </summary>
		Private Sub dataGridView_RowHeaderMouseClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles dataGridViewJunctions.RowHeaderMouseClick, dataGridViewEdges.RowHeaderMouseClick
			' make sure none of the cells are in edit mode.  When in edit mode, the values obtained
			'  programmatically will not yet match the value the user has changed the cell to
			Dim dgv As DataGridView = CType(sender, DataGridView)
			dgv.EndEdit()

			' Only flash when there is one selected row
			If dgv.SelectedRows.Count > 1 Then
				Return
			End If
			Dim selectedRow As DataGridViewRow = dgv.SelectedRows(0)

			' If it is the extra dataGrid row or has errors, then don't try to flash it 
			ValidateRow(selectedRow)
			If selectedRow.IsNewRow OrElse selectedRow.ErrorText <> "" Then
				Return
			End If

			' also, if any of the row's cell have no value, then don't want to flash it
			For Each cell As DataGridViewCell In selectedRow.Cells
				If cell.Value Is Nothing Then
					Return
				End If
			Next cell

			' use the EID to obtain the barrier's corresponding network element and source feature
			Dim element As INetworkElement = GetElementByEID(selectedRow.Cells(0).Value.ToString(), dgv.Name)
			If element Is Nothing Then
				Return
			End If
			Dim sourceFeature As IFeature = GetSourceFeature(element)

			' For an edge, get the part geometry of the barrier covered portion of the source feature
			'  that should be displayed
			Dim netEdge As INetworkEdge = TryCast(element, INetworkEdge)
			Dim displayDirection As esriNetworkEdgeDirection = esriNetworkEdgeDirection.esriNEDNone
			If Not netEdge Is Nothing Then
				sourceFeature.Shape = GetBarrierSubcurve(netEdge, sourceFeature, selectedRow)
				displayDirection = GetDirectionValue(selectedRow)
			End If

			' Draw
			FlashFeature(sourceFeature, displayDirection)
		End Sub

		''' <summary>
		''' Determine the esriNetworkEdgeDirection from the value in the dataGridView cell
		''' <param name="selectedRow">The row containing the barrier's location range information</param>
		''' </summary>
		Private Function GetDirectionValue(ByVal selectedRow As DataGridViewRow) As esriNetworkEdgeDirection
			Dim direction As esriNetworkEdgeDirection = esriNetworkEdgeDirection.esriNEDNone
			Dim textValue As String = selectedRow.Cells(1).Value.ToString()
			If textValue = EDGE_ALONG Then
				direction = esriNetworkEdgeDirection.esriNEDAlongDigitized
			ElseIf textValue = EDGE_AGAINST Then
				direction = esriNetworkEdgeDirection.esriNEDAgainstDigitized
			End If

			Return direction
		End Function

		''' <summary>
		''' Take a network edge, a source feature, and a row from the edges dataGrid, and determine
		'''  the geometry to be flashed on the map
		''' <param name="netEdge">The edge upon which the barrier resides</param>
		''' <param name="sourceFeature">The source feature corresponding to the network edge</param>
		''' <param name="selectedRow">The row containing the barrier's location range information</param>
		''' </summary>
		Private Function GetBarrierSubcurve(ByVal netEdge As INetworkEdge, ByVal sourceFeature As IFeature, ByVal selectedRow As DataGridViewRow) As ICurve
			' value for displaying the entire source feature
			Dim fromPosition As Double = 0
			Dim toPosition As Double = 1

			' Find the values for displaying only the element portion of the source feature
			Dim fromElementPosition, toElementPosition As Double
			netEdge.QueryPositions(fromElementPosition, toElementPosition)

			' due to the element possibly being in the against digitized direction,
			'   fromPosition could be greater than toPosition.  If that is the case, swap the values
			If fromElementPosition > toElementPosition Then
				Dim tmp As Double = fromElementPosition
				fromElementPosition = toElementPosition
				toElementPosition = tmp
			End If

			Dim direction As esriNetworkEdgeDirection = GetDirectionValue(selectedRow)
			If direction = esriNetworkEdgeDirection.esriNEDNone Then
				Return Nothing
			End If

			' Flash the edge
			If rbFlashElementPortion.Checked Then
				fromPosition = fromElementPosition
				toPosition = toElementPosition
				' Flash the barrier portion of the edge
			ElseIf rbFlashBarrierPortion.Checked Then
				Dim fromBarrierPosition As Double = -1
				Dim toBarrierPosition As Double = -1

				' gather the from and to position values for the barrier
				fromBarrierPosition = Double.Parse(selectedRow.Cells(2).Value.ToString())
				toBarrierPosition = Double.Parse(selectedRow.Cells(3).Value.ToString())

				' for barriers in the against direction, we need to adjust that the element position is
				If direction = esriNetworkEdgeDirection.esriNEDAgainstDigitized Then
					fromBarrierPosition = 1 - fromBarrierPosition
					toBarrierPosition = 1 - toBarrierPosition
				End If

				' use the positioning along the element of the barrier
				'  to get the position along the original source feature
				fromPosition = fromElementPosition + (fromBarrierPosition * (toElementPosition - fromElementPosition))
				toPosition = fromElementPosition + (toBarrierPosition * (toElementPosition - fromElementPosition))
			End If

			If fromPosition > toPosition Then
				Dim tmp As Double = fromPosition
				fromPosition = toPosition
				toPosition = tmp
			End If

			' get the subspan on the polyline that represents the from and to positions we specified
			Dim displayCurve As ICurve = Nothing
			Dim sourceCurve As ICurve = TryCast(sourceFeature.Shape, ICurve)
			sourceCurve.GetSubcurve(fromPosition, toPosition, True, displayCurve)
			Return displayCurve
		End Function

		''' <summary>
		''' Take an EID value as a string from one of the dataGridView controls and find
		'''  the network element that corresponds to the EID
		''' <param name="eidString">The EID value as a string</param>
		''' <param name="datagridviewName">The name of the dataGrid that held the EID</param>
		''' </summary>
		Private Function GetElementByEID(ByVal eidString As String, ByVal datagridviewName As String) As INetworkElement
			Dim eid As Integer = -1
			If (Not Int32.TryParse(eidString, eid)) Then
				Return Nothing
			End If

			Dim netQuery As INetworkQuery = TryCast(m_context.NetworkDataset, INetworkQuery)
			Dim edge As INetworkEdge = TryCast(netQuery.CreateNetworkElement(esriNetworkElementType.esriNETEdge), INetworkEdge)
			Dim junction As INetworkJunction = TryCast(netQuery.CreateNetworkElement(esriNetworkElementType.esriNETJunction), INetworkJunction)

			Dim element As INetworkElement = Nothing
			Try
				' Populate the network element from the EID
				If datagridviewName = "dataGridViewEdges" Then
					netQuery.QueryEdge(eid, esriNetworkEdgeDirection.esriNEDAlongDigitized, edge)
					element = TryCast(edge, INetworkElement)
				ElseIf datagridviewName = "dataGridViewJunctions" Then
					netQuery.QueryJunction(eid, junction)
					element = TryCast(junction, INetworkElement)
				End If
			Catch
				' if the query fails, the element will not be displayed
			End Try

			Return element
		End Function

		''' <summary>
		''' Take a network element and return its corresponding source feature
		''' <param name="element">The return source feature corresponds to this element</param>
		''' </summary>
		Private Function GetSourceFeature(ByVal element As INetworkElement) As IFeature
			' To draw the network element, we will need its corresponding source feature information
			' Get the sourceID and OID from the element
			Dim sourceID As Integer = element.SourceID
			Dim sourceOID As Integer = element.OID

			' Get the source feature from the network source
			Dim netSource As INetworkSource = m_context.NetworkDataset.SourceByID(sourceID)
			Dim fClassContainer As IFeatureClassContainer = TryCast(m_context.NetworkDataset, IFeatureClassContainer)
			Dim sourceFClass As IFeatureClass = fClassContainer.ClassByName(netSource.Name)
			Return sourceFClass.GetFeature(sourceOID)
		End Function

		''' <summary>
		''' Flash the feature geometry on the map
		''' <param name="pFeature">The feature being flashed</param>
		''' <param name="pMxDoc">A hook to the application display</param>
		''' <param name="direction">The digitized direction of the barrier with respect to the underlying source feature</param>
		''' </summary>
		Private Sub FlashFeature(ByVal pFeature As IFeature, ByVal direction As esriNetworkEdgeDirection)
			Dim pMxDoc As IMxDocument = TryCast(m_app.Document, IMxDocument)

			' Start drawing on screen. 
			pMxDoc.ActiveView.ScreenDisplay.StartDrawing(0, CShort(Fix(esriScreenCache.esriNoScreenCache)))

			' Switch functions based on Geometry type. 
			Select Case pFeature.Shape.GeometryType
				Case esriGeometryType.esriGeometryPolyline
					FlashLine(pMxDoc.ActiveView.ScreenDisplay, pFeature.Shape, direction)
				Case esriGeometryType.esriGeometryPolygon
					' no network elements can be polygons
				Case esriGeometryType.esriGeometryPoint
					FlashPoint(pMxDoc.ActiveView.ScreenDisplay, pFeature.Shape)
				Case Else
					Throw New Exception("Unexpected Geometry Type")
			End Select

			' Finish drawing on screen. 
			pMxDoc.ActiveView.ScreenDisplay.FinishDrawing()
		End Sub

		''' <summary>
		''' Flash a line feature on the map
		''' <param name="pDisplay">The map screen</param>
		''' <param name="pGeometry">The geometry of the feature to be flashed</param>
		''' <param name="direction">The digitized direction of the barrier with respect to the underlying source feature</param>
		''' </summary>
		Private Sub FlashLine(ByVal pDisplay As IScreenDisplay, ByVal pGeometry As IGeometry, ByVal direction As esriNetworkEdgeDirection)
			' The flash will be on a line symbol with an arrow on it
			Dim ipArrowLineSymbol As ICartographicLineSymbol = New CartographicLineSymbolClass()

			' the line color will be red
			Dim ipRgbRedColor As IRgbColor = New RgbColorClass()
			ipRgbRedColor.Red = 192

			' the arrow will be black
			Dim ipRgbBlackColor As IRgbColor = New RgbColorClass()
			ipRgbBlackColor.RGB = 0

			' set up the arrow that will be displayed along the line
			Dim ipArrowMarker As IArrowMarkerSymbol = New ArrowMarkerSymbolClass()
			ipArrowMarker.Style = esriArrowMarkerStyle.esriAMSPlain
			ipArrowMarker.Length = 18
			ipArrowMarker.Width = 12
			ipArrowMarker.Color = ipRgbBlackColor

			' set up the line itself
			ipArrowLineSymbol.Width = 4
			ipArrowLineSymbol.Color = ipRgbRedColor

			' Set up the Raster Op-Code to help the flash mechanism
			CType(ipArrowMarker, ISymbol).ROP2 = esriRasterOpCode.esriROPNotXOrPen
			CType(ipArrowLineSymbol, ISymbol).ROP2 = esriRasterOpCode.esriROPNotXOrPen

			' decorate the line with the arrow symbol
			Dim ipSimpleLineDecorationElement As ISimpleLineDecorationElement = New SimpleLineDecorationElementClass()
			ipSimpleLineDecorationElement.Rotate = True
			ipSimpleLineDecorationElement.PositionAsRatio = True
			ipSimpleLineDecorationElement.MarkerSymbol = ipArrowMarker
			ipSimpleLineDecorationElement.AddPosition(0.5)
			Dim ipLineDecoration As ILineDecoration = New LineDecorationClass()
			ipLineDecoration.AddElement(ipSimpleLineDecorationElement)
			CType(ipArrowLineSymbol, ILineProperties).LineDecoration = ipLineDecoration

			' the arrow is initially set to correspond to the digitized direction of the line
			'  if the barrier direction is against digitized, then we need to flip the arrow direction
			If direction = esriNetworkEdgeDirection.esriNEDAgainstDigitized Then
				ipSimpleLineDecorationElement.FlipAll = True
			End If

			' Flash the line
			'  Two calls are made to Draw.  Since the ROP2 setting is NotXOrPen, the first call
			'  draws the symbol with our new symbology and the second call redraws what was originally 
			'  in the place of the symbol
			pDisplay.SetSymbol(TryCast(ipArrowLineSymbol, ISymbol))
			pDisplay.DrawPolyline(pGeometry)
			System.Threading.Thread.Sleep(300)
			pDisplay.DrawPolyline(pGeometry)
		End Sub

		''' <summary>
		''' Flash a point feature on the map
		''' <param name="pDisplay">The map screen</param>
		''' <param name="pGeometry">The geometry of the feature to be flashed</param>
		''' </summary>
		Private Sub FlashPoint(ByVal pDisplay As IScreenDisplay, ByVal pGeometry As IGeometry)
			' for a point, we only flash a simple circle
			Dim pMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass()
			pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle

			' Set up the Raster Op-Code to help the flash mechanism
			Dim pSymbol As ISymbol = TryCast(pMarkerSymbol, ISymbol)
			pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

			' Flash the point
			'  Two calls are made to Draw.  Since the ROP2 setting is NotXOrPen, the first call
			'  draws the symbol with our new symbology and the second call redraws what was originally 
			'  in the place of the symbol
			pDisplay.SetSymbol(pSymbol)
			pDisplay.DrawPoint(pGeometry)
			System.Threading.Thread.Sleep(300)
			pDisplay.DrawPoint(pGeometry)
		End Sub

#End Region
	End Class
End Namespace
