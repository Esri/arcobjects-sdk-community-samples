Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Controls

Namespace NAEngine
	Public Class CommonFunctions
		Private Sub New()
		End Sub
		Public Shared Function GetTheEngineNetworkAnalystEnvironment() As IEngineNetworkAnalystEnvironment
            ' The ArcGIS Network Analyst extension environment is a singleton, and must be accessed using the System.Activator
			Dim t As System.Type = System.Type.GetTypeFromProgID("esriControls.EngineNetworkAnalystEnvironment")
            Dim naEnv As IEngineNetworkAnalystEnvironment = TryCast(System.Activator.CreateInstance(t), IEngineNetworkAnalystEnvironment)
			Return naEnv
		End Function
	End Class
End Namespace
