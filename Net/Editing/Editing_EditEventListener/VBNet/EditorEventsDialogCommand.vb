Imports System
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem

Public Class EditorEventsDialogCommand
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Private m_dockableWindow As ESRI.ArcGIS.Framework.IDockableWindow

    Public Sub New()
        Dim windowID As UID = New UIDClass
        windowID.Value = "ESRI_Employee_Editing_EditEventListener_EditorEventsDialog"
        m_dockableWindow = My.ArcMap.DockableWindowManager.GetDockableWindow(windowID)
    End Sub

    Protected Overrides Sub OnClick()
        If m_dockableWindow Is Nothing Then
            Return
        End If

        m_dockableWindow.Show((Not m_dockableWindow.IsVisible()))
        Checked = m_dockableWindow.IsVisible()
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = m_dockableWindow IsNot Nothing
        Checked = m_dockableWindow IsNot Nothing And m_dockableWindow.IsVisible()
    End Sub
End Class
