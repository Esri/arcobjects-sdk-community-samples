'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
'
 
'* copyfeatures.cs : This VB.NET sample uses the Geoprocessor class in conjunction with geoprocessing tools classes to
'* execute a series of geoprocessing tools. This sample will extract features to a new feature class based on a
 '* location and an attribute query.

'
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Windows.Forms
Imports System.Text
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.AnalysisTools
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.esriSystem


Namespace copyfeatures

    Friend Class copyfeatures

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


            ' Run the geoprocessing code
            SelectFeaturesAndRunCopyFeatures()

            ' Shutdown application
            aoLicenseInitializer.ShutdownApplication()

        End Sub


        Private Shared Sub SelectFeaturesAndRunCopyFeatures()

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////
            ' STEP 1: Make feature layers using the MakeFeatureLayer tool for the inputs to the SelectByLocation tool.
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////

            ' Initialize the Geoprocessor 
            Dim GP As Geoprocessor = New Geoprocessor

            ' Set the OverwriteOutput setting to True
            GP.OverwriteOutput = True

            ' Initialize the MakeFeatureLayer tool
            Dim makefeaturelayer As MakeFeatureLayer = New MakeFeatureLayer()
            makefeaturelayer.in_features = "C:\data\nfld.gdb\wells"
            makefeaturelayer.out_layer = "Wells_Lyr"
            RunTool(GP, makefeaturelayer, Nothing)

            makefeaturelayer.in_features = "C:\data\nfld.gdb\bedrock"
            makefeaturelayer.out_layer = "bedrock_Lyr"
            RunTool(GP, makefeaturelayer, Nothing)

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ' STEP 2: Execute SelectLayerByLocation using the feature layers to select all wells that intersect the bedrock geology.
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ' Initialize the SelectLayerByLocation tool
            Dim SelectByLocation As SelectLayerByLocation = New SelectLayerByLocation()

            SelectByLocation.in_layer = "Wells_Lyr"
            SelectByLocation.select_features = "bedrock_Lyr"
            SelectByLocation.overlap_type = "INTERSECT"
            RunTool(GP, SelectByLocation, Nothing)

            '///////////////////////////////////////////////////////////////////////////////////////////////
            ' STEP 3: Execute SelectLayerByAttribute to select all wells that have a well yield > 150 L/min.
            '///////////////////////////////////////////////////////////////////////////////////////////////

            ' Initialize the SelectLayerByAttribute tool
            Dim SelectByAttribute As SelectLayerByAttribute = New SelectLayerByAttribute()

            SelectByAttribute.in_layer_or_view = "Wells_Lyr"
            SelectByAttribute.selection_type = "NEW_SELECTION"
            SelectByAttribute.where_clause = "WELL_YIELD > 150"
            RunTool(GP, SelectByAttribute, Nothing)

            '//////////////////////////////////////////////////////////////////////////////////////////////////////
            ' STEP 4: Execute CopyFeatures tool to create a new feature class of wells with well yield > 150 L/min.
            '//////////////////////////////////////////////////////////////////////////////////////////////////////

            ' Initialize the CopyFeatures tool
            Dim copy_features As ESRI.ArcGIS.DataManagementTools.CopyFeatures = New ESRI.ArcGIS.DataManagementTools.CopyFeatures()

            copy_features.in_features = "Wells_Lyr"
            copy_features.out_feature_class = "C:\data\nfld.gdb\high_yield_wells"

            '' Set the output Coordinate System environment
            'GP.SetEnvironmentValue("outputCoordinateSystem", "C:\Program Files\ArcGIS\Desktop10.0\Coordinate Systems\Projected Coordinate Systems\UTM\NAD 1983\NAD 1983 UTM Zone 21N.prj")

            RunTool(GP, copy_features, Nothing)

        End Sub


        Private Shared Sub RunTool(ByVal geoprocessor As Geoprocessor, ByVal process As IGPProcess, ByVal TC As ITrackCancel)

            ' Set the overwrite output option to true
            geoprocessor.OverwriteOutput = True

            Try
                geoprocessor.Execute(process, Nothing)
                ReturnMessages(geoprocessor)

            Catch err As Exception
                Console.WriteLine(err.Message)
                ReturnMessages(geoprocessor)
            End Try
        End Sub

        ' Function for returning the tool messages.
        Private Shared Sub ReturnMessages(ByVal gp As Geoprocessor)
            ' Print out the messages from tool executions
            Dim Count As Integer
            If gp.MessageCount > 0 Then
                For Count = 0 To gp.MessageCount - 1
                    Console.WriteLine(gp.GetMessage(Count))
                Next
            End If
        End Sub

    End Class

End Namespace
