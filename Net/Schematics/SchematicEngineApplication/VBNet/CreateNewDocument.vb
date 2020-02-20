'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.SystemUI

''' <summary>
''' Summary description for CreateNewDocument.
''' </summary>
Public Class CreateNewDocument
  Inherits BaseCommand
  Private m_hookHelper As IHookHelper = Nothing

  'constructor
  Public Sub New()
    'update the base properties
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "NewDocument"
    MyBase.m_message = "Create a new map"
    MyBase.m_toolTip = "Create a new map"
    MyBase.m_name = "DotNetTemplate_NewDocumentCommand"
  End Sub

#Region "Override Class Methods"

	''' <summary>
	''' Occurs when this command is created
	''' </summary>
	''' <param name="hook">Instance of the application</param>
	Public Overrides Sub OnCreate(ByVal hook As Object)
		If m_hookHelper Is Nothing Then
			m_hookHelper = New HookHelperClass()
		End If

		m_hookHelper.Hook = hook
	End Sub

	''' <summary>
	''' Occurs when this command is clicked
	''' </summary>
	Public Overrides Sub OnClick()
		Dim mapControl As IMapControl3 = Nothing

		'get the MapControl from the hook in case the container is a ToolbatControl
		If TypeOf m_hookHelper.Hook Is IToolbarControl Then
			mapControl = CType((CType(m_hookHelper.Hook, IToolbarControl)).Buddy, IMapControl3)
			'In case the container is MapControl
		ElseIf TypeOf m_hookHelper.Hook Is IMapControl3 Then
			mapControl = CType(m_hookHelper.Hook, IMapControl3)
		Else
			MsgBox("Active control must be MapControl!", MsgBoxStyle.Exclamation, "Warning")
			Return
		End If

		'check to see if there is an active edit session and whether edits have been made

		Dim engineEditor As IEngineEditor = New EngineEditorClass()

		If ((engineEditor.EditState = esriEngineEditState.esriEngineStateEditing) And (engineEditor.HasEdits() = True)) Then

			Dim mbInStyle As Integer = CInt(MsgBoxStyle.Question) + CInt(MsgBoxStyle.YesNoCancel)

			Dim result As MsgBoxResult = MsgBox("Would you like to save your edits?", CType(mbInStyle, Microsoft.VisualBasic.MsgBoxStyle), "Save Edits")

			If result = MsgBoxResult.Cancel Then
				Return
			ElseIf result = MsgBoxResult.No Then
				engineEditor.StopEditing(False)
			ElseIf result = MsgBoxResult.Yes Then
				engineEditor.StopEditing(True)
			End If

		End If


		'allow the user to save the current document
		Dim mbInVal As Integer = CInt(MsgBoxStyle.Question) + CInt(MsgBoxStyle.YesNo)

		Dim res As MsgBoxResult = MsgBox("Would you like to save the current document?", CType(mbInVal, Microsoft.VisualBasic.MsgBoxStyle), "AoView")
		If res = MsgBoxResult.Yes Then
			'launch the save command (why work hard!?)
			Dim command As ICommand = New ControlsSaveAsDocCommandClass()
			command.OnCreate(m_hookHelper.Hook)
			command.OnClick()
		End If

		'create a new Map
		Dim map As IMap = New MapClass()
		map.Name = "Map"

		'assign the new map to the MapControl
		mapControl.DocumentFilename = String.Empty
		mapControl.Map = map
	End Sub

#End Region
End Class

