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

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ArcDataBinding
Namespace DemoITableBinding

    '<summary>
    'This class demonstrates how to bind an ITable to Windows Forms controls.
    '</summary>
    '<remarks>
    'The form this class defines contains a DataGridView, a TextBox and a 
    'BindingNavigator. The class will load the test data and bind it to both
    'the TextBox and the DataGridView by using a BindingSource. The use of a
    'BindingSource allows us to bind the test data to a single object that will
    'act as a datasource for the grid, the text box and the navigation control.
    'This allows changes made in one control to be reflected in the other controls
    'with minimal coding effort. This is most clearly demonstrated by using the
    'navigation control to step backwards and forwards through the grid rows
    'while also showing the value of a single field in the current row in the
    'text box.
    '</remarks>
    Partial Public Class MainWnd

        Inherits Form

        '  Private Shared m_AOLicenseInitializer As LicenseInitializer = New DemoITableBinding.LicenseInitializer()

        <STAThread()> Shared Sub Main()
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)

            Dim m_AOLicenseInitializer As LicenseInitializer = New DemoITableBinding.LicenseInitializer()

            'ESRI License Initializer generated code.
            If (Not m_AOLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB, esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced}, _
            New esriLicenseExtensionCode() {})) Then
                MsgBox(m_AOLicenseInitializer.LicenseMessage() + vbNewLine + vbNewLine _
                + "This application could not initialize with the correct ArcGIS license and will shutdown.")
                m_AOLicenseInitializer.ShutdownApplication()
                Return
            End If


            Application.Run(New MainWnd())
            'ESRI License Initializer generated code.
            'Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication()
        End Sub
        '<summary>
        'This is used to bind our ITable to the binding source. We need to keep
        'a reference to it as we will need to re-attach it to the binding source
        'to force a refresh whenever we change from displaying coded value domain
        'values to displaying their text equivalents and vice versa.
        '</summary>
        Private tableWrapper As ArcDataBinding.ArcDataBinding.TableWrapper

        '<summary>
        'This binding object sets the data member within the data source for the 
        'text box. We need to keep a reference to this as it needs to be reset
        'whenever viewing of coded value domains is changed.
        '</summary>
        Private txtBoxBinding As Binding

        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub

        Private Sub MainWnd_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' Get workspace and open mdb file
            Dim wkspcFactory As IWorkspaceFactory2 = DirectCast(New FileGDBWorkspaceFactoryClass, IWorkspaceFactory2)
            Dim filePath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            filePath = System.IO.Path.Combine (filePath, "ArcGIS\data\SanFrancisco\SanFrancisco.gdb")
            Dim wkspc As IFeatureWorkspace = DirectCast(wkspcFactory.OpenFromFile(filePath, Handle.ToInt32), IFeatureWorkspace)

            'Open the Geodatabase table
            Dim foundITable As ITable = TryCast(wkspc.OpenFeatureClass("Parks"), ITable)

            If (Not (foundITable) Is Nothing) Then
                ' Bind dataset to the binding source
                tableWrapper = New ArcDataBinding.ArcDataBinding.TableWrapper(foundITable)
                Me.bindingSource1.DataSource = tableWrapper
                ' Bind binding source to grid. You could also bind TableWrapper
                ' directly to the grid.
                Me.dataGridView1.DataSource = Me.bindingSource1

                ' Bind binding source to text box, we are binding the NAME
                ' field.
                txtBoxBinding = New Binding("Text", Me.bindingSource1, "NAME")
                textBox1.DataBindings.Add(txtBoxBinding)
            End If
        End Sub

        Private Sub chkUseCVD_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUseCVD.CheckedChanged
            ' Set usage of CV domain on or off
            tableWrapper.UseCVDomains = chkUseCVD.Checked
            ' Refresh the binding source by setting it to null and then rebinding
            ' to the data. This will refresh all the field types and ensures that all
            ' the fields are using the correct type converters (we will need different
            ' type converters depending on whether we are showing cv domain values or
            ' their text equivalents). Note that as we will be setting the binding source
            ' to null, the text box binding will fail as the "FULL_NAME" field will no
            ' longer be present. To prevent any problems here, we need to remove and
            ' reset the text box's binding.
            textBox1.DataBindings.Clear()
            bindingSource1.DataSource = Nothing
            bindingSource1.DataSource = tableWrapper
            textBox1.DataBindings.Add(txtBoxBinding)
            dataGridView1.Refresh()
        End Sub
    End Class
End Namespace
