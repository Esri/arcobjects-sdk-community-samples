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

Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

Friend Class SpatialBookmarksMultiItem
    Implements IMultiItem
	
    Private m_pHookHelper As IHookHelper
	
    Public Sub New()
        MyBase.New()
        m_pHookHelper = New HookHelperClass
    End Sub

    Protected Overrides Sub Finalize()
        m_pHookHelper = Nothing
    End Sub

    Private ReadOnly Property IMultiItem_Caption() As String Implements IMultiItem.Caption
        Get
            Return "Spatial Bookmarks"
        End Get
    End Property

    Private ReadOnly Property IMultiItem_HelpContextID() As Integer Implements IMultiItem.HelpContextID
        Get
            'Not implemented
        End Get
    End Property

    Private ReadOnly Property IMultiItem_HelpFile() As String Implements IMultiItem.HelpFile
        Get
            Return ""
        End Get
    End Property

    Private ReadOnly Property IMultiItem_ItemCaption(ByVal index As Integer) As String Implements IMultiItem.ItemCaption
        Get

            'Get the bookmarks of the focus map
            Dim pMapBookmarks As IMapBookmarks
            pMapBookmarks = m_pHookHelper.FocusMap

            'Get bookmarks enumerator
            Dim pEnumSpatialBookmarks As IEnumSpatialBookmark
            pEnumSpatialBookmarks = pMapBookmarks.Bookmarks
            pEnumSpatialBookmarks.Reset()

            'Loop through the bookmarks to get bookmark names
            Dim pSpatialBookmark As ISpatialBookmark
            Dim bookmarkCount As Integer
            pSpatialBookmark = pEnumSpatialBookmarks.Next

            bookmarkCount = 0
            Do Until pSpatialBookmark Is Nothing
                'Get the correct bookmark
                If bookmarkCount = index Then
                    'Return the bookmark name
                    Return pSpatialBookmark.Name
                    Exit Do
                End If
                bookmarkCount = bookmarkCount + 1
                pSpatialBookmark = pEnumSpatialBookmarks.Next
            Loop
            Return ""
        End Get
    End Property

    Private ReadOnly Property IMultiItem_ItemChecked(ByVal index As Integer) As Boolean Implements IMultiItem.ItemChecked
        Get
            'Not implemented
        End Get
    End Property

    Private ReadOnly Property IMultiItem_ItemEnabled(ByVal index As Integer) As Boolean Implements IMultiItem.ItemEnabled
        Get
            Return True
        End Get
    End Property

    Private ReadOnly Property IMultiItem_Message() As String Implements IMultiItem.Message
        Get
            Return "Spatial bookmarks in the focus map"
        End Get
    End Property

    Private ReadOnly Property IMultiItem_Name() As String Implements IMultiItem.Name
        Get
            Return "Spatial Bookmarks"
        End Get
    End Property

    Private Sub IMultiItem_OnItemClick(ByVal index As Integer) Implements IMultiItem.OnItemClick

        'Get the bookmarks of the focus map
        Dim pMapBookmarks As IMapBookmarks
        pMapBookmarks = m_pHookHelper.FocusMap

        'Get bookmarks enumerator
        Dim pEnumSpatialBookmarks As IEnumSpatialBookmark
        pEnumSpatialBookmarks = pMapBookmarks.Bookmarks
        pEnumSpatialBookmarks.Reset()

        'Loop through the bookmarks to get bookmark to zoom to
        Dim pSpatialBookmark As ISpatialBookmark
        Dim bookmarkCount As Integer
        pSpatialBookmark = pEnumSpatialBookmarks.Next

        bookmarkCount = 0
        Do Until pSpatialBookmark Is Nothing
            'Get the correct bookmark
            If bookmarkCount = index Then
                'Zoom to the bookmark
                pSpatialBookmark.ZoomTo(m_pHookHelper.FocusMap)
                'Refresh the map
                m_pHookHelper.ActiveView.Refresh()
                Exit Do
            End If
            bookmarkCount = bookmarkCount + 1
            pSpatialBookmark = pEnumSpatialBookmarks.Next
        Loop

    End Sub

    Private Function IMultiItem_OnPopup(ByVal hook As Object) As Integer Implements IMultiItem.OnPopup

        m_pHookHelper.Hook = hook

        'Get the bookmarks of the focus map
        Dim pMapBookmarks As IMapBookmarks
        pMapBookmarks = m_pHookHelper.FocusMap

        'Get bookmarks enumerator
        Dim pEnumSpatialBookmarks As IEnumSpatialBookmark
        pEnumSpatialBookmarks = pMapBookmarks.Bookmarks
        pEnumSpatialBookmarks.Reset()

        'Loop through the bookmarks to count them
        Dim pSpatialBookmark As ISpatialBookmark
        Dim bookmarkCount As Integer
        pSpatialBookmark = pEnumSpatialBookmarks.Next

        bookmarkCount = 0
        Do Until pSpatialBookmark Is Nothing
            bookmarkCount = bookmarkCount + 1
            pSpatialBookmark = pEnumSpatialBookmarks.Next
        Loop

        'Return the number of multiitems
        IMultiItem_OnPopup = bookmarkCount

    End Function

    Public ReadOnly Property ItemBitmap(ByVal index As Integer) As Integer Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemBitmap
        Get

        End Get
    End Property

End Class