Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports ESRI.ArcGIS.ConversionTools
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.esriSystem

Namespace GeodatabaseConversion

    Friend Class ToFileGDB

        <STAThread()> _
        Shared Sub Main(ByVal args As String())


            If (Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)) Then
                If (Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)) Then
                    System.Windows.Forms.MessageBox.Show("This application could not load the correct version of ArcGIS.")
                End If
            End If

            Dim aoLicenseInitializer As LicenseInitializer
            aoLicenseInitializer = New LicenseInitializer

            'ESRI License Initializer generated code.
            If (Not aoLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced}, _
            New esriLicenseExtensionCode() {esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork})) Then
                System.Windows.Forms.MessageBox.Show("This application could not initialize with the correct ArcGIS license and will shutdown. LicenseMessage: " + aoLicenseInitializer.LicenseMessage())
                aoLicenseInitializer.ShutdownApplication()
                Return
            End If

            ' Run geoprocessing code
            ConvertPersonalGeodatabaseToFileGeodatabase()

            ' Shutdown application
            aoLicenseInitializer.ShutdownApplication()

        End Sub



        Private Shared Sub ConvertPersonalGeodatabaseToFileGeodatabase()
            ' Initialize the Geoprocessor
            Dim geoprocessor As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor()

            ' Allow for the overwriting of file geodatabases, if they previously exist.
            geoprocessor.OverwriteOutput = True

            ' Set the workspace to a folder containing personal geodatabases.
            geoprocessor.SetEnvironmentValue("workspace", "C:\data")

            ' Identify personal geodatabases.
            Dim workspaces As IGpEnumList = geoprocessor.ListWorkspaces("*", "Access")
            Dim workspace As String = workspaces.Next()
            Do While workspace <> ""
                ' Set workspace to current personal geodatabase
                geoprocessor.SetEnvironmentValue("workspace", workspace)

                ' Create a file geodatabase with the same name as the personal geodatabase
                Dim gdbname As String = System.IO.Path.GetFileName(workspace).Replace(".mdb", "")
                Dim dirname As String = System.IO.Path.GetDirectoryName(workspace)

                ' Execute CreateFileGDB tool
                Dim createFileGDBTool As CreateFileGDB = New CreateFileGDB(dirname, gdbname & ".gdb")
                geoprocessor.Execute(createFileGDBTool, Nothing)

                ' Initialize the Copy Tool
                Dim copyTool As Copy = New Copy()

                ' Identify feature classes and copy to file geodatabase
                Dim fcs As IGpEnumList = geoprocessor.ListFeatureClasses("", "", "")
                Dim fc As String = fcs.Next()
                Do While fc <> ""
                    Console.WriteLine("Copying " & fc & " to " & gdbname & ".gdb")
                    copyTool.in_data = fc
                    copyTool.out_data = dirname & "\" & gdbname & ".gdb" & "\" & fc
                    geoprocessor.Execute(copyTool, Nothing)
                    fc = fcs.Next()
                Loop

                ' Identify feature datasets and copy to file geodatabase
                Dim fds As IGpEnumList = geoprocessor.ListDatasets("", "")
                Dim fd As String = fds.Next()
                Do While fd <> ""
                    Console.WriteLine("Copying " & fd & " to " & gdbname & ".gdb")
                    copyTool.in_data = fd
                    copyTool.data_type = "FeatureDataset"
                    copyTool.out_data = dirname & "\" & gdbname & ".gdb" & "\" & fd
                    Try
                        geoprocessor.Execute(copyTool, Nothing)
                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show(ex.Message)
                    End Try

                    fd = fds.Next()
                Loop

                ' Identify tables and copy to file geodatabase
                Dim tbls As IGpEnumList = geoprocessor.ListTables("", "")
                Dim tbl As String = tbls.Next()
                Do While tbl <> ""
                    Console.WriteLine("Copying " & tbl & " to " & gdbname & ".gdb")
                    copyTool.in_data = tbl
                    copyTool.out_data = dirname & "\" & gdbname & ".gdb" & "\" & tbl
                    geoprocessor.Execute(copyTool, Nothing)
                    tbl = tbls.Next()
                Loop

                workspace = workspaces.Next()
            Loop
        End Sub

    End Class

End Namespace
