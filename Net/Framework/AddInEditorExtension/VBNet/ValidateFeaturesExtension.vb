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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Geodatabase

Namespace AddInEditorExtension
	''' <summary>
	''' ValidateFeaturesExtension class implementing custom ESRI Editor Extension functionalities.
	''' </summary>
	Public Class ValidateFeaturesExtension
		Inherits ESRI.ArcGIS.Desktop.AddIns.Extension
		Public Sub New()
		End Sub
        'Invoked when the Editor Extension is loaded
        Protected Overrides Sub OnStartup()
            AddHandler Events.OnStartEditing, AddressOf Events_OnStartEditing
            AddHandler Events.OnStopEditing, AddressOf Events_OnStopEditing

        End Sub
        'Invoked at the start of the Editor Session
		Private Sub Events_OnStartEditing()
			'Since features of shapefiles, coverages etc cannot be validated, ignore wiring events for them
			If ArcMap.Editor.EditWorkspace.Type <> esriWorkspaceType.esriFileSystemWorkspace Then
				'wire OnCreateFeature Edit event
				AddHandler Events.OnCreateFeature, AddressOf Events_OnCreateChangeFeature
				'wire onChangeFeature Edit Event
				AddHandler Events.OnChangeFeature, AddressOf Events_OnCreateChangeFeature
			End If
		End Sub
        'Invoked at the end of the Edit session
        Private Sub Events_OnStopEditing(ByVal Save As Boolean)
            'Since features of shapefiles, coverages etc cannot be validated, ignore wiring events for them
            If ArcMap.Editor.EditWorkspace.Type <> esriWorkspaceType.esriFileSystemWorkspace Then
                'unwire OnCreateFeature Edit event
                RemoveHandler Events.OnCreateFeature, AddressOf Events_OnCreateChangeFeature
                'unwire onChangeFeature Edit Event
                RemoveHandler Events.OnChangeFeature, AddressOf Events_OnCreateChangeFeature
            End If
        End Sub
        'Invoked when a feature is created or modified
		Private Sub Events_OnCreateChangeFeature(ByVal obj As ESRI.ArcGIS.Geodatabase.IObject)


            Dim inFeature As IFeature = CType(obj, IFeature)
			If TypeOf inFeature.Class Is IValidation Then
				Dim validate As IValidate = CType(inFeature, IValidate)
                Dim errorMessage As String
                errorMessage = String.Empty

                Dim bIsvalid As Boolean = validate.Validate(errorMessage)

				If (Not bIsvalid) Then
					System.Windows.Forms.MessageBox.Show("Invalid Feature" & Constants.vbLf + Constants.vbLf & errorMessage)
				Else
					System.Windows.Forms.MessageBox.Show("Valid Feature")
				End If
			End If
		End Sub

		Protected Overrides Sub OnShutdown()
		End Sub
		#Region "Editor Events"

		#Region "Shortcut properties to the various editor event interfaces"
		Private ReadOnly Property Events() As IEditEvents_Event
			Get
				Return TryCast(ArcMap.Editor, IEditEvents_Event)
			End Get
		End Property
		Private ReadOnly Property Events2() As IEditEvents2_Event
			Get
				Return TryCast(ArcMap.Editor, IEditEvents2_Event)
			End Get
		End Property
		Private ReadOnly Property Events3() As IEditEvents3_Event
			Get
				Return TryCast(ArcMap.Editor, IEditEvents3_Event)
			End Get
		End Property
		Private ReadOnly Property Events4() As IEditEvents4_Event
			Get
				Return TryCast(ArcMap.Editor, IEditEvents4_Event)
			End Get
		End Property
		#End Region

		Private Sub WireEditorEvents()
			'
			'  TODO: Sample code demonstrating editor event wiring
			'
			AddHandler Events.OnCurrentTaskChanged, AddressOf AnonymousMethod1
			AddHandler Events2.BeforeStopEditing, AddressOf AnonymousMethod2
		End Sub
		Private Sub AnonymousMethod1()
			If ArcMap.Editor.CurrentTask IsNot Nothing Then
				System.Windows.Forms.MessageBox.Show(ArcMap.Editor.CurrentTask.Name)
			End If
		End Sub
		Private Sub AnonymousMethod2(ByVal save As Boolean)
			OnBeforeStopEditing(save)
		End Sub

		Private Sub OnBeforeStopEditing(ByVal save As Boolean)
		End Sub
		#End Region

	End Class
End Namespace
