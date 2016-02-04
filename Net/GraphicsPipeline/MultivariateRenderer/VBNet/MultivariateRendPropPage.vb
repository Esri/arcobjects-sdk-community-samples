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
Imports System.Drawing
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs

<ComClass(MultivariateRendPropPage.ClassId, MultivariateRendPropPage.InterfaceId, MultivariateRendPropPage.EventsId)> _
Public Class MultivariateRendPropPage
    ' custom renderer property page class for MultivariateRenderer

    ' a renderer property page must implement these interfaces:
    Implements IComPropertyPage
    Implements IComEmbeddedPropertyPage
    Implements IRendererPropertyPage

    <DllImport("gdi32.dll")> _
    Private Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    Private m_Page As PropPageForm
    Private m_pRend As IFeatureRenderer
    Private m_Priority As Long
    Private m_bitmap As System.Drawing.Bitmap
    Private m_hBitmap As IntPtr


    Public Sub New()
        ''MsgBox("New (color prop page)")
        m_Page = New PropPageForm
        m_Priority = 550    ' 5th category is for multiple attribute renderers

        Dim res() As String = GetType(MultivariateRendPropPage).Assembly.GetManifestResourceNames()
        If (res.GetLength(0) > 0) Then
            Try

                Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
                m_bitmap = New System.Drawing.Bitmap(Me.GetType().Assembly.GetManifestResourceStream(bitmapResourceName))
                If Not (m_bitmap Is Nothing) Then
                    m_bitmap.MakeTransparent(m_bitmap.GetPixel(1, 1))
                    m_hBitmap = m_bitmap.GetHbitmap()
                End If
            Catch

            End Try

        End If

    End Sub

    Protected Overrides Sub Finalize()
        If (m_hBitmap.ToInt32() <> 0) Then
            DeleteObject(m_hBitmap)
        End If
    End Sub

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "FE338F20-B39C-49B8-9F28-FAC2F0DE0C0D"
    Public Const InterfaceId As String = "BAC02C70-EC55-4A1D-AF09-08E39BEA78DB"
    Public Const EventsId As String = "AED3C901-6B05-40A9-93CF-FE26C6C3FA01"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction()> _
    Public Shared Sub Reg(ByVal regKey As String)
        RendererPropertyPages.Register(regKey)
    End Sub

    <ComUnregisterFunction()> _
    Public Shared Sub Unreg(ByVal regKey As String)
        RendererPropertyPages.Unregister(regKey)
    End Sub
