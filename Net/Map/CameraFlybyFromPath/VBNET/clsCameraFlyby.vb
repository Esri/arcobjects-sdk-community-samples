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