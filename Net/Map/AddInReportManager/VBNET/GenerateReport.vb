Public Class GenerateReport
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        Dim exportReport As ExportReport = New ExportReport()
        exportReport.Show()
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
