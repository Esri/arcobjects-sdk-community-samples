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
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports CustomFunction.CustomFunction


<Guid("7441106F-73A4-4fa2-90BC-232BCEE1CEBF")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("CustomFunctionUI.WatermarkFunctionUIClass")> _
<ComVisible(True)> _
Public Class WatermarkFunctionUIClass
    Implements IComPropertyPage
#Region "Private Members"
    Private myForm As WatermarkFunctionUIForm
    ' The UI form object.
    Private myArgs As IWatermarkFunctionArguments
    ' The watermark function arguments object.
    Private myPriority As Integer
    ' Priority for the UI page.
    Private myPageSite As IComPropertyPageSite
    Private myHelpFile As String
    ' Location for the help file if needed.
    Private mySupportedID As UID
    ' UID for the Raster function supported by the property page.
    Private templateMode As Boolean
    ' Flag to specify template mode.
    Private isFormReadOnly As Boolean
    ' Flag to specify whether the UI is in Read-Only Mode.
    Private myRasterVar As IRasterFunctionVariable
    ' Variable for the Raster property.
    Private myWatermarkLocationVar As IRasterFunctionVariable
    ' Variable for the WatermarkLocation property.
    Private myBlendPercentageVar As IRasterFunctionVariable
    ' Variable for the BlendPercentage property.
    Private myWatermarkImagePathVar As IRasterFunctionVariable
    ' Variable for WatermarkImagePath property.
#End Region
    Public Sub New()
        myForm = New WatermarkFunctionUIForm()
        myArgs = Nothing
        myPriority = 1000
        myPageSite = Nothing
        myHelpFile = ""
        mySupportedID = New UIDClass()
        mySupportedID.Value = "{" & "168721E7-7010-4a36-B886-F644437B164D" & "}"
        templateMode = False
        isFormReadOnly = False

        myRasterVar = Nothing
        myBlendPercentageVar = Nothing
        myWatermarkLocationVar = Nothing
        myWatermarkImagePathVar = Nothing
    End Sub

