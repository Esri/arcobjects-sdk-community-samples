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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace GPCalculateArea
    Public Class CalculateAreaFunction : Implements IGPFunction2


        ' Local members
        Private m_ToolName As String = "CalculateArea" 'Function Name
        Private m_MetaDataFile As String = "CalculateArea_area.xml" 'Metadata file
        Private m_Parameters As IArray ' Array of Parameters
        Private m_GPUtilities As New GPUtilities ' GPUtilities object


#Region "IGPFunction2 Members"

        ' Set the name of the function tool. 
        ' This name appears when executing the tool at the command line or in scripting. 
        ' This name should be unique to each toolbox and must not contain spaces.
        Public ReadOnly Property Name() As String Implements IGPFunction2.Name
            Get
                Return m_ToolName
            End Get
        End Property

        ' Set the function tool Display Name as seen in ArcToolbox.
        Public ReadOnly Property DisplayName() As String Implements IGPFunction2.DisplayName
            Get
                Return "Calculate Area"
            End Get
        End Property

        ' This is the location where the parameters to the Function Tool are defined. 
        ' This property returns an IArray of parameter objects (IGPParameter). 
        ' These objects define the characteristics of the input and output parameters. 
        Public ReadOnly Property ParameterInfo() As IArray Implements IGPFunction2.ParameterInfo
            Get
                'Array to the hold the parameters
                Dim pParameters As IArray = New ArrayClass()

                'Input DataType is GPFeatureLayerType
                Dim inputParameter As IGPParameterEdit3 = New GPParameterClass()
                inputParameter.DataType = New GPFeatureLayerTypeClass()

                ' Default Value object is DEFeatureClass
                inputParameter.Value = New GPFeatureLayerClass()

                ' Set Input Parameter properties
                inputParameter.Direction = esriGPParameterDirection.esriGPParameterDirectionInput
                inputParameter.DisplayName = "Input Features"
                inputParameter.Name = "input_features"
                inputParameter.ParameterType = esriGPParameterType.esriGPParameterTypeRequired
                pParameters.Add(inputParameter)

                ' Area field parameter
                inputParameter = New GPParameterClass()
                inputParameter.DataType = New GPStringTypeClass()

                ' Value object is GPString
                Dim gpStringValue As IGPString = New GPStringClass()
                gpStringValue.Value = "Area"
                inputParameter.Value = CType(gpStringValue, IGPValue)

                ' Set field name parameter properties
                inputParameter.Direction = esriGPParameterDirection.esriGPParameterDirectionInput
                inputParameter.DisplayName = "Area Field Name"
                inputParameter.Name = "field_name"
                inputParameter.ParameterType = esriGPParameterType.esriGPParameterTypeRequired

                pParameters.Add(inputParameter)

                ' Output parameter (Derived) and data type is DEFeatureClass
                Dim outputParameter As IGPParameterEdit3 = New GPParameterClass()
                outputParameter.DataType = New GPFeatureLayerTypeClass()

                ' Value object is DEFeatureClass
                outputParameter.Value = New DEFeatureClass()

                'Create a new feature schema object
                Dim featureSchema As IGPFeatureSchema
                featureSchema = New GPFeatureSchema
                Dim schema As IGPSchema
                schema = CType(featureSchema, IGPSchema)

                'Clone the dependency
                schema.CloneDependency = True

                ' Set output parameter properties
                outputParameter.Direction = esriGPParameterDirection.esriGPParameterDirectionOutput
                outputParameter.DisplayName = "Output FeatureClass"
                outputParameter.Name = "out_featureclass"
                outputParameter.ParameterType = esriGPParameterType.esriGPParameterTypeDerived
                outputParameter.Schema = schema
                outputParameter.AddDependency("input_features")
                pParameters.Add(outputParameter)

                Return pParameters
            End Get
        End Property

        '   Validate is an IGPFunction method, and we need to implement it in case there
        '   is legacy code that queries for the IGPFunction interface instead of the IGPFunction2 interface.  
        '   This Validate code is boilerplate - copy and insert into any IGPFunction2 code.
        '   This is the calling sequence that the gp framework now uses when it QI's for IGPFunction2..
        Public Function Validate(ByVal paramvalues As IArray, ByVal updateValues As Boolean, ByVal envMgr As IGPEnvironmentManager) As IGPMessages Implements IGPFunction2.Validate

            If m_Parameters Is Nothing Then
                m_Parameters = ParameterInfo()
            End If

            ' Call UpdateParameters only if updatevalues is true
            If updateValues = True Then
                UpdateParameters(paramvalues, envMgr)
            End If

            ' Call InternalValidate (Basic Validation). Are all the required parameters supplied?
            ' Are the Values to the parameters the correct data type?
            Dim validateMsgs As IGPMessages
            validateMsgs = m_GPUtilities.InternalValidate(m_Parameters, paramvalues, updateValues, True, envMgr)

            ' Call UpdateMessages()
            UpdateMessages(paramvalues, envMgr, validateMsgs)

            ' Return the messages
            Return validateMsgs
        End Function

        ' This method will update the output parameter value with the additional area field.
        Public Sub UpdateParameters(ByVal paramvalues As ESRI.ArcGIS.esriSystem.IArray, ByVal pEnvMgr As ESRI.ArcGIS.Geoprocessing.IGPEnvironmentManager) Implements ESRI.ArcGIS.Geoprocessing.IGPFunction2.UpdateParameters
            m_Parameters = paramvalues

            ' Retrieve the input parameter value
            Dim parameterValue As IGPValue
            parameterValue = m_GPUtilities.UnpackGPValue(m_Parameters.Element(0))

            ' Get the derived output feature class schema and empty the additional fields.
            ' This will ensure you don't get duplicate entries
            Dim derivedFeatures As IGPParameter3
            derivedFeatures = CType(paramvalues.Element(2), IGPParameter3)

            Dim schema As IGPFeatureSchema
            schema = CType(derivedFeatures.Schema, IGPFeatureSchema)
            schema.AdditionalFields = Nothing

            ' If we have an input value, create a new field based on the field name the user entered
            If parameterValue.IsEmpty() = False Then
                Dim fieldNameParameter As IGPParameter3
                fieldNameParameter = CType(paramvalues.Element(1), IGPParameter3)

                Dim fieldName As String
                fieldName = fieldNameParameter.Value.GetAsText()

                ' Check if the user's entered value already exists
                Dim areaField As IField
                areaField = m_GPUtilities.FindField(parameterValue, fieldName)

                If areaField Is Nothing Then
                    Dim fieldsEdit As IFieldsEdit
                    fieldsEdit = New Fields
                    Dim fieldEdit As IFieldEdit
                    fieldEdit = New Field

                    fieldEdit.Name_2 = fieldName
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble

                    fieldsEdit.AddField(fieldEdit)

                    ' Add an additional field for the area values to the derived output
                    Dim pFields As IFields
                    pFields = fieldsEdit
                    schema.AdditionalFields = pFields
                End If
            End If


        End Sub

        ' Called after returning from the internal validation routine. You can examine the messages created from internal validation and change them if desired. 
        Public Sub UpdateMessages(ByVal paramvalues As ESRI.ArcGIS.esriSystem.IArray, ByVal pEnvMgr As ESRI.ArcGIS.Geoprocessing.IGPEnvironmentManager, ByVal Messages As ESRI.ArcGIS.Geodatabase.IGPMessages) Implements ESRI.ArcGIS.Geoprocessing.IGPFunction2.UpdateMessages

            ' Check for error messages
            Dim msg As IGPMessage
            msg = CType(Messages, IGPMessage)

            If msg.IsError() Then
                Return
            End If

            ' Get the first input parameter
            Dim parameter As IGPParameter
            parameter = CType(paramvalues.Element(0), IGPParameter)

            ' UnPackGPValue. This ensures you get the value either from 
            ' the DataElement or from GPVaraible (ModelBuilder)
            Dim parameterValue As IGPValue
            parameterValue = m_GPUtilities.UnpackGPValue(parameter)

            ' Open the Input Dataset - use DecodeFeatureLayer as the input might be
            ' a layer file or a feature layer from ArcMap
            Dim inputFeatureClass As IFeatureClass = Nothing
            Dim qf As IQueryFilter = Nothing
            m_GPUtilities.DecodeFeatureLayer(parameterValue, inputFeatureClass, qf)

            Dim fieldParameter As IGPParameter3
            fieldParameter = CType(paramvalues.Element(1), IGPParameter3)
            Dim fieldName As String
            fieldName = fieldParameter.Value.GetAsText()

            ' Check if the field already exists and provide a warning
            Dim indexA As Integer
            indexA = inputFeatureClass.FindField(fieldName)

            If indexA > 0 Then
                Messages.ReplaceWarning(1, "Field already exists. It will be overwritten.")
            End If

            Return

        End Sub

        ' Execute: Execute the function given the array of the parameters
        Public Sub Execute(ByVal paramvalues As IArray, ByVal trackcancel As ITrackCancel, ByVal envMgr As IGPEnvironmentManager, ByVal message As IGPMessages) Implements IGPFunction2.Execute

            ' Get the first Input Parameter
            Dim parameter As IGPParameter = CType(paramvalues.Element(0), IGPParameter)

            ' UnPackGPValue. This ensures you get the value either form the dataelement or GpVariable (ModelBuilder)
            Dim parameterValue As IGPValue = m_GPUtilities.UnpackGPValue(parameter)

            ' Open the Input Dataset - use DecodeFeatureLayer as the input might be
            ' a layer file or a feature layer from ArcMap
            Dim inputFeatureClass As IFeatureClass = Nothing
            Dim qf As IQueryFilter = Nothing
            m_GPUtilities.DecodeFeatureLayer(parameterValue, inputFeatureClass, qf)

            If inputFeatureClass Is Nothing Then
                message.AddError(2, "Could not open input dataset.")
                Return
            End If

            ' Add the field if it does not exist.
            Dim indexA As Integer
            parameter = CType(paramvalues.Element(1), IGPParameter)
            Dim sField As String = parameter.Value.GetAsText()

            indexA = inputFeatureClass.FindField(sField)
            If indexA < 0 Then
                Dim fieldEdit As IFieldEdit = New FieldClass()
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
                fieldEdit.Name_2 = sField
                message.AddMessage(sField)
                inputFeatureClass.AddField(fieldEdit)
            End If

            Dim featcount As Integer
            featcount = inputFeatureClass.FeatureCount(Nothing)

            ' Set the properties of the Step Progresson
            Dim pStepPro As IStepProgressor
            pStepPro = CType(trackcancel, IStepProgressor)
            pStepPro.MinRange = 0
            pStepPro.MaxRange = featcount
            pStepPro.StepValue = 1
            pStepPro.Message = "Calculate Area"
            pStepPro.Position = 0
            pStepPro.Show()

            ' Create an Update Cursor
            indexA = inputFeatureClass.FindField(sField)
            Dim updateCursor As IFeatureCursor = inputFeatureClass.Update(Nothing, False)
            Dim updateFeature As IFeature = updateCursor.NextFeature()
            Dim geometry As IGeometry
            Dim area As IArea
            Dim dArea As Double

            Do While Not updateFeature Is Nothing
                geometry = updateFeature.Shape
                area = CType(geometry, IArea)
                dArea = area.Area
                updateFeature.Value(indexA) = dArea
                updateCursor.UpdateFeature(updateFeature)
                updateFeature.Store()
                updateFeature = updateCursor.NextFeature()
                pStepPro.Step()
            Loop

            pStepPro.Hide()

            ' Release the update cursor to remove the lock on the input data.
            System.Runtime.InteropServices.Marshal.ReleaseComObject(updateCursor)

        End Sub

        ' This is the function name object for the Geoprocessing Function Tool. 
        ' This name object is created and returned by the Function Factory.
        ' The Function Factory must first be created before implementing this property.
        Public ReadOnly Property FullName() As IName Implements IGPFunction2.FullName
            Get
                ' Add CalculateArea.FullName getter implementation
                Dim functionFactory As IGPFunctionFactory = New CalculateAreaFunctionFactory()
                'INSTANT VB NOTE: The local variable name was renamed since Visual Basic will not uniquely identify class members when local variables have the same name:
                Return CType(functionFactory.GetFunctionName(m_ToolName), IName)
            End Get
        End Property

        ' This is used to set a custom renderer for the output of the Function Tool.
        Public Function GetRenderer(ByVal pParam As IGPParameter) As Object Implements IGPFunction2.GetRenderer
            Return Nothing
        End Function

        ' This is the unique context identifier in a [MAP] file (.h). 
        ' ESRI Knowledge Base article #27680 provides more information about creating a [MAP] file. 
        Public ReadOnly Property HelpContext() As Integer Implements IGPFunction2.HelpContext
            Get
                Return 0
            End Get
        End Property

        ' This is the path to a .chm file which is used to describe and explain the function and its operation. 
        Public ReadOnly Property HelpFile() As String Implements IGPFunction2.HelpFile
            Get
                Return ""
            End Get
        End Property

        ' This is used to return whether the function tool is licensed to execute.
        Public Function IsLicensed() As Boolean Implements IGPFunction2.IsLicensed
            Return True
        End Function

        ' This is the name of the (.xml) file containing the default metadata for this function tool. 
        ' The metadata file is used to supply the parameter descriptions in the help panel in the dialog. 
        ' If no (.chm) file is provided, the help is based on the metadata file. 
        ' ESRI Knowledge Base article #27000 provides more information about creating a metadata file.
        Public ReadOnly Property MetadataFile() As String Implements IGPFunction2.MetadataFile

            ' if you just return the name of an *.xml file as follows:
            ' Get
            '   return m_MetaDataFile
            ' End Get
            ' then the metadata file will be created 
            ' in the default location - <install directory>\help\gp

            ' alternatively, you can send the *.xml file to the location of the DLL.
            ' 
            Get
                Dim filePath As String, fileLocation As String
                fileLocation = System.Reflection.Assembly.GetExecutingAssembly().Location
                filePath = System.IO.Path.GetDirectoryName(fileLocation)
                filePath = System.IO.Path.Combine(filePath, m_MetaDataFile)
                Return filePath
            End Get
        End Property

        ' This is the class id used to override the default dialog for a tool. 
        ' By default, the Toolbox will create a dialog based upon the parameters returned 
        ' by the ParameterInfo property.
        Public ReadOnly Property DialogCLSID() As UID Implements IGPFunction2.DialogCLSID
            Get
                Return Nothing
            End Get
        End Property
