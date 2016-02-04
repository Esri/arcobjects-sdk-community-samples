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
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Display


Namespace GlobeGraphicsToolbar
	Partial Public Class StyleGalleryForm
		Inherits Form
		Private _styleGalleryItem As IStyleGalleryItem = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub axSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles axSymbologyControl1.OnItemSelected
			_styleGalleryItem = TryCast(e.styleGalleryItem, IStyleGalleryItem)
		End Sub

		Public ReadOnly Property StyleGalleryItem() As IStyleGalleryItem
			Get
				Return _styleGalleryItem
			End Get
		End Property

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Hide()
		End Sub


	End Class
End Namespace
