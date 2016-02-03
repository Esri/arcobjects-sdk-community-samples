Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Runtime.InteropServices

Partial Public Class Gallery

	Public mapGallery As MapCollection
	Public mapView As GlobeView

	Private Sub OnMapClick(ByVal sender As Object, ByVal e As RoutedEventArgs)

		mapView = New GlobeView()
		mapView.SelectedMap = TryCast(MapsListBox.SelectedItem, Map)
		mapView.Show()
	End Sub

  Private Sub Window_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    Application.Current.Shutdown()
  End Sub
End Class
