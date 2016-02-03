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