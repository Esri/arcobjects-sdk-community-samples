'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
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
