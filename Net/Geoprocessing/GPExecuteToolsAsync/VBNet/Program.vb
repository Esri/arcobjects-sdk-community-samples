Imports System.Collections.Generic
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS

NotInheritable Class Program
	Private Sub New()
	End Sub

	''' <summary>
	''' The main entry point for the application.
	''' </summary>
	<STAThread> _
	Friend Shared Sub Main()
		If Not RuntimeManager.Bind(ProductCode.Engine) Then
			If Not RuntimeManager.Bind(ProductCode.Desktop) Then
				MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
				Return
			End If
		End If

		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)
		Application.Run(New RunGPForm())
	End Sub
End Class
