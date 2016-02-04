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
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections.Specialized;
using GeocodeRoute_WebService.WebService;

namespace Route_WebService
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Route_WebServiceClass : System.Windows.Forms.Form
	{
		#region Window Controls Declaration

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ComboBox cboNALayers;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkReturnMap;
		private System.Windows.Forms.Button cmdSolve;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.CheckBox chkReturnDirections;
		private System.Windows.Forms.TabControl tabCtrlOutput;
		private System.Windows.Forms.TabPage tabReturnDirections;
		private System.Windows.Forms.TabPage tabReturnMap;
		private System.Windows.Forms.TreeView treeViewDirections;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtStartStreetAddress;
		private System.Windows.Forms.Label lblStartCity;
		private System.Windows.Forms.TextBox txtStartCity;
		private System.Windows.Forms.Label lblStartZipCode;
		private System.Windows.Forms.TextBox txtStartZipCode;
		private System.Windows.Forms.Label lblStartStreetAddress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtStartState;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox txtEndState;
		private System.Windows.Forms.TextBox txtEndZipCode;
		private System.Windows.Forms.TextBox txtEndCity;
		private System.Windows.Forms.TextBox txtEndStreetAddress;

		private SanFrancisco_NAServer m_naServer;

		#endregion

		public Route_WebServiceClass()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdSolve = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.chkReturnMap = new System.Windows.Forms.CheckBox();
			this.cboNALayers = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.chkReturnDirections = new System.Windows.Forms.CheckBox();
			this.tabCtrlOutput = new System.Windows.Forms.TabControl();
			this.tabReturnMap = new System.Windows.Forms.TabPage();
			this.tabReturnDirections = new System.Windows.Forms.TabPage();
			this.treeViewDirections = new System.Windows.Forms.TreeView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtEndState = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtEndZipCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtEndCity = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtEndStreetAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtStartState = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtStartZipCode = new System.Windows.Forms.TextBox();
			this.lblStartZipCode = new System.Windows.Forms.Label();
			this.txtStartCity = new System.Windows.Forms.TextBox();
			this.lblStartCity = new System.Windows.Forms.Label();
			this.txtStartStreetAddress = new System.Windows.Forms.TextBox();
			this.lblStartStreetAddress = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.tabCtrlOutput.SuspendLayout();
			this.tabReturnMap.SuspendLayout();
			this.tabReturnDirections.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdSolve
			// 
			this.cmdSolve.Location = new System.Drawing.Point(120, 384);
			this.cmdSolve.Name = "cmdSolve";
			this.cmdSolve.Size = new System.Drawing.Size(200, 32);
			this.cmdSolve.TabIndex = 29;
			this.cmdSolve.Text = "Find Route";
			this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.White;
			this.pictureBox.Location = new System.Drawing.Point(8, 16);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(448, 360);
			this.pictureBox.TabIndex = 20;
			this.pictureBox.TabStop = false;
			// 
			// chkReturnMap
			// 
			this.chkReturnMap.Checked = true;
			this.chkReturnMap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnMap.Location = new System.Drawing.Point(8, 64);
			this.chkReturnMap.Name = "chkReturnMap";
			this.chkReturnMap.Size = new System.Drawing.Size(96, 16);
			this.chkReturnMap.TabIndex = 7;
			this.chkReturnMap.Text = "Return Map";
			// 
			// cboNALayers
			// 
			this.cboNALayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboNALayers.Location = new System.Drawing.Point(112, 24);
			this.cboNALayers.Name = "cboNALayers";
			this.cboNALayers.Size = new System.Drawing.Size(248, 21);
			this.cboNALayers.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 16);
			this.label8.TabIndex = 71;
			this.label8.Text = "NALayer Name";
			// 
			// chkReturnDirections
			// 
			this.chkReturnDirections.Checked = true;
			this.chkReturnDirections.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnDirections.Location = new System.Drawing.Point(160, 64);
			this.chkReturnDirections.Name = "chkReturnDirections";
			this.chkReturnDirections.Size = new System.Drawing.Size(160, 16);
			this.chkReturnDirections.TabIndex = 19;
			this.chkReturnDirections.Text = "Generate Directions";
			// 
			// tabCtrlOutput
			// 
			this.tabCtrlOutput.Controls.Add(this.tabReturnMap);
			this.tabCtrlOutput.Controls.Add(this.tabReturnDirections);
			this.tabCtrlOutput.Enabled = false;
			this.tabCtrlOutput.Location = new System.Drawing.Point(440, 32);
			this.tabCtrlOutput.Name = "tabCtrlOutput";
			this.tabCtrlOutput.SelectedIndex = 0;
			this.tabCtrlOutput.Size = new System.Drawing.Size(472, 408);
			this.tabCtrlOutput.TabIndex = 30;
			// 
			// tabReturnMap
			// 
			this.tabReturnMap.Controls.Add(this.pictureBox);
			this.tabReturnMap.Location = new System.Drawing.Point(4, 22);
			this.tabReturnMap.Name = "tabReturnMap";
			this.tabReturnMap.Size = new System.Drawing.Size(464, 382);
			this.tabReturnMap.TabIndex = 0;
			this.tabReturnMap.Text = "Map";
			// 
			// tabReturnDirections
			// 
			this.tabReturnDirections.Controls.Add(this.treeViewDirections);
			this.tabReturnDirections.Location = new System.Drawing.Point(4, 22);
			this.tabReturnDirections.Name = "tabReturnDirections";
			this.tabReturnDirections.Size = new System.Drawing.Size(464, 382);
			this.tabReturnDirections.TabIndex = 1;
			this.tabReturnDirections.Text = "Directions";
			// 
			// treeViewDirections
			// 
			this.treeViewDirections.Location = new System.Drawing.Point(0, 8);
			this.treeViewDirections.Name = "treeViewDirections";
			this.treeViewDirections.Size = new System.Drawing.Size(448, 368);
			this.treeViewDirections.TabIndex = 69;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtEndState);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtEndZipCode);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtEndCity);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtEndStreetAddress);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.txtStartState);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtStartZipCode);
			this.groupBox1.Controls.Add(this.lblStartZipCode);
			this.groupBox1.Controls.Add(this.txtStartCity);
			this.groupBox1.Controls.Add(this.lblStartCity);
			this.groupBox1.Controls.Add(this.txtStartStreetAddress);
			this.groupBox1.Controls.Add(this.lblStartStreetAddress);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Location = new System.Drawing.Point(24, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 232);
			this.groupBox1.TabIndex = 72;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Enter addresses to get map and directions";
			// 
			// txtEndState
			// 
			this.txtEndState.Location = new System.Drawing.Point(216, 184);
			this.txtEndState.Name = "txtEndState";
			this.txtEndState.Size = new System.Drawing.Size(72, 20);
			this.txtEndState.TabIndex = 15;
			this.txtEndState.Text = "California";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(176, 184);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "State";
			// 
			// txtEndZipCode
			// 
			this.txtEndZipCode.Location = new System.Drawing.Point(344, 184);
			this.txtEndZipCode.Name = "txtEndZipCode";
			this.txtEndZipCode.Size = new System.Drawing.Size(48, 20);
			this.txtEndZipCode.TabIndex = 17;
			this.txtEndZipCode.Text = "94123";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(296, 184);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 16;
			this.label3.Text = "Zip Code";
			// 
			// txtEndCity
			// 
			this.txtEndCity.Location = new System.Drawing.Point(56, 184);
			this.txtEndCity.Name = "txtEndCity";
			this.txtEndCity.Size = new System.Drawing.Size(112, 20);
			this.txtEndCity.TabIndex = 13;
			this.txtEndCity.Text = "San Francisco";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 184);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 12;
			this.label4.Text = "City";
			// 
			// txtEndStreetAddress
			// 
			this.txtEndStreetAddress.Location = new System.Drawing.Point(104, 152);
			this.txtEndStreetAddress.Name = "txtEndStreetAddress";
			this.txtEndStreetAddress.Size = new System.Drawing.Size(288, 20);
			this.txtEndStreetAddress.TabIndex = 11;
			this.txtEndStreetAddress.Text = "171 Capra Way";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Street Address";
			// 
			// groupBox3
			// 
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(8, 128);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(392, 88);
			this.groupBox3.TabIndex = 18;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Arriving at:";
			// 
			// txtStartState
			// 
			this.txtStartState.Location = new System.Drawing.Point(216, 88);
			this.txtStartState.Name = "txtStartState";
			this.txtStartState.Size = new System.Drawing.Size(72, 20);
			this.txtStartState.TabIndex = 6;
			this.txtStartState.Text = "California";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(176, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "State";
			// 
			// txtStartZipCode
			// 
			this.txtStartZipCode.Location = new System.Drawing.Point(344, 88);
			this.txtStartZipCode.Name = "txtStartZipCode";
			this.txtStartZipCode.Size = new System.Drawing.Size(48, 20);
			this.txtStartZipCode.TabIndex = 8;
			this.txtStartZipCode.Text = "94110";
			// 
			// lblStartZipCode
			// 
			this.lblStartZipCode.Location = new System.Drawing.Point(296, 90);
			this.lblStartZipCode.Name = "lblStartZipCode";
			this.lblStartZipCode.Size = new System.Drawing.Size(56, 16);
			this.lblStartZipCode.TabIndex = 7;
			this.lblStartZipCode.Text = "Zip Code";
			// 
			// txtStartCity
			// 
			this.txtStartCity.Location = new System.Drawing.Point(56, 88);
			this.txtStartCity.Name = "txtStartCity";
			this.txtStartCity.Size = new System.Drawing.Size(112, 20);
			this.txtStartCity.TabIndex = 4;
			this.txtStartCity.Text = "San Francisco";
			// 
			// lblStartCity
			// 
			this.lblStartCity.Location = new System.Drawing.Point(24, 90);
			this.lblStartCity.Name = "lblStartCity";
			this.lblStartCity.Size = new System.Drawing.Size(64, 16);
			this.lblStartCity.TabIndex = 3;
			this.lblStartCity.Text = "City";
			// 
			// txtStartStreetAddress
			// 
			this.txtStartStreetAddress.Location = new System.Drawing.Point(104, 56);
			this.txtStartStreetAddress.Name = "txtStartStreetAddress";
			this.txtStartStreetAddress.Size = new System.Drawing.Size(288, 20);
			this.txtStartStreetAddress.TabIndex = 2;
			this.txtStartStreetAddress.Text = "113 Cleridge Street";
			// 
			// lblStartStreetAddress
			// 
			this.lblStartStreetAddress.Location = new System.Drawing.Point(24, 58);
			this.lblStartStreetAddress.Name = "lblStartStreetAddress";
			this.lblStartStreetAddress.Size = new System.Drawing.Size(96, 16);
			this.lblStartStreetAddress.TabIndex = 1;
			this.lblStartStreetAddress.Text = "Street Address";
			// 
			// groupBox2
			// 
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(392, 88);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Starting from:";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.cboNALayers);
			this.groupBox4.Controls.Add(this.chkReturnDirections);
			this.groupBox4.Controls.Add(this.chkReturnMap);
			this.groupBox4.Location = new System.Drawing.Point(24, 272);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(408, 88);
			this.groupBox4.TabIndex = 72;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Solve Parameters";
			// 
			// Route_WebServiceClass
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(936, 454);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tabCtrlOutput);
			this.Controls.Add(this.cmdSolve);
			this.Controls.Add(this.groupBox4);
			this.Name = "Route_WebServiceClass";
			this.Text = "NAServer - Geocode Route Web Service";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.tabCtrlOutput.ResumeLayout(false);
			this.tabReturnMap.ResumeLayout(false);
			this.tabReturnDirections.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Route_WebServiceClass());
		}

		/// <summary>
		/// This function
		///     - sets the server and solver parameters
		///     - populates the stops NALocations
		///     - gets and displays the server results (map, directions)
		/// </summary>
		private void cmdSolve_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				// Get SolverParams
				NAServerSolverParams solverParams = m_naServer.GetSolverParameters(cboNALayers.Text) as NAServerSolverParams;

				// Set Solver parameters
				SetServerSolverParams(solverParams);

				// Load Locations
				LoadLocations(solverParams);

				//Solve the Route
				NAServerSolverResults solverResults;
				solverResults = m_naServer.Solve(solverParams);

				//Get NAServer results in the tab controls
				OutputResults(solverParams, solverResults);

			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// This function
		///     - gets all route network analysis layers
		///     - selects the first route network analysis layer
		///     - sets all controls for this route network analysis layer
		/// </summary>
		private void GetNetworkAnalysisLayers()
		{

			this.Cursor = Cursors.WaitCursor;

			try
			{

				//Get Route NA layer names
				cboNALayers.Items.Clear();
				string[] naLayers = m_naServer.GetNALayerNames(esriNAServerLayerType.esriNAServerRouteLayer);
				for (int i = 0; i < naLayers.Length; i++)
				{
					cboNALayers.Items.Add(naLayers[i]);
				}

				// Select the first NA Layer name
				if (cboNALayers.Items.Count > 0)
					cboNALayers.SelectedIndex = 0;
				else
					MessageBox.Show("There is no Network Analyst layer associated with this MapServer object!", "NAServer - Route Sample", System.Windows.Forms.MessageBoxButtons.OK);

			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Set server solver paramaters  (ReturnMap, SnapTolerance, etc.)
		/// </summary>
		private void SetServerSolverParams(NAServerSolverParams solverParams)
		{
			solverParams.ReturnMap = chkReturnMap.Checked;
			solverParams.ImageDescription.ImageDisplay.ImageWidth = pictureBox.Width;
			solverParams.ImageDescription.ImageDisplay.ImageHeight = pictureBox.Height;


			NAServerRouteParams routeParams = solverParams as NAServerRouteParams;
			if (routeParams != null)
				routeParams.ReturnDirections = chkReturnDirections.Checked;
		}

		/// <summary>
		/// Load form
		/// </summary> 
		private void Form1_Load(object sender, System.EventArgs e)
		{
			try
			{
				ConnectToWebService();
				GetNetworkAnalysisLayers();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}
		}

		/// <summary>
		/// Get NAServer Object from the web service
		/// </summary>		
		private void ConnectToWebService()
		{
			m_naServer = null;

			// Get NAServer
			m_naServer = new SanFrancisco_NAServer();
			if (m_naServer != null)
				return;

			throw (new System.Exception("Could not find the web service."));

		}

		/// <summary>
		/// This function shows how to populate stop locations using an array of PropertySets
		/// </summary>
		private void LoadLocations(NAServerSolverParams solverParams)
		{
			// Geocode Addresses
			PropertySet[] propSets = new PropertySet[2];
			propSets[0] = GeocodeAddress(txtStartStreetAddress.Text, txtStartCity.Text, txtStartState.Text, txtStartZipCode.Text);
			propSets[1] = GeocodeAddress(txtEndStreetAddress.Text, txtEndCity.Text, txtEndState.Text, txtEndZipCode.Text);

			NAServerPropertySets StopsPropSets = new NAServerPropertySets();
			StopsPropSets.PropertySets = propSets;

			NAServerRouteParams routeParams = solverParams as NAServerRouteParams;
			routeParams.Stops = StopsPropSets;
		}

		/// <summary>
		/// Geocode an address based on the street name, city, state, and zip code
		/// Throws and exception and returns null if the address was unmatched.
		/// </summary> 
		private PropertySet GeocodeAddress(string StreetAddress, string City, string State, string ZipCode)
		{
			PropertySet propSet = null;

			try
			{
				SanFranciscoLocator_GeocodeServer gc = new SanFranciscoLocator_GeocodeServer();
				PropertySet pAddressProperties = new PropertySet();

				Fields pAddressFields;
				Field pField;

				PropertySetProperty[] propSetProperty = new PropertySetProperty[4];
				pAddressFields = gc.GetAddressFields();
				for (int i = 0; i < pAddressFields.FieldArray.GetLength(0); i++)
				{
					pField = pAddressFields.FieldArray[i];

					if (pField.Name.ToUpper() == "STREET")
						propSetProperty[0] = CreatePropertySetProperty(pField.AliasName, StreetAddress) as PropertySetProperty;

					if (pField.Name.ToUpper() == "CITY")
						propSetProperty[1] = CreatePropertySetProperty(pField.AliasName, City) as PropertySetProperty;

					if (pField.Name.ToUpper() == "STATE")
						propSetProperty[2] = CreatePropertySetProperty(pField.AliasName, State) as PropertySetProperty;

					if (pField.Name.ToUpper() == "ZIP")
						propSetProperty[3] = CreatePropertySetProperty(pField.AliasName, ZipCode) as PropertySetProperty;

				}

				pAddressProperties.PropertyArray = propSetProperty;

				// find the matching address	
				propSet = gc.GeocodeAddress(pAddressProperties, null);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}

			// Throw and error if the geocoded address is "Unmatched"
			if ((propSet != null) && (propSet.PropertyArray[1].Value.ToString() == "U"))
				throw (new System.Exception("Could not geocode [" + StreetAddress + "]"));

			return propSet;
		}

		/// <summary>
		/// CreatePropertySetProperty
		/// </summary> 
		private PropertySetProperty CreatePropertySetProperty(string key, object value)
		{
			PropertySetProperty propSetProperty = new PropertySetProperty();
			propSetProperty.Key = key;
			propSetProperty.Value = value;
			return propSetProperty;
		}

		/// <summary>
		/// Output Results map, Directions
		/// </summary>
		private void OutputResults(NAServerSolverParams solverParams, NAServerSolverResults solverResults)
		{
			string messagesSolverResults = "";

			// Output Solve messages
			GPMessages gpMessages = solverResults.SolveMessages;
			GPMessage[] arrGPMessage = gpMessages.GPMessages1;
			if (arrGPMessage != null)
			{
				for (int i = 0; i < arrGPMessage.GetLength(0); i++)
				{
					GPMessage gpMessage = arrGPMessage[i];
					messagesSolverResults += "\n" + gpMessage.MessageDesc;
				}
			}

			// Output the total impedance of each route
			NAServerRouteResults routeSolverResults = solverResults as NAServerRouteResults;

			//Output Map
			pictureBox.Image = null;
			if (solverParams.ReturnMap)
			{
				pictureBox.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(solverResults.MapImage.ImageData));
			}
			pictureBox.Refresh();

			if (routeSolverResults != null)
				OutputDirections(routeSolverResults.Directions); // Return Directions if generated

			tabCtrlOutput.Enabled = true;
		}

		/// <summary>
		/// Output Directions if a TreeView control
		/// </summary> 
		private void OutputDirections(NAStreetDirections[] serverDirections)
		{
			if (serverDirections == null)
			{
				treeViewDirections.Nodes.Clear();
				TreeNode newNode = new TreeNode("Directions not generated");
				treeViewDirections.Nodes.Add(newNode);
				return;
			}

			// Suppress repainting the TreeView until all the objects have been created.
			treeViewDirections.BeginUpdate();

			// Clear the TreeView each time the method is called.
			treeViewDirections.Nodes.Clear();

			for (int i = serverDirections.GetLowerBound(0); i <= serverDirections.GetUpperBound(0); i++)
			{
				// get Directions from the ith route
				NAStreetDirections directions;
				directions = serverDirections[i];

				// get Summary (Total Distance and Time)
				NAStreetDirection direction = directions.Summary;
				string totallength = null, totaltime = null;
				string[] SummaryStrings = direction.Strings;
				for (int k = SummaryStrings.GetLowerBound(0); k < SummaryStrings.GetUpperBound(0); k++)
				{
					if (direction.StringTypes[k] == esriDirectionsStringType.esriDSTLength)
						totallength = SummaryStrings[k];
					if (direction.StringTypes[k] == esriDirectionsStringType.esriDSTTime)
						totaltime = SummaryStrings[k];
				}

				// Add a Top a Node with the Route number and Total Distance and Total Time
				TreeNode newNode = new TreeNode("Directions for Route [" + (i + 1) + "] - Total Distance: " + totallength + " Total Time: " + totaltime);
				treeViewDirections.Nodes.Add(newNode);

				// Then add a node for each step-by-step directions
				NAStreetDirection[] StreetDirections = directions.Directions;
				for (int directionIndex = StreetDirections.GetLowerBound(0); directionIndex <= StreetDirections.GetUpperBound(0); directionIndex++)
				{
					NAStreetDirection streetDirection = StreetDirections[directionIndex];
					string[] StringStreetDirection = streetDirection.Strings;
					for (int stringIndex = StringStreetDirection.GetLowerBound(0); stringIndex <= StringStreetDirection.GetUpperBound(0); stringIndex++)
					{
						if (streetDirection.StringTypes[stringIndex] == esriDirectionsStringType.esriDSTGeneral ||
							streetDirection.StringTypes[stringIndex] == esriDirectionsStringType.esriDSTDepart ||
							streetDirection.StringTypes[stringIndex] == esriDirectionsStringType.esriDSTArrive)
							treeViewDirections.Nodes[i].Nodes.Add(new TreeNode(StringStreetDirection[stringIndex]));
					}
				}
			}

			// Check if Directions have been generated
			if (serverDirections.Length == 0)
			{
				TreeNode newNode = new TreeNode("Directions not generated");
				treeViewDirections.Nodes.Add(newNode);
			}

			// Begin repainting the TreeView.
			treeViewDirections.ExpandAll();
			treeViewDirections.EndUpdate();
		}
	}

}