'Copyright 2016 Esri

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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Display
Imports GlobeGraphicsToolbar
Namespace GlobeGraphicsToolbar
	Public Class StyleGallery
		Private _styleForm As StyleGalleryForm

		Public Sub New()
			_styleForm = New StyleGalleryForm()

			InitializeUI()
		End Sub

		Private Sub InitializeUI()
			_styleForm.AutoSize = True
			_styleForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
			_styleForm.MaximizeBox = False
			_styleForm.MinimizeBox = False
			_styleForm.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
			_styleForm.Text = "Style Gallery"
		End Sub

		Public Function IsStyleSelected() As Boolean
			Return _styleForm.ShowDialog() = DialogResult.OK
		End Function

		Public ReadOnly Property StyleGalleryItem() As IStyleGalleryItem
			Get
				Return _styleForm.StyleGalleryItem
			End Get
		End Property
	End Class
End Namespace