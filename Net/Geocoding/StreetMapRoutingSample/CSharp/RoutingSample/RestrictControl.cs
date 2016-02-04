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

namespace RoutingSample
{
	public partial class RestrictControl
	{

	#region Public properties
		// Restriction name
		public string RSName
		{
			get
			{
				return m_chkCheck.Text;
			}

			set
			{
				m_chkCheck.Text = value;
			}
		}

		// Is restriction checked
		public bool RSChecked
		{
			get
			{
				return m_chkCheck.Checked;
			}

			set
			{
				m_chkCheck.Checked = value;
			}
		}

		public enum ERSType: int
		{
			eStrict = 0,
			eRelaxed = 1
		}

		// Restriction Type
		public ERSType RSType
		{
			get
			{
        return (ERSType)m_cmbType.SelectedIndex;
			}

			set
			{
				m_cmbType.SelectedIndex = Convert.ToInt32(value);
			}
		}

		// Restriction Parameter
		public double RSParameter
		{
			get
			{
				if (! m_txtParameter.Enabled)
						return 0.0;

				double nVal = 0;
				try
				{
					nVal = double.Parse(m_txtParameter.Text);
				}
				catch (Exception ex)
				{
					nVal = 0;
				}

				return nVal;
			}

			set
			{
				m_txtParameter.Text = value.ToString();
			}
		}

		// Is Parameter enabled
		public bool RSUseParameter
		{
			set
			{
				if (! value)
						m_txtParameter.Text = "";
				m_txtParameter.Visible = value;
			}
		}

	#endregion

	}

} //end of root namespace