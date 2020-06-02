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
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ConversionTools

Namespace GPFieldMapping

    Friend Class FieldMapping

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
            RunGPFieldMappinig()

            ' Shutdown application
            aoLicenseInitializer.ShutdownApplication()

        End Sub



        Private Shared Sub RunGPFieldMappinig()
            ' Initialize the Geoprocessor
            Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor()
            GP.OverwriteOutput = True

            ' Create the GPUtilites object
            Dim gputilities As IGPUtilities = New GPUtilitiesClass()

            ' Create a DETable data element object 
            Dim inputTableA As IDETable = CType(gputilities.MakeDataElement("C:\data\citiblocks.gdb\census", Nothing, Nothing), IDETable)

            ' Create an array of input tables
            Dim inputtables As IArray = New ArrayClass()
            inputtables.Add(inputTableA)

            ' Initialize the GPFieldMapping
            Dim fieldmapping As IGPFieldMapping = New GPFieldMappingClass()
            fieldmapping.Initialize(inputtables, Nothing)

            ' Create a new output field
            Dim trackidfield As IFieldEdit = New FieldClass()
            trackidfield.Name_2 = "TRACTID"
            trackidfield.Type_2 = esriFieldType.esriFieldTypeString
            trackidfield.Length_2 = 50

            ' Create a new FieldMap
            Dim trackid As IGPFieldMap = New GPFieldMapClass()
            trackid.OutputField = trackidfield

            ' Find field map "STFID" containing the input field "STFID". Add input field to the new field map.
            Dim fieldmap_index As Integer = fieldmapping.FindFieldMap("STFID")
            Dim stfid_fieldmap As IGPFieldMap = fieldmapping.GetFieldMap(fieldmap_index)
            Dim field_index As Integer = stfid_fieldmap.FindInputField(inputTableA, "STFID")
            Dim inputField As IField = stfid_fieldmap.GetField(field_index)
            trackid.AddInputField(inputTableA, inputField, 5, 10)

            ' Add the new field map to the field mapping
            fieldmapping.AddFieldMap(trackid)

            ' Execute Table to Table tool using the FieldMapping
            Dim tblTotbl As TableToTable = New TableToTable()
            tblTotbl.in_rows = inputTableA
            tblTotbl.out_path = "C:\data\citiblocks.gdb"
            tblTotbl.out_name = "census_out"
            tblTotbl.field_mapping = fieldmapping

            Dim sev As Object = Nothing
            Try
                GP.Execute(tblTotbl, Nothing)
                System.Windows.Forms.MessageBox.Show(GP.GetMessages(sev))
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(GP.GetMessages(sev))
            End Try
        End Sub

    End Class

End Namespace
