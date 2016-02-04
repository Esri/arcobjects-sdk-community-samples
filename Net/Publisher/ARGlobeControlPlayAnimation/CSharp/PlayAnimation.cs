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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PlayAnimation
{
		public partial class PlayAnimation : Form
        {
				public PlayAnimation()
                {                
                InitializeComponent();
                }

				private void btnLoad_Click(object sender, EventArgs e)
				{
				openFileDialog1.Title = "Select Published Map Document";
				openFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf";
				openFileDialog1.ShowDialog();

				// Exit if no map document is selected
				string  sFilePath = "";
				sFilePath = openFileDialog1.FileName;
				
						if (sFilePath == null) 
						{
								return;
						}

						// Load the specified pmf
						if (axArcReaderGlobeControl1.CheckDocument(sFilePath)) 
						{
								axArcReaderGlobeControl1.LoadDocument(sFilePath, "");
						}
						else 
						{
								System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
								return;
						}

						if (axArcReaderGlobeControl1.ARGlobe.AnimationCount != 0)
						{
								//Enable Controls
								enableCommands(true);

								//Populate combo with animations, clearing any existing animations listed previously
								cboAnimations.Items.Clear();
								for (int i = 0; i < axArcReaderGlobeControl1.ARGlobe.AnimationCount; i++)
								{
										cboAnimations.Items.Add(axArcReaderGlobeControl1.ARGlobe.get_AnimationName(i));
										cboAnimations.SelectedIndex=0;
								}
						}
						else
						{
								System.Windows.Forms.MessageBox.Show("This sample requires you load a PMF that contains animations");
								//Disable Controls
								enableCommands(false);
						}

				}

				private void btnPlay_Click(object sender, EventArgs e)
				{
						//Play animation showing or hiding window depending if checkbox is checked
						axArcReaderGlobeControl1.ARGlobe.PlayAnimation(axArcReaderGlobeControl1.ARGlobe.get_AnimationName(cboAnimations.SelectedIndex));
				}

				private void chkShowWindow_CheckedChanged(object sender, EventArgs e)
				{
						//Show or hide window if checkbox is changed
						axArcReaderGlobeControl1.ShowARGlobeWindow(ESRI.ArcGIS.PublisherControls.esriARGlobeWindows.esriARGlobeWindowsAnimation, chkShowWindow.Checked, axArcReaderGlobeControl1.ARGlobe.get_AnimationName(cboAnimations.SelectedIndex));
				}

				private void cboAnimations_SelectedIndexChanged(object sender, EventArgs e)
				{
						//Updates the combo box of the animation window if its present
						axArcReaderGlobeControl1.ShowARGlobeWindow(ESRI.ArcGIS.PublisherControls.esriARGlobeWindows.esriARGlobeWindowsAnimation, chkShowWindow.Checked, axArcReaderGlobeControl1.ARGlobe.get_AnimationName(cboAnimations.SelectedIndex));
				}

				private void PlayAnimation_Load(object sender, EventArgs e)
				{
						//Disable commands
						enableCommands(false);
				}

				private void enableCommands(bool enable)
				{
						cboAnimations.Enabled = enable;
						chkShowWindow.Enabled = enable;
						btnPlay.Enabled = enable;
				}
								
  }
}