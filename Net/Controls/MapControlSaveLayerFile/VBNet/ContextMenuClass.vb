Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Controls

  ''' <summary>
  ''' Context menu class for Engine applications.
  '''</summary>
  <Guid("94e68d22-88b1-4294-ae29-0ae80ffc8468"), ClassInterface(ClassInterfaceType.None), ProgId("MapControlSaveLayerFile.ContextMenuClass")> _
  Public Class ContextMenuClass
	Private m_toolbarMenu As IToolbarMenu2 = Nothing
	Private m_beginGroupFlag As Boolean = False

	Public Sub New()
	End Sub

	''' <summary>
	''' Instantiate the underlying ToolbarMenu and set the hook object to be
	''' passed into the OnCreate event of each command item.
	''' </summary>
	Public Sub SetHook(ByVal hook As Object)
	  m_toolbarMenu = New ToolbarMenuClass()
	  m_toolbarMenu.SetHook(hook)

	  '
	  ' TODO: Define context menu items here
	  '
	  'AddItem("esriControls.ControlsMapZoomOutFixedCommand", -1);
	  'AddItem("esriControls.ControlsMapZoomInFixedCommand", -1);
	  'BeginGroup(); //Separator
	  'AddItem("{380FB31E-6C24-4F5C-B1DF-47F33586B885}", -1); //undo command
	  'AddItem(new Guid("B0675372-0271-4680-9A2C-269B3F0C01E8"), -1); //redo command
	  'BeginGroup(); //Separator
	  'AddItem("MyCustomCommandCLSIDorProgID", -1);
	End Sub

	''' <summary>
	''' Popup the context menu at the given location
	''' </summary>
	''' <param name="X">X coordinate where to popup the menu</param>
	''' <param name="Y">Y coordinate where to popup the menu</param>
	''' <param name="hWndParent">Handle to the parent window</param>
	Public Sub PopupMenu(ByVal X As Integer, ByVal Y As Integer, ByVal hWndParent As Integer)
	  If Not m_toolbarMenu Is Nothing Then
		m_toolbarMenu.PopupMenu(X, Y, hWndParent)
	  End If
	End Sub

	''' <summary>
	''' Retrieve the ToolbarMenu object in case if needed to be modified at
	''' run time.
	''' </summary>
	Public ReadOnly Property ContextMenu() As IToolbarMenu2
	  Get
		Return m_toolbarMenu
	  End Get
	End Property

	#Region "Helper methods to add items to the context menu"
	''' <summary>
	''' Adds a separator bar on the command bar to begin a group. 
	''' </summary>
	Private Sub BeginGroup()
	  m_beginGroupFlag = True
	End Sub

	''' <summary>
	''' Add a command item to the command bar by an Unique Identifier Object (UID).
	''' </summary>
	Private Sub AddItem(ByVal itemUID As UID)
	  m_toolbarMenu.AddItem(itemUID.Value, itemUID.SubType, -1, m_beginGroupFlag, esriCommandStyles.esriCommandStyleIconAndText)
	  m_beginGroupFlag = False 'Reset group flag
	End Sub

	''' <summary>
	''' Add a command item to the command bar by an identifier string
	''' and a subtype index
	''' </summary>
	Private Sub AddItem(ByVal itemID As String, ByVal subtype As Integer)
	  Dim itemUID As UID = New UIDClass()
	  Try
		itemUID.Value = itemID
	  Catch
		'Add an empty guid to indicate something's wrong "Missing"
		itemUID.Value = Guid.Empty.ToString("B")
	  End Try

	  If subtype > 0 Then
		itemUID.SubType = subtype
	  End If
	  AddItem(itemUID)

	End Sub

	''' <summary>
	''' Add a command item to the command bar by the Guid 
	''' and a subtype index.
	''' </summary>
	Private Sub AddItem(ByVal itemGuid As Guid, ByVal subtype As Integer)
	  AddItem(itemGuid.ToString("B"), subtype)
	End Sub

	''' <summary>
	''' Add a command item to the command bar by a type
	''' and a subtype index.
	''' </summary>
	Private Sub AddItem(ByVal itemType As Type, ByVal subtype As Integer)
	  If Not itemType Is Nothing Then
		AddItem(itemType.GUID, subtype)
	  End If
	End Sub

	#End Region

  End Class
