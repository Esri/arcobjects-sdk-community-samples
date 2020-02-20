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
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.NetworkAnalystUI;
using System.Text;

namespace ExportNAClass
{
	/// <summary>
	/// This sample command allows you export a text file version
	/// of the active class in the ArcGIS Network Analyst extension window after 
	/// completion of a successful solve.
	/// </summary>
	/// 
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("08CE5834-8267-4a73-AFDD-2821B4B1F6EC")]
	[ProgId("ExportNAClass.NAClassToTextfileCmd")]
	public sealed class NAClassToTextfileCmd : BaseCommand, INAWindowCommand
	{
		private const string DELIMITER = "\t";
		private INetworkAnalystExtension m_naExt;

		public NAClassToTextfileCmd()
		{
			base.m_category = "Developer Samples";
			base.m_caption = "Export To text file...";
			base.m_message = "Export a network analysis class to a text file.";
			base.m_toolTip = "Export a network analysis class to a text file.";
			base.m_name = "NAClassToTextFileCmd";

			try
			{
				string bitmapResourceName = GetType().Name + ".bmp";
				base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
		}

		#region COM Registration Function(s)

		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType);
		}

		#region ArcGIS Component Category Registrar generated code
		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ESRI.ArcGIS.ADF.CATIDs.MxCommands.Register(regKey);
			// Register with NetworkAnalystWindowItemsCommand to get the 
			// command to show up when you right click on the class in the NAWindow
			ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowCategoryCommand.Register(regKey);
		}

		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ESRI.ArcGIS.ADF.CATIDs.MxCommands.Unregister(regKey);
			ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowCategoryCommand.Unregister(regKey);
		}

		#endregion

		#endregion

		#region "NAWindow Interaction"
		private INALayer GetActiveAnalysisLayer()
		{
			if (m_naExt != null)
				return m_naExt.NAWindow.ActiveAnalysis;
			else
				return null;
		}

		private INAWindowCategory2 GetActiveCategory()
		{
			if (m_naExt != null)
				return m_naExt.NAWindow.ActiveCategory as INAWindowCategory2;
			else
				return null;
		}
		#endregion

		#region "Overridden INAWindowCommand Methods"
		public bool Applies(INALayer naLayer, INAWindowCategory Category)
		{
			return true;
		}
		#endregion

		#region "Overridden BaseCommand Methods"
		public override void OnCreate(object hook)
		{
			// Try to get the ArcGIS Network Analyst extension from the desktop app's extensions
			IApplication app;
			app = hook as IApplication;
			if (app != null)
				m_naExt = app.FindExtensionByName("Network Analyst") as INetworkAnalystExtension;
		}

		/// <summary>
		/// This command will be enabled only for a NAClass
		/// associated with a successful solve
		/// </summary>
		public override bool Enabled
		{
			get
			{
				// there must be an active analysis layer
				INALayer naLayer = GetActiveAnalysisLayer();
				if (naLayer != null)
				{
					// the context must be valid
					INAContext naContext = naLayer.Context;
					if (naContext != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		public override void OnClick()
		{
			try
			{
				ExportToText();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Error");
			}

		}
		#endregion

		private void ExportToText()
		{
			SaveFileDialog sfDialog = new SaveFileDialog();
			SetUpSaveDialog(ref sfDialog);
			// generate the dialog and verify the user successfully clicked save
			DialogResult dResult = sfDialog.ShowDialog();

			if (dResult == DialogResult.OK)
			{
				// set up the text file to be written
				FileInfo t = new FileInfo(sfDialog.FileName);
				StreamWriter swText = t.CreateText();

				ITable table = GetActiveCategory().DataLayer as ITable;

				// write the first line of the text file as column headers
				swText.WriteLine(GenerateColumnHeaderString(ref table));

				// iterate through the table associated with the class
				// to write out each line of data into the text file
				ICursor cursor = table.Search(null, true);
				IRow row = cursor.NextRow();
				while (row != null)
				{
					swText.WriteLine(GenerateDataString(ref row));
					row = cursor.NextRow();
				}
				swText.Close();
			}
		}

		private void SetUpSaveDialog(ref SaveFileDialog sfDialog)
		{
			sfDialog.AddExtension = true;
			sfDialog.Title = "Save an export of the specified class in the active analysis...";
			sfDialog.DefaultExt = "txt";
			sfDialog.OverwritePrompt = true;
			sfDialog.FileName = "ClassExport.txt";
			sfDialog.Filter = "Text files (*.txt;*.csv;*.asc;*.tab)|*.txt;*.tab;*.asc;*.csv";
			sfDialog.InitialDirectory = "c:\\";
		}

		private string GenerateColumnHeaderString(ref ITable table)
		{
			IField field = null;

			// export the names of the fields (tab delimited) as the first line of the export
			string fieldNames = "";
			for (int i = 0; i < table.Fields.FieldCount; i++)
			{
				field = table.Fields.get_Field(i);
				if (i > 0) fieldNames += DELIMITER;

				string columnName = field.Name.ToString();

				// point classes have a special output of X and Y, other classes just output "Shape"
				if (field.Type == esriFieldType.esriFieldTypeGeometry)
				{
					if (field.GeometryDef.GeometryType == esriGeometryType.esriGeometryPoint)
					{
						columnName = "X";
						columnName += DELIMITER;
						columnName += "Y";
					}
				}
				fieldNames += columnName;
			}
			return fieldNames;
		}

		private string GenerateDataString(ref IRow row)
		{
			string textOut = "";

			// On a zero-based index, iterate through the fields in the collection.
			for (int fieldIndex = 0; fieldIndex < row.Fields.FieldCount; fieldIndex++)
			{
				if (fieldIndex > 0) textOut += DELIMITER;
				IField field = row.Fields.get_Field(fieldIndex);

				// for shape fields in a point layer, export the associated X and Y coordinates
				if (field.Type == esriFieldType.esriFieldTypeGeometry)
				{
					if (field.GeometryDef.GeometryType == esriGeometryType.esriGeometryPoint)
					{
						// x y location information must be retrieved from the Feature
						IPoint point = row.get_Value(fieldIndex) as ESRI.ArcGIS.Geometry.Point;
						textOut += point.X.ToString();
						textOut += DELIMITER;
						textOut += point.Y.ToString();
					}
          else if (field.GeometryDef.GeometryType == esriGeometryType.esriGeometryPolyline ||
                   field.GeometryDef.GeometryType == esriGeometryType.esriGeometryPolygon)
          {
            StringBuilder stringBuffer = new StringBuilder();
            var pointCollection = row.get_Value(fieldIndex) as IPointCollection;
            for (int pointIndex = 0; pointIndex < pointCollection.PointCount; pointIndex++)
            {
              IPoint point = pointCollection.get_Point(pointIndex);

              if (pointIndex > 0)
                stringBuffer.Append(",");
              stringBuffer.Append("{");
              stringBuffer.Append(point.X);
              stringBuffer.Append(",");
              stringBuffer.Append(point.Y);
              stringBuffer.Append(",");
              stringBuffer.Append(point.Z);
              stringBuffer.Append(",");
              stringBuffer.Append(point.M);
              stringBuffer.Append("}");
            }
            textOut += stringBuffer.ToString();
          }
          else
          {
            textOut += "Shape";
          }
				}
        // Handle the Locations field for polyline and polygon barrier classes
        else if (field.Name == "Locations" && field.Type == esriFieldType.esriFieldTypeBlob)
        {
          StringBuilder stringBuffer = new StringBuilder();

          
          // get the location ranges out of the barrier feature
            var naLocRangesObject = row as INALocationRangesObject;
            if (naLocRangesObject == null) // Not a location ranges object
              textOut += row.get_Value(fieldIndex).ToString();

            var naLocRanges = naLocRangesObject.NALocationRanges;
            if (naLocRanges == null) // does not have any location ranges
              textOut += row.get_Value(fieldIndex).ToString();

            // add all of the junctions included in the barrier to the Junctions dataGrid
            stringBuffer.Append("{Junctions:{");
            long junctionCount = naLocRanges.JunctionCount;
            int junctionEID = -1;
            for (int i = 0; i < junctionCount; i++)
            {
              naLocRanges.QueryJunction(i, ref junctionEID);

              if (i > 0)
                stringBuffer.Append(",");
              stringBuffer.Append("{");
              stringBuffer.Append(junctionEID);
              stringBuffer.Append("}");
            }
            stringBuffer.Append("}");

            // add all of the edges included in the barrier to the Edges dataGrid
            stringBuffer.Append(",EdgeRanges:{");
            long edgeRangeCount = naLocRanges.EdgeRangeCount;
            int edgeEID = -1;
            double fromPosition, toPosition;
            fromPosition = toPosition = -1;
            esriNetworkEdgeDirection edgeDirection = esriNetworkEdgeDirection.esriNEDNone;
            for (int i = 0; i < edgeRangeCount; i++)
            {
              naLocRanges.QueryEdgeRange(i, ref edgeEID, ref edgeDirection, ref fromPosition, ref toPosition);

              string directionValue = "";
              if (edgeDirection == esriNetworkEdgeDirection.esriNEDAlongDigitized) directionValue = "Along Digitized";
              else if (edgeDirection == esriNetworkEdgeDirection.esriNEDAgainstDigitized) directionValue = "Against Digitized";

              if (i > 0)
                stringBuffer.Append(",");
              stringBuffer.Append("{");
              stringBuffer.Append(edgeEID);
              stringBuffer.Append(",");
              stringBuffer.Append(directionValue);
              stringBuffer.Append(",");
              stringBuffer.Append(fromPosition);
              stringBuffer.Append(",");
              stringBuffer.Append(toPosition);
              stringBuffer.Append("}");
            }
            stringBuffer.Append("}");

            textOut += stringBuffer.ToString();
        }
				else
				{
					textOut += row.get_Value(fieldIndex).ToString();
				}
			}
			return textOut;
		}
	}
}
