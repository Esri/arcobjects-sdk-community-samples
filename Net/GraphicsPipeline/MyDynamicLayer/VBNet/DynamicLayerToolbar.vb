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
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.SystemUI

  ''' <summary>
  ''' A toolbar class for ArcGIS applications.
  ''' <para>In order to add commandItems to the toolbar, use the ToolDef array definition. The parameters passed to the 
  ''' ToolDef struct constructor are the commandItem's CLSID or ProgID, boolean flag which indicates whether
  ''' to begin a new group on the menu and the subtype number of the command. If the CommnadItem does not implements 
  ''' any subtypes, just pass -1</para>
  ''' </summary>
  <Guid("0515b4e8-83b9-409b-bcde-e7d59c6a86a8"), ClassInterface(ClassInterfaceType.None), ProgId("DynamicLayerToolbar"), ComVisible(True)> _
  Public Class DynamicLayerToolbar : Implements IToolBarDef
	#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisible(False)> _
	Private Shared Sub RegisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryRegistration(registerType)

	  '
	  ' TODO: Add any COM registration code here
	  '
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
	  ControlsToolbars.Register(regKey)
	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  ControlsToolbars.Unregister(regKey)
	End Sub

	#End Region
	#End Region

	'class members
	Private m_sToolbarName As String
	Private m_sToolbarCaption As String

	''' <summary>
	''' a data-structure used in order to store and manage item definitions
	''' </summary>
	Private Structure ToolDef
	  Public itemDef As String
	  Public group As Boolean
	  Public subType As Integer

	  ''' <summary>
	  ''' struct constructor
	  ''' </summary>
	  ''' <param name="itd">The CLSID or PROGID of the item being defined</param>
	  ''' <param name="grp">Indicates if the defined item should start a group on the toolbar.</param>
	  ''' <param name="subtype">The subtype of the item being defined</param>
'INSTANT VB NOTE: The parameter subtype was renamed since Visual Basic will not uniquely identify class members when parameters have the same name:
	  Public Sub New(ByVal itd As String, ByVal grp As Boolean, ByVal subtype_Renamed As Integer)
			itemDef = itd
			group = grp
			subType = subtype_Renamed
	  End Sub
	End Structure

	'an array of item definitions which will be used to create the commends for the toolbar
									 'add the 'AddMyDynamicLayerCmd' command onto the toolbar
	Private m_toolDefs As ToolDef() = { New ToolDef("AddMyDynamicLayerCmd", False, -1) }

	''' <summary>
	''' Class constructor
	''' </summary>
	Public Sub New()
	  'name the toolbar and set its caption
	  m_sToolbarName = "MyDynamicLayer"
	  m_sToolbarCaption = "MyDynamicLayer"
	End Sub

	#Region "IToolBarDef Implementations"
	''' <summary>
	''' The CLSID for the item on this toolbar at the specified index.
	''' </summary>
	''' <param name="pos">the locational index number of this item on the toolbar</param>
	''' <param name="itemDef">IItemDef object that defines the item at this position of the toolbar</param>
	Public Sub GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements IToolBarDef.GetItemInfo
	  itemDef.ID = m_toolDefs(pos).itemDef
	  itemDef.Group = m_toolDefs(pos).group
	  itemDef.SubType = m_toolDefs(pos).subType
	End Sub

	''' <summary>
	''' The caption of this toolbar
	''' </summary>
	Public ReadOnly Property Caption() As String Implements IToolBarDef.Caption
	  Get
			Return m_sToolbarCaption
	  End Get
	End Property

	''' <summary>
	''' The name of this toolbar
	''' </summary>
	Public ReadOnly Property Name() As String Implements IToolBarDef.Name
	  Get
			Return m_sToolbarName
	  End Get
	End Property

	''' <summary>
	''' The number of items in this toolbar
	''' </summary>
	Public ReadOnly Property ItemCount() As Integer Implements IToolBarDef.ItemCount
	  Get
			Return m_toolDefs.Length
	  End Get
	End Property
	#End Region
  End Class

