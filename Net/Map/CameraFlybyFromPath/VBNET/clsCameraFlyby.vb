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
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.GlobeCore
Imports cameraflybyfrompath

Namespace CameraFlybyFromPath
  Public Class clsCameraFlyby : Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()
    End Sub

    Protected Overrides Sub OnClick()
      Dim formFlyby As frmCameraPath = New frmCameraPath()
      formFlyby.SetVariables(ArcGlobe.Globe)
      formFlyby.Show()
    End Sub

    Protected Overrides Sub OnUpdate()
      Enabled = Not ArcGlobe.Application Is Nothing
    End Sub
  End Class

End Namespace