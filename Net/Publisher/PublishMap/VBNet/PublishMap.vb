Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Publisher
Imports ESRI.ArcGIS.PublisherUI
Imports ESRI.ArcGIS.Carto

Public Class PublishMap
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        'Enable publisher extension
        If (EnablePublisherExtension()) Then

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim mapDoc As IMxDocument = My.ArcMap.Document

            Dim publishMaps As IPMFPublish3 = New PublisherEngineClass()

      'Accept the default settings when the map is published
            Dim settings As IPropertySet = publishMaps.GetDefaultPublisherSettings()

            Dim orderedMaps As IArray = New ArrayClass()
            'Loop over all the maps in the map document and each one to an Array object
            For i As Integer = 0 To mapDoc.Maps.Count - 1
                orderedMaps.Add(mapDoc.Maps.Item(i))
            Next


            Try
                'Create the PMF file using the current settings and the map order specified in the IArray parameter
                'It will be written to C:\\PublishedMap.pmf and the data will be referenced using relative paths.
                publishMaps.PublishWithOrder(orderedMaps, mapDoc.PageLayout, mapDoc.ActiveView, settings, _
                                                 True, "C:\\PublishedMap.pmf")

                'Report outcome to the user
                Dim mapdocTitle As String = DirectCast(My.ArcMap.Document, ESRI.ArcGIS.Framework.IDocument).Title
                Dim msg As String = String.Empty

                If (orderedMaps.Count = 1) Then
                    msg = "The map in " + mapdocTitle + " has been published successfully"
                Else
                    msg = "The maps in " + mapdocTitle + " have been published successfully"
                End If

                MessageBox.Show(msg)

            Catch ex As Exception
                MessageBox.Show("Error Publishing Map: " + ex.Message, "Error")
            End Try

            System.Windows.Forms.Cursor.Current = Cursors.Default

        End If

        My.ArcMap.Application.CurrentTool = Nothing
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

    Private Function EnablePublisherExtension() As Boolean

        Dim checkedOutOK As Boolean = False

        Try

            Dim extMgr As IExtensionManager = New ExtensionManagerClass()
            Dim extAdmin As IExtensionManagerAdmin = DirectCast(extMgr, IExtensionManagerAdmin)

            Dim ud As UID = New UID()
            ud.Value = "esriPublisherUI.Publisher"
            Dim obj As Object = 0
            extAdmin.AddExtension(ud, obj)

            Dim extConfig As ESRI.ArcGIS.esriSystem.IExtensionConfig = DirectCast(extMgr.FindExtension(ud), ESRI.ArcGIS.esriSystem.IExtensionConfig)

            If Not (extConfig.State = ESRI.ArcGIS.esriSystem.esriExtensionState.esriESUnavailable) Then
                'This checks on the extension
                extConfig.State = ESRI.ArcGIS.esriSystem.esriExtensionState.esriESEnabled
                checkedOutOK = True
            End If

        Catch ex As Exception
            MessageBox.Show("Publisher extension has failed to check out.", "Error")
        End Try

        Return checkedOutOK
    End Function

End Class
