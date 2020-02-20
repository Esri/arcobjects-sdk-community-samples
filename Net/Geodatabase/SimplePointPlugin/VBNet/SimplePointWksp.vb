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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

	''' <summary>
	''' Summary description for SimplePointWksp.
	''' </summary>

<ComVisible(False)> _
Friend Class SimplePointWksp
  Implements IPlugInWorkspaceHelper, IPlugInMetadataPath
  Private m_sWkspPath As String

#Region "class constructor"
  Public Sub New(ByVal wkspPath As String)
    'HIGHLIGHT: set up workspace path
    If System.IO.Directory.Exists(wkspPath) Then
      m_sWkspPath = wkspPath
    Else
      m_sWkspPath = Nothing
    End If
  End Sub
#End Region

#Region "IPlugInWorkspaceHelper Members"
  Public ReadOnly Property OIDIsRecordNumber() As Boolean Implements IPlugInWorkspaceHelper.OIDIsRecordNumber
    Get
      Return True 'OID's are continuous
    End Get
  End Property

  Public Function OpenDataset(ByVal localName As String) As IPlugInDatasetHelper Implements IPlugInWorkspaceHelper.OpenDataset
    'HIGHLIGHT: OpenDataset - give workspace path and local file name
    If m_sWkspPath Is Nothing Then
      Return Nothing
    End If

    Dim ds As SimplePointDataset = New SimplePointDataset(m_sWkspPath, localName)
    Return CType(ds, IPlugInDatasetHelper)
  End Function

  Public ReadOnly Property RowCountIsCalculated() As Boolean Implements IPlugInWorkspaceHelper.RowCountIsCalculated
    Get
      Return True
    End Get
  End Property

  Public ReadOnly Property DatasetNames(ByVal DatasetType As ESRI.ArcGIS.Geodatabase.esriDatasetType) As ESRI.ArcGIS.esriSystem.IArray Implements ESRI.ArcGIS.Geodatabase.IPlugInWorkspaceHelper.DatasetNames
    Get
      If m_sWkspPath Is Nothing Then
        Return Nothing
      End If

      'HIGHLIGHT: DatasetNames - Go through wksString to look for csp files
      If DatasetType <> esriDatasetType.esriDTAny AndAlso DatasetType <> esriDatasetType.esriDTTable Then
        Return Nothing
      End If

      Dim sFiles As String() = System.IO.Directory.GetFiles(m_sWkspPath, "*.csp")
      If sFiles Is Nothing OrElse sFiles.Length = 0 Then
        Return Nothing
      End If

      Dim datasets As IArray = New ArrayClass()
      For Each sFileName As String In sFiles
        Dim ds As SimplePointDataset = New SimplePointDataset(m_sWkspPath, System.IO.Path.GetFileNameWithoutExtension(sFileName))
        datasets.Add(ds)
      Next sFileName

      Return datasets
    End Get
  End Property

  Public ReadOnly Property NativeType(ByVal DatasetType As ESRI.ArcGIS.Geodatabase.esriDatasetType, ByVal localName As String) As ESRI.ArcGIS.Geodatabase.INativeType Implements ESRI.ArcGIS.Geodatabase.IPlugInWorkspaceHelper.NativeType
    Get
      Return Nothing
    End Get
  End Property

#End Region

#Region "IPlugInMetadataPath Members"
  'HIGHLIGHT: implement metadata so export data in arcmap works correctly
  Public ReadOnly Property MetadataPath(ByVal localName As String) As String Implements ESRI.ArcGIS.Geodatabase.IPlugInMetadataPath.MetadataPath
    Get
      Return System.IO.Path.Combine(m_sWkspPath, localName & ".csp.xml")
    End Get
  End Property
#End Region
End Class