#End Region

    Public Function Activate() As Integer Implements IComPropertyPage.Activate

        Return m_Page.Handle.ToInt32()

    End Function

    Public Function Applies(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) As Boolean Implements IComPropertyPage.Applies

        Dim pObj As Object

        If objects.Count <= 0 Then
            Return False
            Exit Function
        End If

        objects.Reset()
        pObj = objects.Next
        Do While Not TypeOf pObj Is IFeatureRenderer
            pObj = objects.Next
            If pObj Is Nothing Then
                Return False
                Exit Function
            End If
        Loop

        Return (TypeOf pObj Is IMultivariateRenderer)

    End Function

    Public Sub Apply() Implements IComPropertyPage.Apply
        QueryObject(m_pRend)

    End Sub

    Public Sub Cancel() Implements IComPropertyPage.Cancel
        ' doing nothing discards any changes made on the page since last Apply.  this is
        '   what we want.

    End Sub

    Public Sub Deactivate() Implements IComPropertyPage.Deactivate

    End Sub

    Public ReadOnly Property Height() As Integer Implements IComPropertyPage.Height
        Get
            'MsgBox("Height")
            Return m_Page.Height
        End Get
    End Property

    Public ReadOnly Property HelpContextID(ByVal controlID As Integer) As Integer Implements IComPropertyPage.HelpContextID
        Get
            ' NOTIMPL
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements IComPropertyPage.HelpFile
        Get
            Return ""
        End Get
    End Property

    Public Sub Hide() Implements IComPropertyPage.Hide
        m_Page.Hide()

    End Sub

    Public ReadOnly Property IsPageDirty() As Boolean Implements IComPropertyPage.IsPageDirty
        Get
            ' check flag on form to see if page is dirty
            ' this tells the property sheet whether or not to redraw page
            Return m_Page.IsDirty
        End Get
    End Property

    Public WriteOnly Property PageSite() As IComPropertyPageSite Implements IComPropertyPage.PageSite
        Set(ByVal Value As IComPropertyPageSite)
            m_Page.PageSite = Value
        End Set
    End Property

    Public Property Priority() As Integer Implements IComPropertyPage.Priority
        Get
            Return m_Priority
        End Get
        Set(ByVal Value As Integer)
            m_Priority = Value
        End Set
    End Property

    Public Sub SetObjects(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) Implements IComPropertyPage.SetObjects
        ' supplies the page with the object(s) to be edited including the map, feature layer,
        '   feature class, and renderer
        ' note:  the feature renderer passed in as part of Objects is the one created
        '   in CreateCompatibleObject

        Dim pObj As Object

        If objects.Count <= 0 Then
            Exit Sub
        End If
        objects.Reset()
        pObj = objects.Next

        Dim pMap As IMap = Nothing
        Dim pGeoLayer As IGeoFeatureLayer = Nothing

        ' in this implementation we need info from the map and the renderer
        Do While Not pObj Is Nothing
            If TypeOf pObj Is IMap Then pMap = pObj
            If TypeOf pObj Is IGeoFeatureLayer Then pGeoLayer = pObj
            If TypeOf pObj Is IFeatureRenderer Then m_pRend = pObj

            pObj = objects.Next
        Loop
        If (Not pMap Is Nothing) And (Not pGeoLayer Is Nothing) And (Not m_pRend Is Nothing) Then
            m_Page.InitControls(m_pRend, pMap, pGeoLayer)
        End If

    End Sub

    Public Sub Show() Implements IComPropertyPage.Show
        m_Page.Show()

    End Sub

    Public Property Title() As String Implements IComPropertyPage.Title
        Get
            Return m_Page.Name
        End Get
        Set(ByVal Value As String)
            m_Page.Name = Value
        End Set
    End Property

    Public ReadOnly Property Width() As Integer Implements IComPropertyPage.Width
        Get
            Return m_Page.Width
        End Get
    End Property

    Public Function CreateCompatibleObject(ByVal kind As Object) As Object Implements IComEmbeddedPropertyPage.CreateCompatibleObject
        ' check to see if the renderer is compatible with the property page...
        '    ...if so, return the renderer.  If not, create a new one.

        Dim pFeatRend As IFeatureRenderer

        If (TypeOf kind Is IMultivariateRenderer) And (Not kind Is Nothing) Then
            pFeatRend = kind
        Else
            ' create a new MultivariateRenderer 
            pFeatRend = New MultivariateRenderer
        End If

        Return pFeatRend
    End Function

    Public Sub QueryObject(ByVal theObject As Object) Implements IComEmbeddedPropertyPage.QueryObject
        ' triggered when OK or Apply is pressed on the property page
        Dim pRend As IFeatureRenderer

        If (TypeOf theObject Is IMultivariateRenderer) And (Not theObject Is Nothing) Then
            pRend = theObject
            m_Page.InitRenderer(pRend)
        End If
    End Sub

    Public Function CanEdit(ByVal obj As IFeatureRenderer) As Boolean Implements IRendererPropertyPage.CanEdit
        Return (TypeOf obj Is IMultivariateRenderer)

    End Function

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements IRendererPropertyPage.ClassID
        Get
            ' return prog id of the property page object
            Dim pUID As New UID
            pUID.Value = "MultivariateRendPropPage"
            Return pUID
        End Get
    End Property

    Public ReadOnly Property Description() As String Implements IRendererPropertyPage.Description
        Get
            ' appears on ArcMap symbology property page
            Return "Display features with multivariate symbology"
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements IRendererPropertyPage.Name
        Get
            Return "Multivariate Renderer"
        End Get
    End Property

    Public ReadOnly Property PreviewImage() As Integer Implements IRendererPropertyPage.PreviewImage
        Get
            Return m_hBitmap.ToInt32()
        End Get
    End Property

    Public ReadOnly Property RendererClassID() As ESRI.ArcGIS.esriSystem.UID Implements IRendererPropertyPage.RendererClassID
        Get
            Dim pUID As New UID
            pUID.Value = "MultivariateRenderer"
            Return pUID
        End Get
    End Property

    Public ReadOnly Property Type() As String Implements IRendererPropertyPage.Type
        Get
            ' text that appears for category in "Show" tree view
            '   on symbology property page

            Return "Multiple Attributes"
        End Get
    End Property
End Class


