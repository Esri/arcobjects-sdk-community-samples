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
using ESRI.ArcGIS.DataSourcesFile;

namespace RoutingSample
{
	public partial class DirectionsForm : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public DirectionsForm() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

		}

	#endregion

	#region Public methods

		// Clears Directions text
		public void Init()
		{
			m_txtDirections.Text = "";
		}

		// Fills text box
		public void Init(ISMDirections objDirections)
		{
			int nCount = objDirections.Count;

			string strText = null;

			// Totals
			strText = objDirections.TotalsText + System.Environment.NewLine + System.Environment.NewLine;

			// Add text for each Direction
			for (int i = 0; i < nCount; i++)
			{
				SMDirItem objItem = null;
				objItem = objDirections.get_Item(i);

				// Direction text
				strText = strText + objItem.Text + System.Environment.NewLine;

				// Drive text (length, time)
				if (objItem.DriveText.Length > 0)
						strText = strText + "    " + objItem.DriveText + System.Environment.NewLine;

				strText = strText + System.Environment.NewLine;
			}

			// Set control text
			m_txtDirections.Text = strText;

			// deselect if was be selected
			m_txtDirections.Select(0, 0);
		}

	#endregion

	}


} //end of root namespace