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
Option Strict Off
Option Explicit On 

Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ADF.BaseClasses

Friend Class CreateBookmarkCommand
    Inherits BaseCommand

    Private m_HookHelper As IHookHelper

	Public Sub New()
        MyBase.New()

        m_HookHelper = New HookHelperClass
        MyBase.m_caption = "Create..."
        MyBase.m_category = "Developer Samples"
        MyBase.m_enabled = True
        MyBase.m_message = "Creates a spatial bookmark based upon the current extent"
        MyBase.m_name = "Create..."
        MyBase.m_toolTip = "Create spatial bookmark"
	End Sub
	
    Protected Overrides Sub Finalize()
        m_HookHelper = Nothing
        MyBase.Finalize()
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_HookHelper.Hook = hook
    End Sub

    Public Overrides Sub OnClick()
        'Get a name for bookmark from the user
        Dim sName As String
        sName = InputBox("Bookmark Name", "Spatial Bookmark")
        If Trim(sName) = "" Then Exit Sub

        'Get the focus map
        Dim pActiveView As IActiveView
        pActiveView = m_HookHelper.FocusMap

        'Create a new bookmark
        Dim pBookmark As IAOIBookmark
        pBookmark = New AOIBookmarkClass
        'Set the location to the current extent of the focus map
        pBookmark.Location = pActiveView.Extent
        'Set the bookmark name
        pBookmark.Name = sName

        'Get the bookmark collection of the focus map
        Dim pMapBookmarks As IMapBookmarks
        pMapBookmarks = m_HookHelper.FocusMap
        'Add the bookmark to the bookmarks collection
        pMapBookmarks.AddBookmark(pBookmark)
    End Sub
End Class