#End Region

#Region "IGPFunction Members"

        Public Function GetRenderer1(ByVal pParam As ESRI.ArcGIS.Geoprocessing.IGPParameter) As Object Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.GetRenderer
            Return Nothing
        End Function

        Public ReadOnly Property ParameterInfo1() As ESRI.ArcGIS.esriSystem.IArray Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.ParameterInfo
            Get
                Return ParameterInfo()
            End Get
        End Property

        Public ReadOnly Property DialogCLSID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.DialogCLSID
            Get
                Return DialogCLSID
            End Get
        End Property

        Public ReadOnly Property DisplayName1() As String Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.DisplayName
            Get
                Return DisplayName
            End Get
        End Property

        Public Sub Execute1(ByVal paramvalues As ESRI.ArcGIS.esriSystem.IArray, ByVal trackcancel As ESRI.ArcGIS.esriSystem.ITrackCancel, ByVal envMgr As ESRI.ArcGIS.Geoprocessing.IGPEnvironmentManager, ByVal message As ESRI.ArcGIS.Geodatabase.IGPMessages) Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.Execute
            Call Execute(paramvalues, trackcancel, envMgr, message)
        End Sub

        Public ReadOnly Property FullName1() As ESRI.ArcGIS.esriSystem.IName Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.FullName
            Get
                FullName1 = FullName
            End Get
        End Property

        Public ReadOnly Property HelpContext1() As Integer Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.HelpContext
            Get
                Return HelpContext
            End Get
        End Property

        Public ReadOnly Property HelpFile1() As String Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.HelpFile
            Get
                Return HelpFile
            End Get
        End Property

        Public Function IsLicensed1() As Boolean Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.IsLicensed
            Return IsLicensed()
        End Function

        Public ReadOnly Property MetadataFile1() As String Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.MetadataFile
            Get
                Return MetadataFile
            End Get
        End Property

        Public ReadOnly Property Name1() As String Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.Name
            Get
                Return Name
            End Get
        End Property

        Public Function Validate1(ByVal paramvalues As ESRI.ArcGIS.esriSystem.IArray, ByVal updateValues As Boolean, ByVal envMgr As ESRI.ArcGIS.Geoprocessing.IGPEnvironmentManager) As ESRI.ArcGIS.Geodatabase.IGPMessages Implements ESRI.ArcGIS.Geoprocessing.IGPFunction.Validate
            Return Validate(paramvalues, updateValues, envMgr)
        End Function
