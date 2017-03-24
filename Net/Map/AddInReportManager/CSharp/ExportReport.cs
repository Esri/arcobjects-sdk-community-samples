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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.IO;

namespace CustomReport_CS
{
    public partial class ExportReport : Form
    {
        #region Member Variables
        String m_FileLocation;
        Dictionary<Int32, String> reports;
        #endregion

        public ExportReport()
        {
            InitializeComponent();
        }

        private void ExportReport_Load(object sender, EventArgs e)
        {
            // store the templates information
            reports = new Dictionary<Int32,String>();
            m_FileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            m_FileLocation = Path.Combine(m_FileLocation, @"ArcGIS\data\California\Report Templates");
            if (!System.IO.File.Exists(m_FileLocation))
                throw new Exception(String.Format("Fix code to point to your sample data: {0} was not found",
                    m_FileLocation));


           String[] filePaths = Directory.GetFiles(m_FileLocation, " *.rlf");
            XmlDocument doc = new XmlDocument();
            try
            {
                for (Int32 c = 0; c <= (filePaths.Count() - 1); c += 1)
                {
                    String fileLocation = filePaths[c];
                    doc.Load(fileLocation);
                    String title = this.ReadRTFElement(doc, "/Report", "Name");
                    reports.Add(c, fileLocation);
                    this.lstReports.Items.Add(title);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                doc = null;
            }
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SaveFileDialog savDialog = new SaveFileDialog();
            savDialog.Filter = "PDF(*.pdf)\"|*.pdf";
            savDialog.AddExtension = true;
            DialogResult result;
            result = savDialog.ShowDialog();
            String savFile = savDialog.FileName;

            if (result == DialogResult.OK)
            {
                IMxDocument mxDoc;
                IReportDataSource rDS = new Report();
                IReportTemplate rwTemplate;
                IReportEngine rwEngine;
                Process openPDFProcess = new Process();
                try
                {
                    mxDoc = (IMxDocument)ArcMap.Application.Document;

                    for (int i = 0; i < mxDoc.FocusMap.LayerCount; i += 1)
                    {
                        if (mxDoc.FocusMap.get_Layer(i).Name == "Counties")
                        {
                            rDS.Layer = mxDoc.FocusMap.get_Layer(i);
                            break;
                        }
                    }
                    rwTemplate = (IReportTemplate)rDS;
                    rwTemplate.LoadReportTemplate(m_FileLocation);
                    rwEngine = (IReportEngine)rDS;
                    rwEngine.RunReport(null);
                    String pdfReport = (savFile);
                    rwEngine.ExportReport(pdfReport, "1", esriReportExportType.esriReportExportPDF);

                    // launch PDF
                    openPDFProcess.StartInfo.UseShellExecute = true;
                    openPDFProcess.StartInfo.CreateNoWindow = true;
                    openPDFProcess.StartInfo.FileName = pdfReport;
                    openPDFProcess.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    mxDoc = null;
                    rDS = null;
                    rwTemplate = null;
                    rwEngine = null;
                    openPDFProcess = null;
                }
            }
            else
            {
                savDialog.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lstReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_FileLocation = reports[lstReports.SelectedIndex];
            this.txtReportInformation.Clear();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(m_FileLocation);
                // Data source
                txtReportInformation.Text = "DataSource:" + Environment.NewLine +
                                            ReadRTFElement(doc, "/Report/DataSource/Workspace", "PathName");
                // Data name
                txtReportInformation.Text = txtReportInformation.Text + Environment.NewLine + "Name:" + Environment.NewLine +
                                            ReadRTFElement(doc, "/Report/DataSource", "Name");
                // Fields
                txtReportInformation.Text = txtReportInformation.Text + Environment.NewLine + "Fields:" + Environment.NewLine +
                                            ReadRTFElement(doc, "/Report/ReportFields/Field", "Name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                doc = null;
            }
        }

        private String ReadRTFElement(XmlDocument doc, String tagLocation, String elementName)
        {
           
            String retValue = "";
            // Get and display all the book titles.
            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.SelectNodes(tagLocation);
            try
            {
                foreach (XmlNode title in elemList)
                {
                    if (retValue == "")
                    {
                        retValue = title.Attributes[elementName].Value + Environment.NewLine;
                    }
                    else
                    {
                        retValue = retValue + title.Attributes[elementName].Value + Environment.NewLine;
                    }
                }
                return retValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                root = null;
                elemList = null;
            }
        }
    }
}
