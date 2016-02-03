Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.SystemUI
Imports System.Drawing.Text

<ComClass(FontToolControl.ClassId, FontToolControl.InterfaceId, FontToolControl.EventsId), _
 ProgId("ToolAndControlSampleVB.FontToolControl")> _
Public Class FontToolControl
    Implements ICommand
    Implements IToolControl
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

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "7E5412D7-9569-435a-81D4-5B34DE6CF9BC"
    Public Const InterfaceId As String = "1C3AB91A-3B3C-4fae-95E0-42C23F850BA9"
    Public Const EventsId As String = "7C107C99-AACA-469f-8C19-C4505FD8B8B4"
#End Region

    Private m_application As IApplication

    Private m_ifc As InstalledFontCollection
    Private m_hBitmap As IntPtr
    Private m_completeNotify As ICompletionNotify

    <DllImport("gdi32.dll")> _
    Private Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        'Set up bitmap - Note clean up is done in Dispose method instead of Finalize
        m_hBitmap = My.Resources.FontIcon.GetHbitmap(Drawing.Color.Magenta)
    End Sub

    ''' <summary>
    ''' Custom drawing when item is selected in the drop down to render
    ''' the actual font
    ''' </summary>
    Private Sub cboFont_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboFont.DrawItem
        e.DrawBackground()
        Dim ff As Drawing.FontFamily = DirectCast(cboFont.Items(e.Index), Drawing.FontFamily)
        Dim f As Drawing.Font

        'bitwise comparison for draw state
        If (e.State And Windows.Forms.DrawItemState.Selected) = Windows.Forms.DrawItemState.Selected And _
        (e.State And Windows.Forms.DrawItemState.ComboBoxEdit) <> Windows.Forms.DrawItemState.ComboBoxEdit Then
            'Determine font to render text
            If (ff.IsStyleAvailable(System.Drawing.FontStyle.Regular)) Then
                f = New Drawing.Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Regular)
            ElseIf (ff.IsStyleAvailable(System.Drawing.FontStyle.Bold)) Then
                f = New Drawing.Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Bold)
            ElseIf (ff.IsStyleAvailable(System.Drawing.FontStyle.Italic)) Then
                f = New Drawing.Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Italic)
            Else
                f = New Drawing.Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Underline)
            End If
            e.DrawFocusRectangle()
        Else
            f = cboFont.Font
        End If
        'Draw the item 
        e.Graphics.DrawString(ff.Name, f, System.Drawing.SystemBrushes.ControlText, e.Bounds.X, e.Bounds.Y)
    End Sub

    Private Sub cboFont_DropDownClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFont.DropDownClosed
        If m_completeNotify IsNot Nothing Then
            m_completeNotify.SetComplete()
        End If
    End Sub

#Region "ICommand Members"
    Public ReadOnly Property Bitmap() As Integer Implements ESRI.ArcGIS.SystemUI.ICommand.Bitmap
        Get
            Return m_hBitmap.ToInt32()
        End Get
    End Property

    Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.SystemUI.ICommand.Caption
        Get
            Return "Font Dropdown (VB.Net)"
        End Get
    End Property

    Public ReadOnly Property Category() As String Implements ESRI.ArcGIS.SystemUI.ICommand.Category
        Get
            Return ".NET Samples"
        End Get
    End Property

    Public ReadOnly Property Checked() As Boolean Implements ESRI.ArcGIS.SystemUI.ICommand.Checked
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property Enabled1() As Boolean Implements ESRI.ArcGIS.SystemUI.ICommand.Enabled
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property HelpContextID() As Integer Implements ESRI.ArcGIS.SystemUI.ICommand.HelpContextID
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements ESRI.ArcGIS.SystemUI.ICommand.HelpFile
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property Message() As String Implements ESRI.ArcGIS.SystemUI.ICommand.Message
        Get
            Return "Document Font dropdown list"
        End Get
    End Property

    Public ReadOnly Property Name1() As String Implements ESRI.ArcGIS.SystemUI.ICommand.Name
        Get
            Return "VBNETSamples_FontToolControl"
        End Get
    End Property

    Public Sub OnClick1() Implements ESRI.ArcGIS.SystemUI.ICommand.OnClick

    End Sub

    Public Sub OnCreate(ByVal hook As Object) Implements ESRI.ArcGIS.SystemUI.ICommand.OnCreate
        'Set up data for the dropdown
        m_ifc = New InstalledFontCollection()
        cboFont.DataSource = m_ifc.Families
        cboFont.ValueMember = "Name"

        m_application = TryCast(hook, IApplication)

        'TODO: Uncomment the following lines if you want the control to sync with default document font
        'OnDocumentSession()
        'SetUpDocumentEvent(m_application.Document)
        'AddHandler cboFont.SelectedValueChanged, AddressOf cboFont_SelectedValueChanged
    End Sub

    Public ReadOnly Property Tooltip() As String Implements ESRI.ArcGIS.SystemUI.ICommand.Tooltip
        Get
            Return "Font (VB.Net)"
        End Get
    End Property