#End Region
    End Class
    '////////////////////////////
    ' Function Factory Class
    '//////////////////////////
    <Guid("2554BFC7-94F9-4d28-B3FE-14D17599B35A"), ComVisible(True)> _
    Public Class CalculateAreaFunctionFactory : Implements IGPFunctionFactory
        Private m_GPFunction As IGPFunction

        ' Register the Function Factory with the ESRI Geoprocessor Function Factory Component Category.
#Region "Component Category Registration"
        <ComRegisterFunction()> _
        Private Shared Sub Reg(ByVal regKey As String)
            GPFunctionFactories.Register(regKey)
        End Sub

        <ComUnregisterFunction()> _
        Private Shared Sub Unreg(ByVal regKey As String)
            GPFunctionFactories.Unregister(regKey)
        End Sub
#End Region

        ' Utility Function added to create the function names.
        Private Function CreateGPFunctionNames(ByVal index As Long) As IGPFunctionName

            Dim functionName As IGPFunctionName = New GPFunctionNameClass()
            'INSTANT VB NOTE: The local variable name was renamed since Visual Basic will not uniquely identify class members when local variables have the same name:
            Dim name_Renamed As IGPName

            Select Case index
                Case (0)
                    name_Renamed = CType(functionName, IGPName)
                    name_Renamed.Category = "AreaCalculation"
                    name_Renamed.Description = "Calculate Area for FeatureClass"
                    name_Renamed.DisplayName = "Calculate Area"
                    name_Renamed.Name = "CalculateArea"
                    name_Renamed.Factory = Me
            End Select

            Return functionName
        End Function

        ' Implementation of the Function Factory
