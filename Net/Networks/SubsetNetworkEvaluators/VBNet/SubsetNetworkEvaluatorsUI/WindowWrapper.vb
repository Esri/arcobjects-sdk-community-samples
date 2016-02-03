Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms

Namespace SubsetNetworkEvaluatorsUI
	Public Class WindowWrapper : Implements System.Windows.Forms.IWin32Window
		Public Sub New(ByVal windowHandle As IntPtr)
			_hwnd = windowHandle
		End Sub

		Public ReadOnly Property Handle() As IntPtr Implements System.Windows.Forms.IWin32Window.Handle
			Get
				Return _hwnd
			End Get
		End Property

		Private _hwnd As IntPtr
	End Class
End Namespace
