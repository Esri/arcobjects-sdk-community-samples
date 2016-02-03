using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SubsetNetworkEvaluatorsUI
{
	public class WindowWrapper : System.Windows.Forms.IWin32Window
	{
		public WindowWrapper(IntPtr handle)
		{
			_hwnd = handle;
		}

		public IntPtr Handle
		{
			get { return _hwnd; }
		}

		private IntPtr _hwnd;
	}
}
