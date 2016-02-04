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
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geodatabase
Imports System.Runtime.InteropServices

<ComClass(clsExtentView.ClassId, clsExtentView.InterfaceId, clsExtentView.EventsId), _
 ProgId("ExtentViewVBNET.clsExtentView")> _
Public Class clsExtentView
    Implements ESRI.ArcGIS.CatalogUI.IGxView
#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxPreviews.Register(regKey)
        GxTabViews.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxPreviews.Unregister(regKey)
        GxTabViews.Unregister(regKey)

    End Sub

#End Region
#End Region


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6d5be066-c982-4475-81f9-d6893e4f5223"
    Public Const InterfaceId As String = "036839ad-597c-469c-b0a7-538fddba2425"
    Public Const EventsId As String = "ceb9c4a7-67f4-430e-ae78-013696ed505e"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

#Region "Member Variables"
    Private m_pFillSymbol As IFillSymbol
    Private WithEvents m_pSelection As GxSelection
    Private frmExtentView As New FrmExtentView
    Private m_path As String
    Private m_Deactivate As Boolean = False
#End Region
    Public Sub New()
        MyBase.New()
    End Sub
    Private Sub m_pSelection_OnSelectionChanged(ByVal Selection As ESRI.ArcGIS.Catalog.IGxSelection, ByRef initiator As Object) Handles m_pSelection.OnSelectionChanged
        'Refresh view
        If Not m_Deactivate Then Refresh()
    End Sub
    Public Sub Activate(ByVal Application As ESRI.ArcGIS.CatalogUI.IGxApplication, ByVal Catalog As ESRI.ArcGIS.Catalog.IGxCatalog) Implements ESRI.ArcGIS.CatalogUI.IGxView.Activate
        Try
            'Get selection
            m_pSelection = Application.Selection
            ' get data from the MyProject's settings.
            ' please change accordingly
            m_path = My.Settings.DataLocation
            'Add data to map control
            frmExtentView.AxMapControl1.AddShapeFile(m_path, "world30")
            frmExtentView.AxMapControl1.Extent = frmExtentView.AxMapControl1.FullExtent

            'Create and setup the fill symbol that will be used to draw the dataset's extent
            ' rectangle if it is not cached
            If m_pFillSymbol Is Nothing Then
                m_pFillSymbol = New SimpleFillSymbol

                Dim pColor As IColor
                Dim pLineSymbol As ILineSymbol
                pColor = New RgbColor
                pColor.NullColor = True
                m_pFillSymbol.Color = pColor

                pLineSymbol = New SimpleLineSymbol
                pColor.NullColor = False
                pColor.RGB = &HFF&  'Red
                pLineSymbol.Color = pColor
                pLineSymbol.Width = 2
                m_pFillSymbol.Outline = pLineSymbol
            End If

            'Draw extent
            Refresh()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Public Function Applies(ByVal Selection As ESRI.ArcGIS.Catalog.IGxObject) As Boolean Implements ESRI.ArcGIS.CatalogUI.IGxView.Applies
        'This view applies if the current Gx selection supports IGxDataset.
        Applies = (Not Selection Is Nothing) And (TypeOf Selection Is IGxDataset)
    End Function

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.CatalogUI.IGxView.ClassID
        Get
            'Set class ID
            Dim pUID As IUID
            pUID = New UID
            pUID.Value = "ExtentViewVBNET.clsExtentView"
            ClassID1 = pUID
        End Get
    End Property

    Public Sub Deactivate() Implements ESRI.ArcGIS.CatalogUI.IGxView.Deactivate
        'Prevent circular reference
        If Not m_pSelection Is Nothing Then
            m_Deactivate = True
            m_pSelection = Nothing
        End If
    End Sub

    Public ReadOnly Property DefaultToolbarCLSID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.CatalogUI.IGxView.DefaultToolbarCLSID
        Get
            'You can set it to the CLSID of toolbar you want to use
            DefaultToolbarCLSID = Nothing
        End Get
    End Property

    Public ReadOnly Property hWnd() As Integer Implements ESRI.ArcGIS.CatalogUI.IGxView.hWnd
        Get
            'The map control's hWnd is to be used as the Gx view window.  Gx will embed this
            ' hWnd inside the Preview window.
            hWnd = frmExtentView.AxMapControl1.Handle
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.CatalogUI.IGxView.Name
        Get
            'Set view name
            Name = "Extent"
        End Get
    End Property

    Public Sub Refresh() Implements ESRI.ArcGIS.CatalogUI.IGxView.Refresh
        'If the selection does not support IGxDataset, do nothing.
        Dim pSelection As IGxSelection
        Dim pLocation As IGxObject
        Dim pGraphicsLayer As IGraphicsContainer
        Try
            pSelection = m_pSelection
            pLocation = pSelection.Location
            If Not TypeOf pLocation Is IGxDataset Then Exit Sub

            'Clear the contents of the graphics layer.

            pGraphicsLayer = frmExtentView.AxMapControl1.Map.BasicGraphicsLayer
            pGraphicsLayer.DeleteAllElements()

            'Some dataset may not have content at all
            Dim pGxDataset As IGxDataset
            Dim pGeoDataset As IGeoDataset
            Dim pElement As IElement
            Dim pFillShapeElement As IFillShapeElement
            Try
                'Get the geodataset out of the GxDataset.
                pGxDataset = pLocation
                If (pGxDataset.Type = esriDatasetType.esriDTLayer Or _
                    pGxDataset.Type = esriDatasetType.esriDTFeatureClass Or _
                    pGxDataset.Type = esriDatasetType.esriDTFeatureDataset) Then
                    pGeoDataset = pGxDataset.Dataset
                Else
                    Exit Sub
                End If
                pGeoDataset = pGxDataset.Dataset

                'Create a rectangular graphic element to represent the geodataset's full extent.
                pElement = New RectangleElement
                pElement.Geometry = pGeoDataset.Extent

                'Set the element's symbology.
                pFillShapeElement = pElement
                pFillShapeElement.Symbol = m_pFillSymbol

                'Add the rectangle element to the graphics layer, and force the map to redraw.
                pGraphicsLayer.AddElement(pElement, 0)
                frmExtentView.AxMapControl1.Refresh()

            Catch ex As Exception
                frmExtentView.AxMapControl1.Refresh()
                Throw ex
            Finally
                pGxDataset = Nothing
                pGeoDataset = Nothing
                pElement = Nothing
                pFillShapeElement = Nothing
            End Try
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            pSelection = Nothing
            pLocation = Nothing
            pGraphicsLayer = Nothing
        End Try

    End Sub

    Public ReadOnly Property SupportsTools() As Boolean Implements ESRI.ArcGIS.CatalogUI.IGxView.SupportsTools
        Get
            'If you want this view to support tools, you can set it to "True"
            SupportsTools = False
        End Get
    End Property

    Public Sub SystemSettingChanged(ByVal flag As Integer, ByVal section As String) Implements ESRI.ArcGIS.CatalogUI.IGxView.SystemSettingChanged

    End Sub
End Class


