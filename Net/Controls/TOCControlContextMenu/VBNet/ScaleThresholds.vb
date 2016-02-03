Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.SystemUI

Public NotInheritable Class ScaleThresholds

    Inherits BaseCommand
    Implements ICommandSubType

    Private m_pMapControl As IMapControl3
    Private m_lSubType As Long

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pMapControl = hook
    End Sub

    Public Overrides Sub OnClick()
        Dim pLayer As ILayer
        pLayer = m_pMapControl.CustomProperty
        If (m_lSubType = 1) Then pLayer.MaximumScale = m_pMapControl.MapScale
        If (m_lSubType = 2) Then pLayer.MinimumScale = m_pMapControl.MapScale
        If (m_lSubType = 3) Then
            pLayer.MaximumScale = 0
            pLayer.MinimumScale = 0
        End If
        m_pMapControl.Refresh(esriViewDrawPhase.esriViewGeography)
    End Sub

    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Return 3
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        m_lSubType = SubType
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            If (m_lSubType = 1) Then
                Return "Set Maximum Scale"
            ElseIf (m_lSubType = 2) Then
                Return "Set Minimum Scale"
            Else
                Return "Remove Scale Thresholds"
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            Dim bEnabled As Boolean
            bEnabled = True

            Dim pLayer As ILayer
            pLayer = m_pMapControl.CustomProperty

            If (m_lSubType = 3) Then
                If (pLayer.MaximumScale = 0) And (pLayer.MinimumScale = 0) Then bEnabled = False
            End If
            Return bEnabled
        End Get
    End Property
End Class


