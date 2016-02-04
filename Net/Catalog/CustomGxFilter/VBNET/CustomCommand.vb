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
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI

Public Class CustomCommand
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

#Region "Member Variables"
    Private m_pApp As IGxApplication
#End Region

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        '
        '  TODO: Sample code showing how to access button host
        '
        My.ArcCatalog.Application.CurrentTool = Nothing
        ' set
        m_pApp = My.ArcCatalog.Application
        Dim pCat As IGxCatalog, pFileFilter As IGxFileFilter, pSelection As IEnumGxObject = Nothing
        Dim pDlg As IGxDialog, pFilter As IGxObjectFilter
        'Dim pDlg As IGxDialog, pHandle As ESRI.ArcGIS.esriSystem.OLE_HANDLE, pFilter As IGxObjectFilter
        Try

            pDlg = New GxDialog
            pCat = pDlg.InternalCatalog
            pFileFilter = pCat.FileFilter
            If pFileFilter.FindFileType("py") < 0 Then
                'enter the third parameter with the location of the icon as needed
                pFileFilter.AddFileType("py", "Python file", "")
            End If

            pFilter = New CustomFilter
            pDlg.ObjectFilter = pFilter
            pDlg.Title = "Please select a .Py file"
            pDlg.AllowMultiSelect = True
            pDlg.DoModalOpen(0, pSelection)
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            pCat = Nothing
            pFileFilter = Nothing
            pSelection = Nothing
            pDlg = Nothing
            pFilter = Nothing
        End Try
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcCatalog.Application IsNot Nothing
    End Sub
End Class
