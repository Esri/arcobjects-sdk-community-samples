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
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

	''' <summary>
	''' Summary description for SimplePointWkspFact.
	''' </summary>
	''' 
<ClassInterface(ClassInterfaceType.None), Guid("6e167940-6d49-485b-a2b8-061c144d805f"), ProgId("SimplePointPlugin.SimplePointWFHelper")> _
Public NotInheritable Class SimplePointWkspFact
	Implements IPlugInWorkspaceFactoryHelper
#Region "Component Category Registration"

	<ComRegisterFunction()> _
	Public Shared Sub RegisterFunction(ByVal regKey As String)
		'proxy PluginWorkspaceFactoryHelper
		PlugInWorkspaceFactoryHelpers.Register(regKey)
	End Sub

	<ComUnregisterFunction()> _
	Public Shared Sub UnregisterFunction(ByVal regKey As String)
		PlugInWorkspaceFactoryHelpers.Unregister(regKey)
	End Sub
#End Region

#Region "class constructor"
	Public Sub New()
	End Sub
#End Region

#Region "IPlugInWorkspaceFactoryHelper Members"

	Public ReadOnly Property DatasetDescription(ByVal DatasetType As ESRI.ArcGIS.Geodatabase.esriDatasetType) As String Implements ESRI.ArcGIS.Geodatabase.IPlugInWorkspaceFactoryHelper.DatasetDescription
		Get
			Select Case DatasetType
				Case esriDatasetType.esriDTTable
					Return "SimplePoint Table"
				Case esriDatasetType.esriDTFeatureClass
					Return "SimplePoint Feature Class"
				Case esriDatasetType.esriDTFeatureDataset
					Return "SimplePoint Feature Dataset"
				Case Else
					Return Nothing
			End Select
		End Get
	End Property

	Public ReadOnly Property WorkspaceDescription(ByVal plural As Boolean) As String Implements ESRI.ArcGIS.Geodatabase.IPlugInWorkspaceFactoryHelper.WorkspaceDescription
		Get
			If plural Then
				Return "Simple Points"
			Else
				Return "Simple Point"
			End If
		End Get
	End Property

	Public ReadOnly Property CanSupportSQL() As Boolean Implements IPlugInWorkspaceFactoryHelper.CanSupportSQL
		Get
			Return False
		End Get
	End Property

	Public ReadOnly Property DataSourceName() As String Implements IPlugInWorkspaceFactoryHelper.DataSourceName
		'HIGHLIGHT: ProgID = esriGeoDatabase.<DataSourceName>WorkspaceFactory
		Get
			Return "SimplePointPlugin"
		End Get
	End Property

	Public Function ContainsWorkspace(ByVal parentDirectory As String, ByVal fileNames As IFileNames) As Boolean Implements IPlugInWorkspaceFactoryHelper.ContainsWorkspace
		If fileNames Is Nothing Then
			Return Me.IsWorkspace(parentDirectory)
		End If

		If (Not System.IO.Directory.Exists(parentDirectory)) Then
			Return False
		End If

		Dim sFileName As String
		sFileName = fileNames.Next()
		Do While Not sFileName Is Nothing
			If Not fileNames.IsDirectory() Then
				If System.IO.Path.GetExtension(sFileName).Equals(".csp") Then
					Return True
				End If
			End If
			sFileName = fileNames.Next()
		Loop

		Return False
	End Function

	Public ReadOnly Property WorkspaceFactoryTypeID() As UID Implements IPlugInWorkspaceFactoryHelper.WorkspaceFactoryTypeID
		'HIGHLIGHT: Generate a new GUID to identify the workspace factory
		Get
			Dim wkspFTypeID As UID = New UIDClass()
			wkspFTypeID.Value = "{b8a25f89-2adc-43c0-ac2e-16b3a88e3915}" 'proxy
			Return wkspFTypeID
		End Get
	End Property


	Public Function IsWorkspace(ByVal wksString As String) As Boolean Implements ESRI.ArcGIS.Geodatabase.IPlugInWorkspaceFactoryHelper.IsWorkspace
		'IsWorkspace is True when the folder contains csp files
		If System.IO.Directory.Exists(wksString) Then
			Return System.IO.Directory.GetFiles(wksString, "*.csp").Length > 0
		End If
		Return False
	End Function

	Public ReadOnly Property WorkspaceType() As esriWorkspaceType Implements IPlugInWorkspaceFactoryHelper.WorkspaceType
		'HIGHLIGHT: WorkspaceType - FileSystem type strongly recommended
		Get
			Return esriWorkspaceType.esriFileSystemWorkspace
		End Get
	End Property

	Public Function OpenWorkspace(ByVal wksString As String) As IPlugInWorkspaceHelper Implements IPlugInWorkspaceFactoryHelper.OpenWorkspace
		'HIGHLIGHT: OpenWorkspace
		'Don't have to check if wksString contains valid data file. 
		'Any valid folder path is fine since we want paste to work in any folder
		If System.IO.Directory.Exists(wksString) Then
			Dim openWksp As SimplePointWksp = New SimplePointWksp(wksString)
			Return CType(openWksp, IPlugInWorkspaceHelper)
		End If
		Return Nothing
	End Function

	Public Function GetWorkspaceString(ByVal parentDirectory As String, ByVal fileNames As IFileNames) As String Implements IPlugInWorkspaceFactoryHelper.GetWorkspaceString
		'return the path to the workspace location if 
		If (Not System.IO.Directory.Exists(parentDirectory)) Then
			Return Nothing
		End If

		If fileNames Is Nothing Then 'don't have to check.csp file
			Return parentDirectory
		End If

		'HIGHLIGHT: GetWorkspaceString - claim and remove file names from list
		Dim sFileName As String
		Dim fileFound As Boolean = False
		sFileName = fileNames.Next()

		Do While Not sFileName Is Nothing
			If Not fileNames.IsDirectory() Then
				If System.IO.Path.GetExtension(sFileName).Equals(".csp") Then
					fileFound = True
					fileNames.Remove()
				End If
			End If
			sFileName = fileNames.Next()
		Loop

		If fileFound Then
			Return parentDirectory
		Else
			Return Nothing
		End If
	End Function

#End Region

End Class
