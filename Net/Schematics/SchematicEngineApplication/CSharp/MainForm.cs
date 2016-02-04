/*

   Copyright 2016 Esri

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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.SchematicControls;
using ESRI.ArcGIS.Schematic;

namespace SchematicApplication
{
	public sealed partial class MainForm : Form
	{
		#region class private members
		private ITOCControl2 m_tocControl;
		private IMapControl3 m_mapControl = null;

		private string m_mapDocumentName = string.Empty;
		private IToolbarMenu m_menuSchematicLayer;
		private IToolbarMenu m_menuLayer;
		private IToolbarMenu m_CreateMenu = new ToolbarMenuClass();
		private Boolean m_blnExistingMap = false;
		private ESRI.ArcGIS.esriSystem.IArray m_arrMaps = new ESRI.ArcGIS.esriSystem.Array();
		private Boolean m_blnToolbarItemClick = false;
		private Boolean m_blnOpenDoc = false;
		#endregion

		#region class constructor
		public MainForm()
		{
			InitializeComponent();
		}
		#endregion

		private void MainForm_Load(object sender, EventArgs e)
		{
			//get the MapControl and tocControl
			m_tocControl = (ITOCControl2)axTOCControl1.Object;
			m_mapControl = (IMapControl3)axMapControl1.Object;

			//Set buddy control for tocControl
			m_tocControl.SetBuddyControl(m_mapControl);
			axToolbarControl2.SetBuddyControl(m_mapControl);

			//disable the Save menu (since there is no document yet)
			menuSaveDoc.Enabled = false;
			axToolbarControl2.Select();

			//Create a SchematicEditor's MenuDef object
			IMenuDef menuDefSchematicEditor = new CreateMenuSchematicEditor();

			//Add SchematicEditor on the ToolBarMenu
			m_CreateMenu.AddItem(menuDefSchematicEditor, 0, -1, false, esriCommandStyles.esriCommandStyleIconAndText);

			//Set the ToolbarMenu's hook
			m_CreateMenu.SetHook(axToolbarControl2.Object);

			//Set the ToolbarMenu's caption
			m_CreateMenu.Caption = "SchematicEditor";

			/// Add ToolbarMenu on the ToolBarControl
			axToolbarControl2.AddItem(m_CreateMenu, -1, -1, false, 0, esriCommandStyles.esriCommandStyleMenuBar);

			///Create a other ToolbarMenu for layer
			m_menuSchematicLayer = new ToolbarMenuClass();
			m_menuLayer = new ToolbarMenuClass();

			///Add 3 items on the SchematicLayer properties menu 
			m_menuSchematicLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
			m_menuSchematicLayer.AddItem("esriControls.ControlsSchematicSaveEditsCommand", -1, 1, true, esriCommandStyles.esriCommandStyleIconAndText);
			m_menuSchematicLayer.AddItem("esriControls.ControlsSchematicUpdateDiagramCommand", -1, 2, false, esriCommandStyles.esriCommandStyleIconAndText);

			IMenuDef subMenuDef = new CreateSubMenuSchematic();
			m_menuSchematicLayer.AddSubMenu(subMenuDef, 3, true);
			////Add the sub-menu as the third item on the Layer properties menu, making it start a new group
			m_menuSchematicLayer.AddItem(new ZoomToLayer(), -1, 4, true, esriCommandStyles.esriCommandStyleTextOnly);

			m_menuLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
			m_menuLayer.AddItem(new ZoomToLayer(), -1, 1, true, esriCommandStyles.esriCommandStyleTextOnly);

			////Set the hook of each menu
			m_menuSchematicLayer.SetHook(m_mapControl);
			m_menuLayer.SetHook(m_mapControl);
		}

		private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
		{
			if (e.button != 2) return;

			esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
			IBasicMap map = null;
			ILayer layer = null;
			object other = null;
			object index = null;

			//Determine what kind of item is selected
			m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

			//Ensure the item gets selected 
			if (item == esriTOCControlItem.esriTOCControlItemMap)
				m_tocControl.SelectItem(map, null);
			else
				m_tocControl.SelectItem(layer, null);

			//Set the layer into the CustomProperty (this is used by the custom layer commands)			
			m_mapControl.CustomProperty = layer;

			ISchematicLayer schLayer = layer as ISchematicLayer;
			if (schLayer != null) /// attach menu for SchematicLayer
			{
				ISchematicTarget schematicTarget = new ESRI.ArcGIS.SchematicControls.EngineSchematicEnvironmentClass() as ISchematicTarget;
				if (schematicTarget != null)
					schematicTarget.SchematicTarget = schLayer;

				//Popup the correct context menu

				if (item == esriTOCControlItem.esriTOCControlItemLayer) m_menuSchematicLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);
			}
			else /// attach menu for Layer
			{
				//Popup the correct context menu
				if (item == esriTOCControlItem.esriTOCControlItemLayer) m_menuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);
			}
		}


		#region Main Menu event handlers
		private void menuNewDoc_Click(object sender, EventArgs e)
		{
			//execute New Document command
			ICommand command = new CreateNewDocument();
			command.OnCreate(m_mapControl.Object);
			command.OnClick();
		}

		private void menuOpenDoc_Click(object sender, EventArgs e)
		{
			//execute Open Document command
			ICommand command = new ControlsOpenDocCommandClass();
			command.OnCreate(m_mapControl.Object);
			command.OnClick();
			m_blnOpenDoc = true;
			m_blnExistingMap = false;
			cboFrame.Items.Clear();
		}

		private void menuSaveDoc_Click(object sender, EventArgs e)
		{
			//execute Save Document command
			if (m_mapControl.CheckMxFile(m_mapDocumentName))
			{
				//create a new instance of a MapDocument
				IMapDocument mapDoc = new MapDocumentClass();
				mapDoc.Open(m_mapDocumentName, string.Empty);

				//Make sure that the MapDocument is not read only
				if (mapDoc.get_IsReadOnly(m_mapDocumentName))
				{
					MessageBox.Show("Map document is read only!");
					mapDoc.Close();
					return;
				}

				//Replace its contents with the current map
				mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

				//save the MapDocument in order to persist it
				mapDoc.Save(mapDoc.UsesRelativePaths, false);

				//close the MapDocument
				mapDoc.Close();
			}
		}

		private void menuSaveAs_Click(object sender, EventArgs e)
		{
			//execute SaveAs Document command
			ICommand command = new ControlsSaveAsDocCommandClass();
			command.OnCreate(m_mapControl.Object);
			command.OnClick();
		}

		private void menuExitApp_Click(object sender, EventArgs e)
		{
			//exit the application
			Application.Exit();
		}


		#endregion



		//listen to MapReplaced event in order to update the status bar and the Save menu
		private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
		{
			//get the current document name from the MapControl
			m_mapDocumentName = m_mapControl.DocumentFilename;

			if (m_blnToolbarItemClick == true)
			{
				m_blnToolbarItemClick = false;
				m_blnExistingMap = true;
				//need to add the new diagram to the combobox
				IMap p = (IMap)e.newMap;
				if (!cboFrame.Items.Contains(p.Name))
					cboFrame.Items.Add(p.Name.ToString());
				m_arrMaps.Add(p);
			}

			if (m_blnExistingMap == false)
			{
				IMap m;
				m_arrMaps = axMapControl1.ReadMxMaps(m_mapDocumentName);
				Int16 i;
				for (i = 0; i < m_arrMaps.Count; i++)
				{
					m = (IMap)m_arrMaps.Element[i];

					if (!cboFrame.Items.Contains(m.Name.ToString()))
						cboFrame.Items.Add(m.Name.ToString());
				}
				cboFrame.Text = this.axMapControl1.ActiveView.FocusMap.Name;
			}
			//if there is no MapDocument, disable the Save menu and clear the status bar
			if (m_mapDocumentName == string.Empty)
			{
				menuSaveDoc.Enabled = false;
				statusBarXY.Text = string.Empty;
			}
			else
			{
				//enable the Save menu and write the doc name to the status bar
				menuSaveDoc.Enabled = true;
				statusBarXY.Text = Path.GetFileName(m_mapDocumentName);
			}

			m_blnExistingMap = true;

			cboFrame.Text = axMapControl1.Map.Name.ToString();
		}

		private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
		{
			statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
		}

		private void onSelectedValueChanged(object sender, EventArgs e)
		{
			IMap m;
			Int16 i;
			for (i = 0; i < m_arrMaps.Count; i++)
			{
				m = (IMap)m_arrMaps.Element[i];
				if (m.Name == cboFrame.Text)
				{
					m_blnExistingMap = true;
					m_mapControl.Map = (IMap)m_arrMaps.Element[i];
					m_mapControl.Refresh();
					m_blnExistingMap = false;
					return;
				}
			}
		}

		//specific for toolbar2
		private void onToolbarItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
		{
			if (e.index == 0)
			{
				//clicked generate new diagram
				m_blnToolbarItemClick = true;

			}
			//if (m_blnOpenDoc == false)
			//{
			//    m_blnToolbarItemClick = true;

			//}
			//else
			//{
			//    m_blnOpenDoc = false;
			//}

		}

	}
}