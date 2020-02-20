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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Globalization

<ComClass(CultureTool.ClassId, CultureTool.InterfaceId, CultureTool.EventsId)> _
Public NotInheritable Class CultureTool
    Inherits BaseTool

    'The HookHelper object that deals with the hook passed to the OnCreate event
    Private m_pHookHelper As New HookHelperClass


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "2e2ab7c0-a7a3-4f40-9b58-c67f96c8f463"
    Public Const InterfaceId As String = "c55088e2-af75-42e3-8c8d-2ca28f51f86d"
    Public Const EventsId As String = "ce02de9c-98e5-4bb6-92c8-2a6f0ed6ce3c"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction(), ComVisible(False)> _
    Public Shared Sub RegisterFunction(ByVal sKey As String)
        Dim fullKey As String = sKey.Remove(0, 18) & "\Implemented Categories"
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, True)
        If Not (regKey Is Nothing) Then
            regKey.CreateSubKey("{B284D891-22EE-4F12-A0A9-B1DDED9197F4}")
        End If
    End Sub
    <ComUnregisterFunction(), ComVisible(False)> _
    Public Shared Sub UnregisterFunction(ByVal sKey As String)
        Dim fullKey As String = sKey.Remove(0, 18) & "\Implemented Categories"
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, True)
        If Not (regKey Is Nothing) Then
            regKey.DeleteSubKey("{B284D891-22EE-4F12-A0A9-B1DDED9197F4}")
        End If
    End Sub
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        'The BitMap, Caption, Message and ToolTip are set from strings and images 
        'stored in the Resource File. The ResourceManager acquires the appropriate 
        'Resource file according to the UI Culture of the current thread

        Dim pResourceManager As New System.Resources.ResourceManager("VBDotNetCultureSample.Resources", Me.GetType().Assembly)
        Dim pResource_bitmap As System.Drawing.Bitmap
        Dim pResource_str As String

        'Set the tool properties
        pResource_bitmap = CType(pResourceManager.GetObject("ToolImage"), System.Drawing.Bitmap)
        MyBase.m_bitmap = New System.Drawing.Bitmap(pResource_bitmap)

        pResource_str = CType(pResourceManager.GetObject("ToolCaption"), String)
        MyBase.m_caption = pResource_str

        pResource_str = CType(pResourceManager.GetObject("ToolMessage"), String)
        MyBase.m_message = pResource_str

        pResource_str = CType(pResourceManager.GetObject("ToolToolTip"), String)
        MyBase.m_toolTip = pResource_str

        MyBase.m_category = "CustomCommands"
        MyBase.m_name = "CustomCommands_CultureTool"

    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            'Set the enabled property
            If Not m_pHookHelper.ActiveView Is Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        'With this tool the user may place the current Date and Time onto the Page Layout
        'using the Timestamp format defined by the UI Culture of the current thread

        'Get the active view
        Dim pActiveView As IActiveView
        pActiveView = m_pHookHelper.ActiveView

        'Create a new text element
        Dim pTextElement As ITextElement
        pTextElement = New TextElement

        'Create a text symbol
        Dim pTextSymbol As ITextSymbol
        pTextSymbol = New TextSymbol
        pTextSymbol.Size = 10

        'Create a page point
        Dim pPoint As IPoint
        pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

        'Get the FullDateTimePattern from the CurrentUICulture of the thread
        Dim pDateTimePattern As String
        pDateTimePattern = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern.ToString()

        'Set the text element properties
        pTextElement.Symbol = pTextSymbol
        pTextElement.Text = System.DateTime.Now.ToString(pDateTimePattern)

        'QI for IElement
        Dim pElement As IElement
        pElement = pTextElement
        'Set the elements geometry
        pElement.Geometry = pPoint

        'Add the element to the graphics container
        pActiveView.GraphicsContainer.AddElement(pTextElement, 0)
        'Refresh the graphics
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    End Sub

End Class


