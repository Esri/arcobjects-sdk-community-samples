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

Public Class PackagePMF
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        'Enable publisher extension
        If (EnablePublisherExtension()) Then

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim pmfPackager As IPMFPackage = New PackagerEngineClass()

            'Create a new directory to store the pmf and data folders of the package:
            Directory.CreateDirectory("C:\temp\MyPMFPackage")

            'All vector data will be converted to file geodatabase feature class format.
            'All raster data will be converted to compressed file geodatabase raster format.
            Dim settings As IPropertySet = pmfPackager.GetDefaultPackagerSettings()
            settings.SetProperty("Vector Type", esriAPEVectorType.esriAPEVectorTypeFileGDB)
            settings.SetProperty("Raster Type", esriAPERasterType.esriAPERasterTypeFileGDBCompressed)
            settings.SetProperty("Package Directory", "C:\temp\MyPMFPackage")

            'Specify the name of the pmf to be packaged
            Dim strArray As IStringArray = New StrArrayClass()
            strArray.Add("C:\PublishedMap.pmf")

            Try
                'Package the pmf with the specified settings
                pmfPackager.Package(settings, Nothing, strArray)

                MessageBox.Show("Packaging is complete.", "Packaging Results")

            Catch ex As Exception
                MessageBox.Show("Failed to package the PMF: " + ex.Message)
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
