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
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto


<ComClass(CultureCommand.ClassId, CultureCommand.InterfaceId, CultureCommand.EventsId)> _
Public NotInheritable Class CultureCommand
  Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "e9f55592-10be-4ebd-8a82-9d3871ee2bd7"
    Public Const InterfaceId As String = "3b79bd48-c5fd-42f3-b18a-7ea0173b1557"
    Public Const EventsId As String = "99552d21-7704-4388-b0c7-7d6fd7944018"
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

    Private m_pHookHelper As IHookHelper

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
        pResource_bitmap = CType(pResourceManager.GetObject("CommandImage"), System.Drawing.Bitmap)
        MyBase.m_bitmap = New System.Drawing.Bitmap(pResource_bitmap)

        pResource_str = CType(pResourceManager.GetObject("CommandCaption"), String)
        MyBase.m_caption = pResource_str

        pResource_str = CType(pResourceManager.GetObject("CommandMessage"), String)
        MyBase.m_message = pResource_str

        pResource_str = CType(pResourceManager.GetObject("CommandToolTip"), String)
        MyBase.m_toolTip = pResource_str

        MyBase.m_category = "CustomCommands"
        MyBase.m_name = "CustomCommands_CultureCommand"


    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If (m_pHookHelper Is Nothing) Then
            m_pHookHelper = New HookHelperClass
        End If

        If Not (hook Is Nothing) Then
            m_pHookHelper.Hook = hook
        End If

    End Sub


    Public Overrides Sub OnClick()

        'This button simply tells the user which culture and regional language the application
        'employs when it is running.

        'Acquire the Thread Culture Name
        Dim m_culture As String
        m_culture = "Culture = " + System.Threading.Thread.CurrentThread.CurrentUICulture.DisplayName.ToString()

        'Acquire the Regional Language string
        Dim m_language As String
        m_language = "The culture of this application employs the '" _
        + System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToString() _
        + "' regional and language code " + Environment.NewLine

        'Acquire the Timestamp format
        Dim m_datetime As String
        m_datetime = "with the following timestamp format: " _
        + System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern.ToString()

        'Write the message to the MessageBox
        Dim m_message As String
        m_message = m_language + m_datetime
        System.Windows.Forms.MessageBox.Show(m_message, m_culture)

    End Sub

End Class



