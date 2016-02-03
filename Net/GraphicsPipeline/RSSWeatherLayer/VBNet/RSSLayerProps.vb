Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices


  <Guid("12D0AE46-D542-43f7-8A53-5B7FCA4AA111"), ComVisible(True), ProgId("RSSLayerProps"), ClassInterface(ClassInterfaceType.None)> _
  Public Partial Class RSSLayerProps : Inherits PropertyPage
	Public Sub New()
	  InitializeComponent()
	End Sub

	Protected Overrides Sub OnPageApply()
	  MyBase.OnPageApply()

	  Dim propSheet As PropertySheet = TryCast(Objects(0), PropertySheet)

	  Dim layer As RSSWeatherLayerClass = propSheet.RSSWatherLayer
	  If Nothing Is layer Then
		Return
	  End If

	End Sub

	Protected Overrides Sub OnPageActivate(ByVal hWndParent As IntPtr, ByVal Rect As Rectangle, ByVal bModal As Boolean)
	  MyBase.OnPageActivate(hWndParent, Rect, bModal)

	  Dim propSheet As PropertySheet = TryCast(Objects(0), PropertySheet)

	  Dim layer As RSSWeatherLayerClass = propSheet.RSSWatherLayer
	  If Nothing Is layer Then
		Return
	  End If

	  'get the cityNames from the layer
	  Dim cityNames As String() = layer.GetCityNames()

	  'clear the listbox
			listBoxCityNames.Items.Clear()
	  listBoxCityNames.Items.AddRange(cityNames)
	End Sub
  End Class
