Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.Collections
Imports System.Reflection
Namespace PersistExtensionAddIn
  Public Class LogExtension
	  Inherits ESRI.ArcGIS.Desktop.AddIns.Extension
	Private _log As Dictionary(Of String, String)

	Public Sub New()
	End Sub

	Protected Overrides Sub OnStartup()
	  WireDocumentEvents()
	End Sub

	Private Sub WireDocumentEvents()
	  ' Named event handler
	  AddHandler ArcMap.Events.OpenDocument, AddressOf Events_OpenDocument
	End Sub


	Private Sub Events_OpenDocument()
	  Dim logText As String = "Document was saved by " & _log("userName") & " at " & _log("time")
	  LogMessage(logText)
	End Sub

	'Get called when saving document.
	Protected Overrides Sub OnSave(ByVal outStrm As Stream)
            ' Override OnSave and uses a binary formatter to serialize the log.
	  Dim log As New Dictionary(Of String, String)()
	  log.Add("userName", Environment.UserName)
	  log.Add("time", DateTime.Now.ToLongTimeString())
	  Dim bf = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
	  bf.Serialize(outStrm, log)
	End Sub

	'Get called when opening a document with persisted stream.
	Protected Overrides Sub OnLoad(ByVal inStrm As Stream)
            ' Override OnLoad and uses a binary formatter to deserialize the log.
	  Dim bf = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
	  _log = CType(bf.Deserialize(inStrm), Dictionary(Of String, String))
	End Sub

	Private Sub LogMessage(ByVal message As String)
	  System.Windows.Forms.MessageBox.Show(message)
	End Sub
  End Class

End Namespace
