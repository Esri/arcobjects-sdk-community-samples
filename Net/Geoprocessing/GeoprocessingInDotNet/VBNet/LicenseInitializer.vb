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
Imports ESRI.ArcGIS

Partial Friend Class LicenseInitializer

  Public Sub New()
    ' TODO: Uncomment if implicit runtime binding (pre-10 version) is allowed
    ' AllowImplicitRuntimeBinding = True
  End Sub

  Private Sub BindingArcGISRuntime(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResolveBindingEvent
    ' TODO: Add ESRI.ArcGIS.RuntimeManager code to load target ArcGIS runtime here

  End Sub

End Class