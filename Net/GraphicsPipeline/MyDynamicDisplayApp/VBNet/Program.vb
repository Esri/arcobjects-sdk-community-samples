Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem

  Friend Class Program
	''' <summary>
	''' The main entry point for the application.
	''' </summary>
	Private Sub New()
	End Sub
	<STAThread> _
	Shared Sub Main()
	  Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)
	  Application.Run(New MainForm())
	End Sub
  End Class