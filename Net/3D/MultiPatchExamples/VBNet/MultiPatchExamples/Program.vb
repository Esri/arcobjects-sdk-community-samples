Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms


Public Class Program
    Private Sub New()
    End Sub
    <STAThread()> _
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        If Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine) Then
            If Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime.")
            End If
        End If

        Application.Run(New MultiPatchExamples())
    End Sub
End Class
