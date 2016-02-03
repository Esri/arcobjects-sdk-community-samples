Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Carto
Imports System.Collections.Generic

Namespace SelectionCOMSample
  ''' <summary>
  ''' Summary description for SelectionTargetComboBox.
  ''' </summary>
  <Guid("80e577d6-1b27-4471-986e-c04050a0e2d8"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.SelectionTargetComboBox")> _
  Public NotInheritable Class SelectionTargetComboBox
	  Inherits BaseCommand
    Implements IComboBox

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisible(False)> _
    Private Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)

      '
      ' TODO: Add any COM registration code here
      ''
    End Sub

    <ComUnregisterFunction(), ComVisible(False)> _
    Private Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)

      '
      ' TODO: Add any COM unregistration code here
      ''
    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_doc As IMxDocument
    Private Shared s_comboBox As SelectionTargetComboBox
    Private m_selAllCookie As Integer
    Private m_comboBoxHook As IComboBoxHook
    Private m_list As Dictionary(Of Integer, IFeatureLayer)

    Public Sub New()
      MyBase.m_category = "Developer Samples"
      MyBase.m_caption = "Selection Target"
      MyBase.m_message = "Select the selection target VB.NET."
      MyBase.m_toolTip = "Select the selection target VB.NET."
      MyBase.m_name = "ESRI_SelectionCOMSample_SelectionTargetComboBox"

      m_selAllCookie = -1
      s_comboBox = Me
      m_list = New Dictionary(Of Integer, IFeatureLayer)()
      Try
        MyBase.m_bitmap = New Bitmap(Me.GetType().Assembly.GetManifestResourceStream("SelectionTargetComboBox.png"))
      Catch ex As Exception
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
      End Try
    End Sub
    Friend Shared Function GetSelectionComboBox() As SelectionTargetComboBox
      Return s_comboBox
    End Function
    Friend Sub AddItem(ByVal itemName As String, ByVal layer As IFeatureLayer)
      If m_selAllCookie = -1 Then
        m_selAllCookie = m_comboBoxHook.Add("Select All")
      End If

      'Add each item to combo box.
      Dim cookie As Integer = m_comboBoxHook.Add(itemName)
      m_list.Add(cookie, layer)
    End Sub

    Friend Sub ClearAll()
      m_selAllCookie = -1
      m_comboBoxHook.Clear()
    End Sub

#Region "Overridden Class Methods"

        ''' <summary>
        ''' Occurs when this command is created
        ''' </summary>
        ''' <param name="hook">Instance of the application</param>
        Public Overrides Sub OnCreate(ByVal hook As Object)
            If hook Is Nothing Then
                Return
            End If

            m_comboBoxHook = TryCast(hook, IComboBoxHook)

            Dim application As IApplication = TryCast(m_comboBoxHook.Hook, IApplication)
            m_doc = TryCast(application.Document, IMxDocument)

            'Disable if it is not ArcMap
            If TypeOf m_comboBoxHook.Hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If

        End Sub

        ''' <summary>
        ''' Occurs when this command is clicked
        ''' </summary>
        Public Overrides Sub OnClick()
        End Sub

#End Region

#Region "IComboBox Members"
    Public ReadOnly Property DropDownHeight() As Integer Implements ESRI.ArcGIS.SystemUI.IComboBox.DropDownHeight
      Get
        Return 4
      End Get
    End Property

    Public ReadOnly Property DropDownWidth() As String Implements ESRI.ArcGIS.SystemUI.IComboBox.DropDownWidth
      Get
        Return "WWWWWWWWWWWWWWWWW"
      End Get
    End Property

    Public ReadOnly Property Editable() As Boolean Implements ESRI.ArcGIS.SystemUI.IComboBox.Editable
      Get
        Return False
      End Get
    End Property

    Public ReadOnly Property HintText() As String Implements ESRI.ArcGIS.SystemUI.IComboBox.HintText
      Get
        Return "Choose a target layer."
      End Get
    End Property

    Public Sub OnEditChange(ByVal editString As String) Implements ESRI.ArcGIS.SystemUI.IComboBox.OnEditChange

    End Sub

    Public Sub OnEnter() Implements ESRI.ArcGIS.SystemUI.IComboBox.OnEnter

    End Sub

    Public Sub OnFocus(ByVal [set] As Boolean) Implements ESRI.ArcGIS.SystemUI.IComboBox.OnFocus

    End Sub

    Public Sub OnSelChange(ByVal cookie As Integer) Implements ESRI.ArcGIS.SystemUI.IComboBox.OnSelChange
      If cookie = -1 Then
        Return
      End If

      For Each item As KeyValuePair(Of Integer, IFeatureLayer) In m_list
        'All feature layers are selectable if "Select All" is selected;
        'otherwise, only the selected layer is selectable.
        Dim fl As IFeatureLayer = item.Value
        If fl Is Nothing Then
          Continue For
        End If

        If cookie = item.Key Then
          fl.Selectable = True
          Continue For
        End If

        fl.Selectable = If((cookie = m_selAllCookie), True, False)
      Next item

      'Fire ContentsChanged event to cause TOC to refresh with new selected layers.
      m_doc.ActiveView.ContentsChanged()
    End Sub

    Public ReadOnly Property ShowCaption() As Boolean Implements ESRI.ArcGIS.SystemUI.IComboBox.ShowCaption
      Get
        Return False
      End Get
    End Property

    Public ReadOnly Property Width() As String Implements ESRI.ArcGIS.SystemUI.IComboBox.Width
      Get
        Return "WWWWWWWWWWWWWX"
      End Get
    End Property
#End Region

  End Class
End Namespace
