Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.Remoting
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

  <Guid("71D93E11-AD59-4104-B922-A92B5F2BF69E"), ComVisible(True), ProgId("PropertySheet"), ClassInterface(ClassInterfaceType.None)> _
  Public Partial Class PropertySheet : Inherits UserControl : Implements IProvideObjectHandle, ISpecifyPropertyPages
	#Region "Control Registration"
	<ComRegisterFunction> _
	Private Shared Sub ComRegister(ByVal t As Type)
	  Dim keyName As String = "CLSID\" & t.GUID.ToString("B")
	  Using key As RegistryKey = Registry.ClassesRoot.OpenSubKey(keyName, True)
		key.CreateSubKey("Control").Close()
		Using subkey As RegistryKey = key.CreateSubKey("MiscStatus")
		  subkey.SetValue("", "131457")
		End Using
		Using subkey As RegistryKey = key.CreateSubKey("TypeLib")
		  Dim libid As Guid = Marshal.GetTypeLibGuidForAssembly(t.Assembly)
		  subkey.SetValue("", libid.ToString("B"))
		End Using
		Using subkey As RegistryKey = key.CreateSubKey("Version")
		  Dim ver As Version = t.Assembly.GetName().Version
		  Dim version As String = String.Format("{0}.{1}", ver.Major, ver.Minor)
		  If version = "0.0" Then
		  version = "1.0"
		  End If
		  subkey.SetValue("", version)
		End Using
	  End Using
	End Sub

	<ComUnregisterFunction> _
	Private Shared Sub ComUnregister(ByVal t As Type)
	  ' Delete entire CLSID\{clsid} subtree
	  Dim keyName As String = "CLSID\" & t.GUID.ToString("B")
	  Registry.ClassesRoot.DeleteSubKeyTree(keyName)
	End Sub
	#End Region

	Private m_layer As RSSWeatherLayerClass = Nothing

	Public Sub New()
	  InitializeComponent()
	End Sub

	#Region "IProvideObjectHandle Members"

	Public ReadOnly Property ObjectHandle() As ObjectHandle Implements IProvideObjectHandle.ObjectHandle
	  Get
		  Return New ObjectHandle(Me)
	  End Get
	End Property

	#End Region

	#Region "ISpecifyPropertyPages Members"

	Public Sub GetPages(ByRef pPages As CAUUID) Implements ISpecifyPropertyPages.GetPages
	  Dim g As Guid() = New Guid(1){}

	  g(0) = GetType(RSSLayerProps).GUID
	  g(1) = GetType(RSSLayerProps2).GUID
	  pPages.SetPages(g)
	End Sub

	Public Property RSSWatherLayer() As RSSWeatherLayerClass
	  Get
		  Return m_layer
	  End Get
	  Set
		  m_layer = Value
	  End Set
	End Property
	#End Region
  End Class