#Region "IGPFunctionFactory Members"

        ' This is the name of the function factory. 
        ' This is used when generating the Toolbox containing the function tools of the factory.
        Public ReadOnly Property Name() As String Implements IGPFunctionFactory.Name
            Get
                Return "AreaCalculation"
            End Get
        End Property

        ' This is the alias name of the factory.
        Public ReadOnly Property [Alias]() As String Implements IGPFunctionFactory.Alias
            Get
                Return "area"
            End Get
        End Property

        ' This is the class id of the factory. 
        Public ReadOnly Property CLSID() As UID Implements IGPFunctionFactory.CLSID
            Get
                Dim id As UID = New UIDClass()
                id.Value = Me.GetType().GUID.ToString("B")
                Return id
            End Get
        End Property

        ' This method will create and return a function object based upon the input name.
        Public Function GetFunction(ByVal Name As String) As IGPFunction Implements IGPFunctionFactory.GetFunction
            Select Case Name
                Case ("CalculateArea")
                    m_GPFunction = New CalculateAreaFunction()
            End Select

            Return m_GPFunction
        End Function

        ' This method will create and return a function name object based upon the input name.
        Public Function GetFunctionName(ByVal Name As String) As IGPName Implements IGPFunctionFactory.GetFunctionName
            Dim gpName As IGPName = New GPFunctionNameClass()

            Select Case Name
                Case ("CalculateArea")
                    Return CType(CreateGPFunctionNames(0), IGPName)
            End Select
            Return gpName
        End Function

        ' This method will create and return an enumeration of function names that the factory supports.
        Public Function GetFunctionNames() As IEnumGPName Implements IGPFunctionFactory.GetFunctionNames
            ' Add CalculateFunctionFactory.GetFunctionNames implementation
            Dim nameArray As IArray = New EnumGPNameClass()
            nameArray.Add(CreateGPFunctionNames(0))
            Return CType(nameArray, IEnumGPName)
        End Function

        ' This method will create and return an enumeration of GPEnvironment objects. 
        ' If tools published by this function factory required new environment settings, 
        'then you would define the additional environment settings here. 
        ' This would be similar to how parameters are defined. 
        Public Function GetFunctionEnvironments() As IEnumGPEnvironment Implements IGPFunctionFactory.GetFunctionEnvironments
            Return Nothing
        End Function

#End Region
    End Class

End Namespace
