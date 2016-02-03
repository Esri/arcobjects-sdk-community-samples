Option Explicit On
Option Strict On

Imports ESRI.ArcGIS

Namespace My

  ' The following events are available for MyApplication:
  ' 
  ' Startup: Raised when the application starts, before the startup form is created.
  ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
  ' UnhandledException: Raised if the application encounters an unhandled exception.
  ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
  ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
  Partial Friend Class MyApplication

    Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
      If Not RuntimeManager.Bind(ProductCode.EngineOrDesktop) Then
        MessageBox.Show("Could not bind to a Runtime!", "No ArcGIS Runtime available!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return
      End If

    End Sub
  End Class

End Namespace

