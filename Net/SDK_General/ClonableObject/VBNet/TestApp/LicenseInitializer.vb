Imports ESRI.ArcGIS
Imports System

Partial Friend Class LicenseInitializer

  Public Sub New()

  End Sub

  Private Sub BindingArcGISRuntime(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResolveBindingEvent
    '
    ' TODO: Modify ArcGIS runtime binding code as needed; for example, 
    ' the list of products and their binding preference order.
    '
    Dim supportedRuntimes() As ProductCode = New ProductCode() {ProductCode.Engine, ProductCode.Desktop}
    For Each c As ProductCode In supportedRuntimes
      If (RuntimeManager.Bind(c)) Then Return
    Next

    '
    ' TODO: Modify the code below on how to handle bind failure
    '

    ' Failed to bind, announce and force exit
    Console.WriteLine("ArcGIS runtime binding failed. Application will shut down.")
    End
  End Sub

End Class