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
using System.Windows.Forms;

namespace GlobeDynamicObjectTracking
{
  /// <summary>
  /// This dialog allow user to select the required type of tracking
  /// </summary>
  public partial class TrackSelectionDlg : Form
  {
    public TrackSelectionDlg()
    {
      InitializeComponent();
    }

    /// <summary>
    /// OK button click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;

    }

    /// <summary>
    /// Cancel button click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    /// <summary>
    /// Returns the selected mode of tracking (above the element or behind the element)
    /// </summary>
    public bool UseOrthoTrackingMode
    {
      get { return chkOrthogonal.Checked;  }
    }
  }
}