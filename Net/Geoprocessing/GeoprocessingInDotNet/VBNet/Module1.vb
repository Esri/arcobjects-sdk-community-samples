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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geoprocessing

Module Module1

    Private m_AOLicenseInitializer As LicenseInitializer = New GeoprocessingInDotNet2008.LicenseInitializer()
    <STAThread()> _
    Sub Main()

        ' Load the product code and version to the version manager
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)

        'ESRI License Initializer generated code.
        m_AOLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeAdvanced}, _
        New esriLicenseExtensionCode() {})

        ' Create geoprocessor. Overwrite true will replace existing output
        Dim gp As IGeoProcessor2 = New GeoProcessor()
        gp.OverwriteOutput = True

        ' Get the workspace from the user
        Console.WriteLine("Enter the path to folder where you copied the data folder.")
        Console.WriteLine("Example: C:\AirportsAndGolf\data")
        Console.Write(">")
        Dim wks As String = Console.ReadLine()

        ' Set the workspace to the value user entered
        gp.SetEnvironmentValue("workspace", wks + "\golf.gdb")

        ' Add the custom toolbox to geoprocessor
        gp.AddToolbox(wks + "\Find Golf Courses.tbx")

        ' Create a variant - data are in the workspace
        Dim parameters As IVariantArray = New VarArray()
        parameters.Add("Airports")
        parameters.Add("8 Miles")
        parameters.Add("Golf")
        parameters.Add("GolfNearAirports")

        Dim sev As Object = Nothing

        Try
            gp.Execute("GolfFinder", parameters, Nothing)
            Console.WriteLine(gp.GetMessages(sev))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Dim errorMsgs As String = gp.GetMessages(sev)
            Console.WriteLine(errorMsgs)
        Finally
            Console.WriteLine("Hit Enter to quit")
            Console.ReadLine()    ' pause the console to see messages
        End Try

        'ESRI License Initializer generated code.
        'Do not make any call to ArcObjects after ShutDownApplication()
        m_AOLicenseInitializer.ShutdownApplication()
    End Sub

End Module
