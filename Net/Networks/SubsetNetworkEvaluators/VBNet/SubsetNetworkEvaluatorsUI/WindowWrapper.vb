'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
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
