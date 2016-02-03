Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace SelectionCOMSample
  <Guid("3c20265d-1db1-4c94-b145-a2f6cdb504bf"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionCountDockWin")> _
  Partial Public Class SelectionCountDockWin
	  Inherits UserControl
    Implements IDockableWindowDef, IDockableWindowInitialPlacement, IDockableWindowImageDef

    Private m_application As IApplication
    Private Shared s_listView As System.Windows.Forms.ListView

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisible(False)> _
    Private Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)
    End Sub

    <ComUnregisterFunction(), ComVisible(False)> _
    Private Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)
    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxDockableWindows.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxDockableWindows.Unregister(regKey)

    End Sub

#End Region
#End Region

    Public Sub New()
      InitializeComponent()
      s_listView = listView1
      listView1.View = View.Details

    End Sub
    Friend Shared Sub Clear()
      If s_listView IsNot Nothing Then
        s_listView.Items.Clear()
      End If
    End Sub

    Friend Shared Sub AddItem(ByVal layerName As String, ByVal selectionCount As Integer)
      If s_listView Is Nothing Then
        Return
      End If

      Dim item As New ListViewItem(layerName)
      item.SubItems.Add(selectionCount.ToString())
      s_listView.Items.Add(item)
    End Sub


#Region "IDockableWindowDef Members"

    Private ReadOnly Property Caption() As String Implements IDockableWindowDef.Caption
      Get
        Return "Selected Features Count"
      End Get
    End Property

    Private ReadOnly Property ChildHWND() As Integer Implements IDockableWindowDef.ChildHWND
      Get
        Return Me.Handle.ToInt32()
      End Get
    End Property

    Private ReadOnly Property Name() As String Implements IDockableWindowDef.Name
      Get
        Return "ESRI_SelectionCOMSample_SelCountDockWin"
      End Get
    End Property

    Private Sub OnCreate(ByVal hook As Object) Implements IDockableWindowDef.OnCreate
      m_application = TryCast(hook, IApplication)
    End Sub

    Private Sub OnDestroy() Implements IDockableWindowDef.OnDestroy
    End Sub

    Private ReadOnly Property UserData() As Object Implements IDockableWindowDef.UserData
      Get
        Return Nothing
      End Get
    End Property

#End Region


#Region "IDockableWindowImageDef Members"
    Public ReadOnly Property Bitmap() As Integer Implements ESRI.ArcGIS.Framework.IDockableWindowImageDef.Bitmap
      Get
        Dim image As New System.Drawing.Bitmap(Me.GetType().Assembly.GetManifestResourceStream("ToggleDockWinBtn.png"))
        image.MakeTransparent()
        Return image.GetHbitmap().ToInt32()
      End Get
    End Property
#End Region



#Region "IDockableWindowInitialPlacement Members"

    Private ReadOnly Property DockPosition() As esriDockFlags Implements IDockableWindowInitialPlacement.DockPosition
      Get
        Return esriDockFlags.esriDockRight Or esriDockFlags.esriDockTabbed
      End Get
    End Property

    Private ReadOnly Property Height() As Integer Implements IDockableWindowInitialPlacement.Height
      Get
        Return 300
      End Get
    End Property

    Private ReadOnly Property Neighbor() As UID Implements IDockableWindowInitialPlacement.Neighbor
      Get
        Dim uid As UID = New UIDClass()
        uid.Value = "esriArcMapUI.TOCDockableWindow" 'TOC

        Return uid
      End Get
    End Property

    Private ReadOnly Property Width() As Integer Implements IDockableWindowInitialPlacement.Width
      Get
        Return 300
      End Get
    End Property

#End Region


  End Class
End Namespace
