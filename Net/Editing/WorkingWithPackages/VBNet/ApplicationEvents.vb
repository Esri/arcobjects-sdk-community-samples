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

