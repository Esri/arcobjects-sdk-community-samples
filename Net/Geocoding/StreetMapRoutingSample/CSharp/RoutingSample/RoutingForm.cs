using System;

using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Location;
using ESRI.ArcGIS.LocationUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace RoutingSample
{
	public partial class RoutingForm : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public RoutingForm() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			m_dlgDirections = new DirectionsForm();
			m_dlgRestrictions = new RestrictionsForm();
		}

	#endregion

	#region Form Buttons Events Handlers

		// Just hides form on closing
		private void RoutingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();

			// Bug workaround.
			// ArcMap can lose focus after form closing. SetFocus prevents it.
			SetFocus(m_application.hWnd);
		}

		// Bug workaround.
		// Allows Tab and Escape buttons using in form
		private void RoutingForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (! this.Modal)
			{
				if (e.KeyCode.Equals((Keys.Tab)))
				{
					e.Handled = true;
					this.ProcessTabKey(! (e.Modifiers == Keys.Shift));
				}
				else if (e.KeyCode.Equals((Keys.Escape)))
					this.Close();
			}
		}

		// Bug workaround.
		// Allows Tab button using in form
		protected override bool ShowFocusCues
		{
			get
			{
				if (this.Modal)
					return base.ShowFocusCues;
				else
					return true;
			}
		}

		private void m_btnRoutingService_Click(object sender, System.EventArgs e)
		{
			OpenRoutingService();
		}

		private void m_btnAddressLocator_Click(object sender, System.EventArgs e)
		{
			OpenAddressLocator();
		}

		private void m_btnBarriersOpen_Click(object sender, System.EventArgs e)
		{
			OpenBarriers();
		}

		private void m_btnBarriersClear_Click(object sender, System.EventArgs e)
		{
			ClearBarriers();
		}

		private void m_btnFindRoute_Click(object sender, System.EventArgs e)
		{
			FindRoute();
		}

		private void m_btnShowDirections_Click(object sender, System.EventArgs e)
		{
			ShowDirections();
		}

		private void m_btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void RoutingForm_Load(object sender, System.EventArgs e)
		{
			m_cmbDistanceUnit.SelectedIndex = 2;
		}

		private void m_btnRestrictions_Click(object sender, System.EventArgs e)
		{
			ShowRestrictions();
		}

	#endregion

	#region Public members

		// Store ArcMap application as internal member
		public void Init(IApplication application)
		{
			m_application = application;
		}

	#endregion

	#region Private Helpers

	#region Initialization helpers

		// Open Route Service file and create Router and Spatial Reference
		private void OpenRoutingService()
		{
			try
			{
				DialogResult res = m_dlgRoutingSrvc.ShowDialog(this);
				if (res == System.Windows.Forms.DialogResult.OK)
				{
					string strPath = m_dlgRoutingSrvc.FileName;

					// create router factory    
					if (m_objRouterFactory == null)
							m_objRouterFactory = new SMRouterFactoryClass();

					// if new path passed
					if ((m_objRouter == null) | (strPath != m_strRouteServicePath))
					{
						// Barriers should be created again for new Router
						ClearBarriers();

						m_strRouteServicePath = strPath;

						// create new router 
						m_objRouter = m_objRouterFactory.CreateRouter(m_strRouteServicePath);
						m_txtRoutingService.Text = m_strRouteServicePath;

						//get Router Spatial Reference
						m_objSpatialReference = CreateSpatialReference(m_objRouter.ProjectionString);

						// Init Restrictions
						m_dlgRestrictions.Init(m_objRouter);
					}
				}
			}
			catch (Exception ex)
			{
				// Clear on Error
				m_objRouter = null;
				m_objSpatialReference = null;
				m_txtRoutingService.Text = "";
				m_strRouteServicePath = "";
			}
			finally
			{
				// Check FindRoute, Barriers and Restrictions buttons state
				CheckRouteButtons();
			}
		}

		// Opens Address Locator for addresses geocoding
		private void OpenAddressLocator()
		{
			try
			{
				// Create Dialog on first call and init filter
				InitAddressLocatorDlg();

				ILocator objLocator = null;
				objLocator = null;

				// Get Locator
				IEnumGxObject gxObjects = null;
				gxObjects = null;

				if (m_dlgAddressLocator.DoModalOpen(this.Handle.ToInt32(), out gxObjects) & (gxObjects != null))
				{
					gxObjects.Reset();

					// Get first Locator
					IGxObject gxObject = null;
					gxObject = gxObjects.Next();

					if (gxObject != null)
					{
						IGxLocator gxLocator = null;
						gxLocator = gxObject as IGxLocator;
						objLocator = gxLocator.Locator;
						m_objAddressGeocoding = objLocator as IAddressGeocoding;
					}
				}

				if (m_objAddressGeocoding == null)
					m_txtAddressLocator.Text = "";
				else
					m_txtAddressLocator.Text = objLocator.Name;
			}
			catch (Exception ex)
			{
				// Clear on Error
				m_objAddressGeocoding = null;
				m_txtAddressLocator.Text = "";
			}
			finally
			{
				// Check FindRoute, Barriers and Restrictions buttons state
				CheckRouteButtons();
			}
		}

		// Adds Barriers from Dataset
		private void OpenBarriers()
		{
			System.Windows.Forms.Cursor Cursor = this.Cursor;
			try
			{
				// Create Dialog on first call and init filter
				InitBarriersDlg();

				// Get Barriers 
				IGxObject gxObject = null;

				IEnumGxObject gxObjects = null;
				gxObjects = null;

				if (m_dlgBarriers.DoModalOpen(this.Handle.ToInt32(), out gxObjects) & (gxObjects != null))
				{

					this.Cursor = Cursors.WaitCursor;

					// Init Barriers
					ClearBarriers();

					// use first object
					gxObjects.Reset();
					gxObject = gxObjects.Next();

					// Use first object
					if (gxObject != null)
					{
						// Add Barriersfrom object dataset
						IGxDataset objGxDS = null;
						objGxDS = gxObject as IGxDataset;

						AddBarriersFromDataset(objGxDS.Dataset);
					}

					// Is Barriers added to Router
					if (m_nBarriersCount == 0)
						m_txtBarriers.Text = "";
					else
						m_txtBarriers.Text = gxObject.Name;

					if (m_nBarriersIgnoredCount > 0)
						MessageBox.Show(this, m_nBarriersIgnoredCount.ToString() + " barriers cannot be added.", "Routing Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}

			}
			catch (Exception ex)
			{
				// Clear on Error
				ClearBarriers();
			}
			finally
			{
				this.Cursor = Cursor;
			}
		}

	#endregion

	#region Finding Route helpers

		// Find route and initialize Driving Directions page
		private void FindRoute()
		{
			if (m_objRouter == null)
					return;

			// Init DD
			m_btnShowDirections.Enabled = false;
			m_dlgDirections.Init();

			System.Windows.Forms.Cursor Cursor = this.Cursor;
			try
			{
				this.Cursor = Cursors.WaitCursor;

				// set highways priority (0.0 - 100.0)    
				SMRoadPreferences objPreferences = m_objRouter.Preferences;
				objPreferences.set_Item(esriSMRoadType.esriSMRoadTypeHighways, (short)m_trackUseRoad.Value);

				// set route type (Time/Length)    
				if (m_rbtnQuickest.Checked)
					m_objRouter.NetAttributeName = "Time";
				else
					m_objRouter.NetAttributeName = "Length";

				// Set Length Units for Directions output
				SetDirectionsUnits();

				// Geocode user data and adds Start and finish
				SMStopsCollection objStopsCol = new SMStopsCollectionClass(); ;
				AddStops(objStopsCol);

				// Add restrictions
				m_dlgRestrictions.SetupRouter(m_objRouter);

				// get driving directions    
				ISMDirections objDirections = null;
				objDirections = m_objRouter.Solve(objStopsCol, null);

				// Output result, zoom to route, fill directions
				ShowResult(objDirections);

			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Cannot find route." + System.Environment.NewLine + ex.Message, "Routing Sample", MessageBoxButtons.OK, MessageBoxIcon.Error);

				// Init DD
				m_btnShowDirections.Enabled = false;
				m_dlgDirections.Init();
			}
			finally
			{
				this.Cursor = Cursor;
			}

		}

		// Geocodes user data and adds Start and finish
		private void AddStops(SMStopsCollection objStopsCol)
		{
			// create and add "From" stop to stops collection    
			CreateStop(m_objRouter, m_txtStartAddress.Text, m_txtStartCity.Text, m_txtStartState.Text, m_txtStartCode.Text, objStopsCol, 0);

			// create and add "To" stop to stops collection    
			CreateStop(m_objRouter, m_txtFinishAddress.Text, m_txtFinishCity.Text, m_txtFinishState.Text, m_txtFinishCode.Text, objStopsCol, 1);
		}

		// Returns Address string
		private string GetAddressString(string strAddress, string strCity, string strState, string strCode)
		{

			string strAddress1 = strAddress;
			if (strAddress1.Length > 0)
					strAddress1 = strAddress1 + ", ";

			string strCity1 = strCity;
			if (strCity1.Length > 0)
					strCity1 = strCity1 + ", ";

			string strCode1 = strCode;
			if (strCode1.Length > 0)
					strCode1 = strCode1 + ", ";

			string strRes = strAddress1 + strCode1 + strCity1 + strState;

			if (strRes.EndsWith(", "))
					strRes = strRes.Remove(strRes.Length - 2, 2);

			return strRes;
		}

		// Geocodes address by Address, City, State, ZIP code
		private IPoint GeocodeAddress(string strAddress, string strCity, string strState, string strCode)
		{

			if (m_objAddressGeocoding == null)
				throw new Exception("Cannot geocode address.");

			// Get Address fields from textboxes
			IPropertySet objAddressProperties = new PropertySetClass();
			objAddressProperties.SetProperty("Street", strAddress);
			objAddressProperties.SetProperty("City", strCity);
			objAddressProperties.SetProperty("State", strState);
			objAddressProperties.SetProperty("ZIP", strCode);

            //// Match Address
            //IPropertySet objMatchProperties = null;
            //objMatchProperties = m_objAddressGeocoding.MatchAddress(objAddressProperties);

            //IFields objMatchFields = null;
            //objMatchFields = m_objAddressGeocoding.MatchFields;

            //if (objMatchFields.FieldCount > 0)
            //{
            //    // Use first candidate
            //    IField objMatchField = null;
            //    objMatchField = objMatchFields.get_Field(0);

            //    if (objMatchField.Type == esriFieldType.esriFieldTypeGeometry)
            //        return objMatchProperties.GetProperty(objMatchField.Name) as IPoint;
            //}

            // Match Address
            IPropertySet objMatchProperties = null;
            objMatchProperties = m_objAddressGeocoding.MatchAddress(objAddressProperties);

            IFields objMatchFields = null;
            objMatchFields = m_objAddressGeocoding.MatchFields;

            if (objMatchFields.FieldCount > 0)
            {
                // Use first candidate
                IField objGeometryField = null;

                int nFieldCount = objMatchFields.FieldCount;
                for (int nIndex = 0; nIndex < nFieldCount; nIndex++)
                {
                    IField objCurField = objMatchFields.get_Field(nIndex);
                    if (objCurField.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        objGeometryField = objCurField;
                        break;
                    }
                }

                if (objGeometryField != null)
                    return objMatchProperties.GetProperty(objGeometryField.Name) as IPoint;
                else
                    throw new Exception("Cannot obtain geometry field.");
            }

			throw new Exception("Cannot geocode address.");
		}

		// Creates Stop by Stop Point, Index and Description and adds it to Stops collection
		private void CreateStop(SMRouter objRouter, string strAddress, string strCity, string strState, string strCode, SMStopsCollection objStopsCol, int nID)
		{

			// geocode point
			IPoint objPoint = new PointClass();
			objPoint = GeocodeAddress(strAddress, strCity, strState, strCode);

			if (objPoint.IsEmpty)
					throw new Exception("Cannot geocode address.");

			// project point
			objPoint.Project(m_objSpatialReference);

			// create and initialize router point    
			SMRouterPoint objRouterPoint = null;
			objRouterPoint = new SMRouterPointClass();

			objRouterPoint.X = objPoint.X;
			objRouterPoint.Y = objPoint.Y;

			// create flag    
			ISMFlagCreator2 objFlagCreator2 = null;
			objFlagCreator2 = objRouter.FlagCreator as ISMFlagCreator2;
			if (objFlagCreator2 != null)
					objFlagCreator2.SearchTolerance = 5;

			SMFlag objFlag = null;
			objFlag = objRouter.FlagCreator.CreateFlag(objRouterPoint);

			// create and initialize stop    
			SMStop objStop = null;
			objStop = new SMStop();
			objStop.StopID = nID;
			objStop.Duration = 0;
			objStop.Flag = objFlag;
			objStop.Description = GetAddressString(strAddress, strCity, strState, strCode);

			objStopsCol.Add(objStop);
		}

		private ISpatialReference CreateSpatialReference(string strPrjString)
		{
			ISpatialReference temp_CreateSpatialReference = null;
			// create router spatial reference    
			ISpatialReferenceFactory objSpatRefFact = null;
			objSpatRefFact = new SpatialReferenceEnvironmentClass();

			// create temporary file
			string strTempPath = System.IO.Path.GetTempFileName();

			try
			{
				System.IO.StreamWriter wrtr = System.IO.File.CreateText(strTempPath);
				wrtr.Write(strPrjString);
				wrtr.Close();

				ISpatialReference objSR = null;
				objSR = objSpatRefFact.CreateESRISpatialReferenceFromPRJFile(strTempPath);

				temp_CreateSpatialReference = objSR;
			}
			finally
			{
				System.IO.File.Delete(strTempPath);
			}

			return temp_CreateSpatialReference;
		}

		// Set length unit for Direction
		private void SetDirectionsUnits()
		{
			int nIndex = m_cmbDistanceUnit.SelectedIndex;

			// Get Router Setup object
			ISMRouterSetup2 objSetup = null;
			objSetup = m_objRouter as ISMRouterSetup2;

			// get Directions unit
			esriSMDirectionsLengthUnits eUnits = 0;

			if (nIndex == 0) // Feet
				eUnits = esriSMDirectionsLengthUnits.esriSMDLUFeet;
			else if (nIndex == 1) // Yards
				eUnits = esriSMDirectionsLengthUnits.esriSMDLUYards;
			else if (nIndex == 2) // Miles
				eUnits = esriSMDirectionsLengthUnits.esriSMDLUMiles;
			else if (nIndex == 3) // Meters
				eUnits = esriSMDirectionsLengthUnits.esriSMDLUMeters;
			else if (nIndex == 4) // Kilometers
				eUnits = esriSMDirectionsLengthUnits.esriSMDLUKilometers;

			// Set Directions Unit
			objSetup.DirectionsLengthUnits = eUnits;
		}

	#endregion

	#region Resulting helpers

		// Outputs result, zoom to route, fill directions
		private void ShowResult(ISMDirections objDirections)
		{
			// Fill directions
			m_dlgDirections.Init(objDirections);
			m_btnShowDirections.Enabled = true;

			try
			{
                // Zoom to extent
				Envelope objEnv = null;
				objEnv = new Envelope();

				objEnv.PutCoords(objDirections.BoundBox.Left, objDirections.BoundBox.Bottom, objDirections.BoundBox.Right, objDirections.BoundBox.Top);
				objEnv.Expand(1.1, 1.1, true);

				// set spatial reference
				IGeometry objGeo = objEnv as IGeometry;
				objGeo.SpatialReference = m_objSpatialReference;

				ZoomToExtent(objEnv);

				// Create polyline
				CreatePolyline(objDirections);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Cannot show result.", "Routing Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				// Refresh map
				RefreshMap();
			}
		}

		// refreshes Map
		private void RefreshMap()
		{
			IMxDocument objDoc = (IMxDocument)m_application.Document; 
			IMap objMap = objDoc.FocusMap;
			IActiveView objView = objMap as IActiveView;
			objView.Refresh();
		}

		// Zooms Map to extent
		private void ZoomToExtent(Envelope objEnv)
		{
			IMxDocument objDoc = m_application.Document as IMxDocument;
			IMap objMap = objDoc.FocusMap;
			IActiveView objView = objMap as IActiveView;

			// check if projections are not equal
			IGeometry objGeo = objEnv as IGeometry;

			// project to Map projection and expand
			if (! (IsSREqual(objGeo.SpatialReference, objMap.SpatialReference)))
					((IGeometry)objEnv).Project(objMap.SpatialReference);

			// Zoom
			objView.Extent = objEnv as IEnvelope;
		}

		// Returns true if Spatial Systems is equal (without precision)
		private bool IsSREqual(ISpatialReference objSR1, ISpatialReference objSR2)
		{
			IClone objSRClone1 = objSR1 as IClone;
			return objSRClone1.IsEqual((IClone)objSR2);
		}

		// Creates Route polyline
		private void CreatePolyline(ISMDirections objDirections)
		{
			// create polyline
			IPolyline objLine = null;
			objLine = new PolylineClass();

			// get points collection
			IPointCollection objPoints = null;
			objPoints = objLine as IPointCollection;

			// Adds Directions points to polyline
			AddPointsToPolyline(objDirections, ref objPoints);

			// Project points to Map projection
			IMxDocument objDoc = m_application.Document as IMxDocument;
			IMap objMap = objDoc.FocusMap;
			objLine.Project(objMap.SpatialReference);

			// create path graphics element
			IElement objElement = null;
			objElement = new LineElementClass();

			objElement.Geometry = objLine;

			// Set line color width and style
			SetLineProperties(objElement);

            // get Graphic container
			IGraphicsContainer objCont = objMap as IGraphicsContainer;

			// Add line to map
			objCont.AddElement(objElement, 0);
		}

		// Sets line color, width and style
		private void SetLineProperties(IElement objElement)
		{
			SimpleLineSymbol objSymbol = null;
			objSymbol = new SimpleLineSymbolClass();

			// Set Symbol style and width
			objSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
			objSymbol.Width = 10;

			// Set symbol color
			RgbColor objColor = null;
			objColor = new RgbColorClass();
			objColor.Red = 0;
			objColor.Green = 255;
			objColor.Blue = 0;

			objSymbol.Color = objColor;

			// set line symbol
			ILineElement objLineElement = objElement as ILineElement;
			objLineElement.Symbol = objSymbol as ILineSymbol;
		}

		// Adds Directions points to point collection
		private void AddPointsToPolyline(ISMDirections objDirections, ref IPointCollection objPoints)
		{

			Point objPoint = new PointClass();

			// copy points from DD to line
			int nItemsCount = objDirections.Count;
			for (int i = 0; i < nItemsCount; i++)
			{
				// get shape from Direction
				ISMDirItem objItem = null;
				objItem = objDirections.get_Item(i) as ISMDirItem;

				ISMPointsCollection objShape = null;
				objShape = objItem.Shape;

                // Add point from Direction to received collection
				int nPointsCount = objShape.Count - 1;
				for (int j = 0; j <= nPointsCount; j++)
				{
					// get point from route
					SMRouterPoint objRouterPoint = objShape.get_Item(j);

                    // Optimization: Not add point if last added point has similar coords
					bool bAddPoint = false;

					if (objPoint.IsEmpty)
						bAddPoint = true;
					else if ((objPoint.X != objRouterPoint.X) & (objPoint.Y != objRouterPoint.Y))
						bAddPoint = true;

					if (bAddPoint)
					{
						// Add point if need
						objPoint.X = objRouterPoint.X;
						objPoint.Y = objRouterPoint.Y;
						objPoint.SpatialReference = m_objSpatialReference;

						object missing = System.Reflection.Missing.Value;
						objPoints.AddPoint(objPoint, ref missing, ref missing);
					}
				}
			}
		}

	#endregion

	#region Barriers helpers

		// Adds barriers from object dataset
		private void AddBarriersFromDataset(IDataset objDS)
		{
			if (objDS.Type == esriDatasetType.esriDTFeatureClass)
			{
				// It is Feature Class
				AddBarriersFromFeatureClass(objDS as IFeatureClass);

			}
			else if (objDS.Type == esriDatasetType.esriDTFeatureDataset)
			{
				// It is Feature DatasetClass

				IFeatureDataset objFeatureDS = null;
				objFeatureDS = objDS as IFeatureDataset;

				// Enum Dataset subsets
				IEnumDataset objEnumDS = null;
				objEnumDS = objFeatureDS.Subsets;

				IDataset objSubDS = null;
				objSubDS = objEnumDS.Next();
				while ( objSubDS != null)
				{
					// Add Barriers from subset Feature Class 
					if (objSubDS.Type == esriDatasetType.esriDTFeatureClass)
							AddBarriersFromFeatureClass(objSubDS as IFeatureClass);

					objSubDS = objEnumDS.Next();
				}
			}
		}

		// Adds Barriers from Feature Class 
		private void AddBarriersFromFeatureClass(IFeatureClass objFeatureClass)
		{
			// only for Point features
			if (objFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
			{
				// create filter
				IQueryFilter objFilter = null;
				objFilter = new QueryFilterClass();

				// enum features
				IFeatureCursor objCursor = null;
				objCursor = objFeatureClass.Search(objFilter, false);

				IFeature objFeature = null;
				objFeature = objCursor.NextFeature();

				while ( objFeature != null)
				{
					// Add feature Point to Barriers List
					AddBarrier(objFeature.ShapeCopy as IPoint);

					objFeature = objCursor.NextFeature();
				}

			}
		}

		// Add Barrier
		private void AddBarrier(IPoint objPoint)
		{
			// project to Routing projection
			objPoint.Project(m_objSpatialReference);

			// add point from shape to Barriers
			ISMRouterPoint objSMPoint = null;
			objSMPoint = new SMRouterPointClass();

			objSMPoint.X = objPoint.X;
			objSMPoint.Y = objPoint.Y;

			ISMNetBarrier objBarrier = null;
			objBarrier = new SMNetBarrierClass();

			objBarrier.BarrierID = m_nBarriersCount;
			objBarrier.Point = objSMPoint as SMRouterPointClass;

			try
			{
				m_objRouter.Barriers.Add(objBarrier as SMNetBarrier);
			}
			catch (Exception ex)
			{
				m_nBarriersIgnoredCount = m_nBarriersIgnoredCount + 1;
			}

			m_nBarriersCount = m_nBarriersCount + 1;
		}

        // Clears textbox, Barriers collection in Router
		private void ClearBarriers()
		{
			m_nBarriersCount = 0;
			m_nBarriersIgnoredCount = 0;
			m_txtBarriers.Text = "";
			if (m_objRouter != null)
					m_objRouter.Barriers.Clear();
		}

	#endregion

	#region File Dialogs helpers

		// Open Directions Dialog
		private void ShowDirections()
		{
			m_dlgDirections.ShowDialog(this);
		}

		// Open Restrictions Dialog
		private void ShowRestrictions()
		{
			m_dlgRestrictions.ShowDialog(this);
		}

		// Checks FindRoute, Barriers and Restrictions buttons state
		private void CheckRouteButtons()
		{
			m_btnFindRoute.Enabled = false;
			m_btnBarriersOpen.Enabled = false;
			m_btnRestrictions.Enabled = false;

			if (m_objRouter != null)
			{
				if (m_objAddressGeocoding != null)
					m_btnFindRoute.Enabled = true;

				m_btnBarriersOpen.Enabled = true;
				m_btnRestrictions.Enabled = true;
			}
		}

		// Creates Dialog on first call and init filter
		private void InitAddressLocatorDlg()
		{
			if (m_dlgAddressLocator == null)
			{
                // Create Address Locator Dialog
				m_dlgAddressLocator = new GxDialogClass();

				IGxObjectFilter filter = null;
				filter = new GxFilterAddressLocatorsClass();

				m_dlgAddressLocator.ObjectFilter = filter;
				m_dlgAddressLocator.Title = "Add Address Locator";
			}
		}

		// Creates Dialog on first call and init filter
		private void InitBarriersDlg()
		{
			if (m_dlgBarriers == null)
			{
				// Create Barriers dialog
				m_dlgBarriers = new GxDialogClass();

				IGxObjectFilter objFilter = null;

				objFilter = new GxFilterFeatureDatasetsAndFeatureClassesClass();
				m_dlgBarriers.ObjectFilter = objFilter;

				m_dlgBarriers.Title = "Choose Barriers Layer";
			}
		}

	#endregion

	#region Imported methods
		[System.Runtime.InteropServices.DllImport("user32", EntryPoint="SetFocus", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Ansi, SetLastError=true)]
		public static extern int SetFocus(int Hwnd);
	#endregion

	#endregion

	#region Private members

		// ArcMap application
		private IApplication m_application;

		// Driving Direction dialog
		private DirectionsForm m_dlgDirections;

		// Router factory and object
		private SMRouterFactory m_objRouterFactory;
		private SMRouter m_objRouter;
		// Store Route Services path
		private string m_strRouteServicePath;
		// Spatial Reference of Router object
		private ISpatialReference m_objSpatialReference;

		// Address Locator dialog
		private GxDialog m_dlgAddressLocator;
		// Address Geocoding object
		private IAddressGeocoding m_objAddressGeocoding;

		// Barriers dialog 
		private GxDialogClass m_dlgBarriers;
		// Added Barriers count - used for current barrier ID
		private int m_nBarriersCount;
		// Number of ignored Barriers
		private int m_nBarriersIgnoredCount;

		// Restrictions dialog
		private RestrictionsForm m_dlgRestrictions;

	#endregion

	}


} //end of root namespace