#Region "IComPropertyPage Members"
    ''' <summary>
    ''' Activate the form. 
    ''' </summary>
    ''' <returns>Handle to the form</returns>
    Public Function Activate() As Integer Implements IComPropertyPage.Activate
        If templateMode Then
            ' In template mode, set the form values using the RasterFunctionVariables
            myForm.BlendPercentage = CDbl(myBlendPercentageVar.Value)
            myForm.WatermarkImagePath = DirectCast(myWatermarkImagePathVar.Value, String)
            myForm.WatermarkLocation = CType(myWatermarkLocationVar.Value, esriWatermarkLocation)
            myForm.InputRaster = myRasterVar
        Else
            ' Otherwise use the arguments object to update the form values.
            myForm.WatermarkLocation = myArgs.WatermarkLocation
            myForm.BlendPercentage = myArgs.BlendPercentage
            myForm.WatermarkImagePath = myArgs.WatermarkImagePath
            myForm.InputRaster = myArgs.Raster
        End If
        myForm.UpdateUI()
        myForm.Activate()
        Return myForm.Handle.ToInt32()
    End Function

    ''' <summary>
    ''' Check if the form is applicable to the given set of objects. In this case
    ''' only the Raster function object is used to check compatibility.
    ''' </summary>
    ''' <param name="objects">Set of object to check against.</param>
    ''' <returns>Flag to specify whether the form is applicable.</returns>
    Public Function Applies(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) As Boolean Implements IComPropertyPage.Applies
        objects.Reset()
        For i As Integer = 0 To objects.Count - 1
            Dim currObject As Object = objects.[Next]()
            If TypeOf currObject Is IRasterFunction Then
                Dim rasterFunction As IRasterFunction = DirectCast(currObject, IRasterFunction)
                If TypeOf rasterFunction Is IPersistVariant Then
                    Dim myVariantObject As IPersistVariant = DirectCast(rasterFunction, IPersistVariant)
                    ' Compare the ID from the function object with the ID's supported by this UI page.
                    If myVariantObject.ID.Compare(mySupportedID) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Apply the properties set in the form to the arguments object.
    ''' </summary>
    Public Sub Apply() Implements IComPropertyPage.Apply
        If Not isFormReadOnly Then
            ' If the form is read only, do not update.
            If templateMode Then
                ' If in template mode, use the values from the page to
                ' update the variables.
                If myForm.InputRaster IsNot Nothing Then
                    If TypeOf myForm.InputRaster Is IRasterFunctionVariable Then
                        myRasterVar = DirectCast(myForm.InputRaster, IRasterFunctionVariable)
                    Else
                        myRasterVar.Value = myForm.InputRaster
                    End If
                End If
                myBlendPercentageVar.Value = myForm.BlendPercentage
                myWatermarkImagePathVar.Value = myForm.WatermarkImagePath
                myWatermarkLocationVar.Value = myForm.WatermarkLocation
                ' Then set the variables on the arguments object.
                Dim rasterFunctionArgs As IRasterFunctionArguments = DirectCast(myArgs, IRasterFunctionArguments)
                rasterFunctionArgs.PutValue("Raster", myRasterVar)
                rasterFunctionArgs.PutValue("BlendPercentage", myBlendPercentageVar)
                rasterFunctionArgs.PutValue("WatermarkLocation", myWatermarkLocationVar)
                rasterFunctionArgs.PutValue("WatermarkImagePath", myWatermarkImagePathVar)
            ElseIf myArgs IsNot Nothing Then
                ' If not in template mode, update the arguments object
                ' with the values from the form.
                myArgs.BlendPercentage = myForm.BlendPercentage
                myArgs.WatermarkLocation = myForm.WatermarkLocation
                myArgs.WatermarkImagePath = myForm.WatermarkImagePath
                If myForm.InputRaster IsNot Nothing Then
                    myArgs.Raster = myForm.InputRaster
                End If
            End If
        End If

        myForm.IsFormDirty = False
    End Sub

    ''' <summary>
    ''' Do not set any properties set in the form
    ''' </summary>
    Public Sub Cancel() Implements IComPropertyPage.Cancel
        myForm.IsFormDirty = False
    End Sub

    ''' <summary>
    ''' Shut down the form and destroy the object.
    ''' </summary>
    Public Sub Deactivate() Implements IComPropertyPage.Deactivate
        myForm.Close()
        myForm.Dispose()
        myForm = Nothing
    End Sub

    ''' <summary>
    ''' Return the height of the form.
    ''' </summary>
    Public ReadOnly Property Height() As Integer Implements IComPropertyPage.Height
        Get
            Return myForm.Height
        End Get
    End Property

    ''' <summary>
    ''' Returns the path to the helpfile associated with the form.
    ''' </summary>
    Public ReadOnly Property HelpFile() As String Implements IComPropertyPage.HelpFile
        Get
            Return myHelpFile
        End Get
    End Property

    ''' <summary>
    ''' Hide the form.
    ''' </summary>
    Public Sub Hide() Implements IComPropertyPage.Hide
        myForm.Hide()
    End Sub

    ''' <summary>
    ''' Flag to specify if the form has been changed.
    ''' </summary>
    Public ReadOnly Property IsPageDirty() As Boolean Implements IComPropertyPage.IsPageDirty
        Get
            Return myForm.IsFormDirty
        End Get
    End Property

    ''' <summary>
    ''' Set the pagesite for the form.
    ''' </summary>
    Public WriteOnly Property PageSite() As IComPropertyPageSite Implements IComPropertyPage.PageSite
        Set(ByVal value As IComPropertyPageSite)
            myPageSite = value
        End Set
    End Property

    ''' <summary>
    ''' Get or set the priority for the form.
    ''' </summary>
    Public Property Priority() As Integer Implements IComPropertyPage.Priority
        Get
            Return myPriority
        End Get
        Set(ByVal value As Integer)
            myPriority = value
        End Set
    End Property

    ''' <summary>
    ''' Set the necessary objects required for the form. In this case
    ''' the form is given an arguments object in edit mode, or is required 
    ''' to create one in create mode. After getting or creating the arguments
    ''' object, template mode is checked for and handled. The template mode 
    ''' requires all parameters of the arguments object to converted to variables.
    ''' </summary>
    ''' <param name="objects">Set of objects required for the form.</param>
    Public Sub SetObjects(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) Implements IComPropertyPage.SetObjects
        Try
            ' Recurse through the objects
            objects.Reset()
            For i As Integer = 0 To objects.Count - 1
                Dim currObject As Object = objects.[Next]()
                ' Find the properties to be set.
                If TypeOf currObject Is IPropertySet Then
                    Dim uiParameters As IPropertySet = DirectCast(currObject, IPropertySet)
                    Dim names As Object = Nothing, values As Object = Nothing
                    uiParameters.GetAllProperties(names, values)

                    Dim disableForm As Boolean = False
                    Try
                        disableForm = Convert.ToBoolean(uiParameters.GetProperty("RFxPropPageIsReadOnly"))
                    Catch generatedExceptionName As Exception
                    End Try

                    If disableForm Then
                        isFormReadOnly = True
                    Else
                        isFormReadOnly = False
                    End If

                    ' Check if the arguments object exists in the property set.
                    Dim functionArgument As Object = Nothing
                    Try
                        functionArgument = uiParameters.GetProperty("RFxArgument")
                    Catch generatedExceptionName As Exception
                    End Try
                    ' If not, the form is in create mode.
                    If functionArgument Is Nothing Then
                        '#Region "Create Mode"
                        ' Create a new arguments object.
                        myArgs = New WatermarkFunctionArguments()
                        ' Create a new property and set the arguments object on it.
                        uiParameters.SetProperty("RFxArgument", myArgs)
                        ' Check if a default raster is supplied.
                        Dim defaultRaster As Object = Nothing
                        Try
                            defaultRaster = uiParameters.GetProperty("RFxDefaultInputRaster")
                        Catch generatedExceptionName As Exception
                        End Try
                        If defaultRaster IsNot Nothing Then
                            ' If it is, set it to the raster property.
                            myArgs.Raster = defaultRaster
                        End If
                        ' Check if the form is in template mode.
                        templateMode = CBool(uiParameters.GetProperty("RFxTemplateEditMode"))
                        If templateMode Then
                            ' Since we are in create mode already, new variables have to be 
                            ' created for each property of the arguments object.
                            '#Region "Create Variables"
                            If defaultRaster IsNot Nothing Then
                                ' If a default raster is supplied and it is a variable,
                                ' there is no need to create one.
                                If TypeOf defaultRaster Is IRasterFunctionVariable Then
                                    myRasterVar = DirectCast(defaultRaster, IRasterFunctionVariable)
                                Else
                                    ' Create variable object for the InputRaster property.
                                    myRasterVar = New RasterFunctionVariableClass()
                                    myRasterVar.Value = defaultRaster
                                    myRasterVar.Name = "InputRaster"
                                    myRasterVar.IsDataset = True
                                End If
                            End If

                            ' Create a variable for the BlendPercentage property.
                            myBlendPercentageVar = New RasterFunctionVariableClass()
                            myBlendPercentageVar.Name = "BlendPercentage"
                            ' Use the default value from the arguments object
                            myBlendPercentageVar.Value = myArgs.BlendPercentage

                            ' Create a variable for the WatermarkLocation property.
                            myWatermarkLocationVar = New RasterFunctionVariableClass()
                            myWatermarkLocationVar.Name = "WatermarkLocation"
                            ' Use the default value from the arguments object
                            myWatermarkLocationVar.Value = myArgs.WatermarkLocation

                            ' Create a variable for the WatermarkImagePath property.
                            myWatermarkImagePathVar = New RasterFunctionVariableClass()
                            myWatermarkImagePathVar.Name = "WatermarkImagePath"
                            ' Use the default value from the arguments object
                            myWatermarkImagePathVar.Value = myArgs.WatermarkImagePath

                            ' Set the variables created as properties on the arguments object.
                            Dim rasterFunctionArgs As IRasterFunctionArguments = DirectCast(myArgs, IRasterFunctionArguments)
                            rasterFunctionArgs.PutValue("Raster", myRasterVar)
                            rasterFunctionArgs.PutValue("BlendPercentage", myBlendPercentageVar)
                            rasterFunctionArgs.PutValue("WatermarkLocation", myWatermarkLocationVar)
                            '#End Region
                            rasterFunctionArgs.PutValue("WatermarkImagePath", myWatermarkImagePathVar)
                            '#End Region
                        End If
                    Else
                        '#Region "Edit Mode"
                        ' Get the arguments object from the property set.
                        myArgs = DirectCast(functionArgument, IWatermarkFunctionArguments)
                        ' Check if the form is in template mode.
                        templateMode = CBool(uiParameters.GetProperty("RFxTemplateEditMode"))
                        If templateMode Then
                            '#Region "Edit Template"
                            ' In template edit mode, the variables from the arguments object
                            ' are extracted.
                            Dim rasterFunctionArgs As IRasterFunctionArguments = DirectCast(myArgs, IRasterFunctionArguments)
                            Dim raster As Object = rasterFunctionArgs.GetValue("Raster")
                            Dim blendPercentage As Object = rasterFunctionArgs.GetValue("BlendPercentage")
                            Dim watermarkLocation As Object = rasterFunctionArgs.GetValue("WatermarkLocation")
                            Dim watermarkPath As Object = rasterFunctionArgs.GetValue("WatermarkImagePath")
                            ' Create or Open the Raster variable.
                            If TypeOf raster Is IRasterFunctionVariable Then
                                myRasterVar = DirectCast(raster, IRasterFunctionVariable)
                            Else
                                myRasterVar = New RasterFunctionVariableClass()
                                myRasterVar.Name = "InputRaster"
                                myRasterVar.Value = raster
                            End If
                            ' Create or Open the BlendPercentage variable.
                            If TypeOf blendPercentage Is IRasterFunctionVariable Then
                                myBlendPercentageVar = DirectCast(blendPercentage, IRasterFunctionVariable)
                            Else
                                myBlendPercentageVar = New RasterFunctionVariableClass()
                                myBlendPercentageVar.Name = "BlendPercentage"
                                myBlendPercentageVar.Value = blendPercentage
                            End If
                            ' Create or Open the WatermarkLocation variable.
                            If TypeOf blendPercentage Is IRasterFunctionVariable Then
                                myWatermarkLocationVar = DirectCast(watermarkLocation, IRasterFunctionVariable)
                            Else
                                myWatermarkLocationVar = New RasterFunctionVariableClass()
                                myWatermarkLocationVar.Name = "WatermarkLocation"
                                myWatermarkLocationVar.Value = watermarkLocation
                            End If
                            ' Create or Open the WatermarkImagePath variable.
                            If TypeOf watermarkPath Is IRasterFunctionVariable Then
                                myWatermarkImagePathVar = DirectCast(watermarkPath, IRasterFunctionVariable)
                            Else
                                myWatermarkImagePathVar = New RasterFunctionVariableClass()
                                myWatermarkImagePathVar.Name = "WatermarkImagePath"
                                myWatermarkImagePathVar.Value = watermarkPath
                                '#End Region
                            End If
                            '#End Region
                        End If
                    End If
                End If
            Next
        Catch exc As Exception
            Dim errorMsg As String = exc.Message
        End Try
    End Sub

    ''' <summary>
    ''' Show the form.
    ''' </summary>
    Public Sub Show() Implements IComPropertyPage.Show
        If isFormReadOnly Then
            myForm.Enabled = False
        Else
            myForm.Enabled = True
        End If
        myForm.Show()
    End Sub

    ''' <summary>
    ''' Get or set the title of the form
    ''' </summary>
    Public Property Title() As String Implements IComPropertyPage.Title
        Get
            Return myForm.Text
        End Get
        Set(ByVal value As String)
            myForm.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Get the width of the form.
    ''' </summary>
    Public ReadOnly Property Width() As Integer Implements IComPropertyPage.Width
        Get
            Return myForm.Width
        End Get
    End Property

    ''' <summary>
    ''' Return the help context ID of the form if it exists.
    ''' </summary>
    ''' <param name="controlID">Control ID for the sheet.</param>
    ''' <returns>The context ID.</returns>
    Public ReadOnly Property HelpContextID(ByVal controlID As Integer) As Integer Implements IComPropertyPage.HelpContextID
        Get
            Return 0
        End Get
    End Property
#End Region

#Region "COM Registration Function(s)"
    ''' <summary>
    ''' Register the Property Page with the Raster Function Property Pages
    ''' </summary>
    ''' <param name="regKey">Key to register.</param>
    <ComRegisterFunction()> _
    Private Shared Sub Reg(ByVal regKey As String)
        RasterFunctionPropertyPages.Register(regKey)
    End Sub

    ''' <summary>
    ''' Unregister the Property Page with the Raster Function Property Pages
    ''' </summary>
    ''' <param name="regKey">Key to unregister.</param>
    <ComUnregisterFunction()> _
    Private Shared Sub Unreg(ByVal regKey As String)
        RasterFunctionPropertyPages.Unregister(regKey)
    End Sub
#End Region
End Class