#End Region
#Region "IToolControl Members"
    Public ReadOnly Property hWnd() As Integer Implements ESRI.ArcGIS.SystemUI.IToolControl.hWnd
        Get
            Return Me.Handle.ToInt32()
        End Get
    End Property

    Public Function OnDrop(ByVal barType As ESRI.ArcGIS.SystemUI.esriCmdBarType) As Boolean Implements ESRI.ArcGIS.SystemUI.IToolControl.OnDrop
        OnDocumentSession() 'Initialize the font
        Return True
    End Function

    Public Sub OnFocus(ByVal complete As ESRI.ArcGIS.SystemUI.ICompletionNotify) Implements ESRI.ArcGIS.SystemUI.IToolControl.OnFocus
        m_completeNotify = complete

        'Can also do any last minute UI update here
    End Sub

#End Region

#Region "Optional implementation to set document default font"
    ''' <summary>
    ''' Optional, wire the cboFont's SelectedValueChanged event if you want
    ''' to use this tool control to set the default text font of the document
    ''' </summary>
    Private Sub cboFont_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If m_application IsNot Nothing Then
            Dim mxDoc As IMxDocument = DirectCast(m_application.Document, IMxDocument)
            If Not mxDoc.DefaultTextFont.Name.Equals(cboFont.SelectedValue.ToString()) Then
                Dim ff As Drawing.FontFamily = DirectCast(cboFont.Items(cboFont.SelectedIndex), Drawing.FontFamily)
                Dim newFont As stdole.IFontDisp = DirectCast(New stdole.StdFontClass(), stdole.IFontDisp)
                newFont.Name = ff.GetName(1)
                newFont.Size = mxDoc.DefaultTextFont.Size

                'Alternative: Create a .Net Font object then convert
                'Dim f As Drawing.Font = New Drawing.Font(ff.GetName(1), mxDoc.DefaultTextFont.Size, System.Drawing.FontStyle.Regular)
                'Dim newFont As stdole.IFontDisp = DirectCast(ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(f), stdole.IFontDisp)

                'Set other font properties
                If mxDoc.DefaultTextFont.Bold Then newFont.Bold = True
                If mxDoc.DefaultTextFont.Italic Then newFont.Italic = True
                If mxDoc.DefaultTextFont.Underline Then newFont.Underline = True
                If mxDoc.DefaultTextFont.Strikethrough Then newFont.Strikethrough = True

                mxDoc.DefaultTextFont = newFont

                'Set dirty flag with change
                Dim docDirty As IDocumentDirty = DirectCast(mxDoc, IDocumentDirty)
                docDirty.SetDirty()
            End If
        End If
    End Sub

#Region "Document event handling"
    Private m_docEvents As IDocumentEvents_Event 'Event member variable.

    Sub SetUpDocumentEvent(ByVal myDocument As IDocument)
        m_docEvents = CType(myDocument, IDocumentEvents_Event)
        AddHandler m_docEvents.NewDocument, AddressOf OnDocumentSession
        AddHandler m_docEvents.OpenDocument, AddressOf OnDocumentSession
    End Sub

    Sub OnDocumentSession()
        'Get the default document font and update listbox
        Dim mxDoc As IMxDocument = DirectCast(m_application.Document, IMxDocument)
        Dim defaultFontName As String = mxDoc.DefaultTextFont.Name
        cboFont.SelectedValue = defaultFontName
    End Sub

#End Region
#End Region
End Class
