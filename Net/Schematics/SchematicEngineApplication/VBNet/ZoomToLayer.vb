Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses

Public NotInheritable Class ZoomToLayer

    Inherits BaseCommand

    Private m_pMapControl As imapcontrol3

    Public Sub New()
        MyBase.New()
        MyBase.m_caption = "Zoom To Layer"
    End Sub

	Public Overrides Sub OnCreate(ByVal hook As Object)
		m_pMapControl = TryCast(hook, IMapControl3)
	End Sub

    Public Overrides Sub OnClick()
        Dim pLayer As ILayer
		pLayer = TryCast(m_pMapControl.CustomProperty, ILayer)
        m_pMapControl.Extent = pLayer.AreaOfInterest
    End Sub
End Class






