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