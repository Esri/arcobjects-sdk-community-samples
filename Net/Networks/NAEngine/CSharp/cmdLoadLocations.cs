using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.NetworkAnalyst;

// This command allows users to load locations from another point feature layer into the selected NALayer and active category.
namespace NAEngine
{
	[Guid("72BDDCB7-03E8-4777-BECA-11DC47EFEDBA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("NAEngine.LoadLocations")]
	public sealed class cmdLoadLocations : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
	{
		private ESRI.ArcGIS.Controls.IMapControl3 m_mapControl;

		public cmdLoadLocations()
		{
			base.m_caption = "Load Locations...";
		}

		public override void OnClick()
		{
			if (m_mapControl == null)
			{
				MessageBox.Show("Error: Map control is null for this command");
				return;
			}

			// Get the NALayer and corresponding NAContext of the layer that
			// was right-clicked on in the table of contents
			// m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
			var naLayer = m_mapControl.CustomProperty as INALayer;
			if (naLayer == null)
			{
				MessageBox.Show("Error: NALayer was not set as the CustomProperty of the map control");
				return;
			}

			var naEnv = CommonFunctions.GetTheEngineNetworkAnalystEnvironment();
			if (naEnv == null || naEnv.NAWindow == null )
			{
				MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured");
				return;
			}

			ESRI.ArcGIS.Controls.IEngineNAWindowCategory naWindowCategory = naEnv.NAWindow.ActiveCategory;
			if (naWindowCategory == null )
			{
				MessageBox.Show("Error: There is no active category for the NAWindow");
				return;
			}

			INAClass naClass = naWindowCategory.NAClass;
			if (naClass == null)
			{
				MessageBox.Show("Error: There is no NAClass for the active category");
				return;
			}

			INAClassDefinition naClassDefinition = naClass.ClassDefinition;
			if (naClassDefinition == null)
			{
				MessageBox.Show("Error: NAClassDefinition is null for the active NAClass");
				return;
			}

			if (!naClassDefinition.IsInput)
			{
				MessageBox.Show("Error: Locations can only be loaded into an input NAClass");
				return;
			}

			// Set the Active Analysis layer to be the layer right-clicked on
			naEnv.NAWindow.ActiveAnalysis = naLayer;

			// Show the Property Page form for ArcGIS Network Analyst extension
			var loadLocations = new frmLoadLocations();
			if (loadLocations.ShowModal(m_mapControl, naEnv))
			{
				// Notify that the context has changed, because we have added locations to a NAClass within it
				var contextEdit = naEnv.NAWindow.ActiveAnalysis.Context as INAContextEdit;
				contextEdit.ContextChanged();

				// Refresh the NAWindow and the map
				m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, m_mapControl.Extent);
				naEnv.NAWindow.UpdateContent(naEnv.NAWindow.ActiveCategory);
			}
		}

		public override void OnCreate(object hook)
		{
			// The "hook" was set as a MapControl in formMain_Load
			m_mapControl = hook as ESRI.ArcGIS.Controls.IMapControl3;
		}
	}
}
