Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports NDVIFunction.CustomFunction
Imports System.Runtime.InteropServices

<Guid("6DAD598C-D4D5-466A-9754-BE78CA4C41ED")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("CustomFunctionUI.NDVICustomFunctionUIClass")> _
<ComVisible(True)> _
Public Class NDVICustomFunctionUIClass
    Implements IComPropertyPage
	#Region "Private Members"
	Private myForm As NDVICustomFunctionUIForm
	' The UI form object.
	Private myArgs As INDVICustomFunctionArguments
	' The NDVI Custom function arguments object.
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
	Private myBandIndicesVar As IRasterFunctionVariable
	' Variable for the Band Indices property.
	#End Region
	Public Sub New()
		myForm = New NDVICustomFunctionUIForm()
		myArgs = Nothing
        myPriority = 100
		myPageSite = Nothing
		myHelpFile = ""
        mySupportedID = New UID()
		' The UID of the NDVICustomFunction object.
		mySupportedID.Value = "{" & "652642F3-9106-4EB3-9262-A4C39E03BC56" & "}"
		templateMode = False

		myRasterVar = Nothing
		myBandIndicesVar = Nothing
	End Sub

	#Region "IComPropertyPage Members"

	''' <summary>
	''' Activate the form. 
	''' </summary>
	''' <returns>Handle to the form</returns>
	Public Function Activate() As Integer Implements IComPropertyPage.Activate
		If templateMode Then
			' In template mode, set the form values using the RasterFunctionVariables
			myBandIndicesVar = Nothing
			myForm.InputRaster = myRasterVar
			myForm.BandIndices = DirectCast(myBandIndicesVar.Value, String)
		Else
			' Otherwise use the arguments object to update the form values.
			myForm.InputRaster = myArgs.Raster
			myForm.BandIndices = myArgs.BandIndices
		End If
		myForm.UpdateUI()
		myForm.Activate()
		Return myForm.Handle.ToInt32()
	End Function

	''' <summary>
	''' Check if the form is applicable to the given set of objects. In this case
	''' only the Raster Function object is used to check compatibility.
	''' </summary>
	''' <param name="objects">Set of object to check against.</param>
	''' <returns>Flag to specify whether the form is applicable.</returns>
	Public Function Applies(objects As ESRI.ArcGIS.esriSystem.ISet) As Boolean Implements IComPropertyPage.Applies
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
				myBandIndicesVar.Value = myForm.BandIndices
				' Then set the variables on the arguments object.
				Dim rasterFunctionArgs As IRasterFunctionArguments = DirectCast(myArgs, IRasterFunctionArguments)
				rasterFunctionArgs.PutValue("BandIndices", myBandIndicesVar)
				rasterFunctionArgs.PutValue("Raster", myRasterVar)
			ElseIf myArgs IsNot Nothing Then
				' If not in template mode, update the arguments object
				' with the values from the form.
				myArgs.BandIndices = myForm.BandIndices
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
		Set
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
		Set
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
	Public Sub SetObjects(objects As ESRI.ArcGIS.esriSystem.ISet) Implements IComPropertyPage.SetObjects
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
						myArgs = New NDVICustomFunctionArguments()
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
                                    myRasterVar = New RasterFunctionVariable()
									myRasterVar.Value = defaultRaster
									myRasterVar.Name = "InputRaster"
									myRasterVar.IsDataset = True
								End If
							End If

							' Create a variable for the BandIndices property.
                            myBandIndicesVar = New RasterFunctionVariable()
							myBandIndicesVar.Name = "BandIndices"
							' Use the default value from the arguments object
							myBandIndicesVar.Value = myArgs.BandIndices

							' Set the variables created as properties on the arguments object.
							Dim rasterFunctionArgs As IRasterFunctionArguments = DirectCast(myArgs, IRasterFunctionArguments)
							rasterFunctionArgs.PutValue("Raster", myRasterVar)
								'#End Region
							rasterFunctionArgs.PutValue("BandIndices", myBandIndicesVar)
							'#End Region
						End If
					Else
						'#Region "Edit Mode"
						' Get the arguments object from the property set.
						myArgs = DirectCast(functionArgument, INDVICustomFunctionArguments)
						' Check if the form is in template mode.
						templateMode = CBool(uiParameters.GetProperty("RFxTemplateEditMode"))
						If templateMode Then
							'#Region "Edit Template"
							' In template edit mode, the variables from the arguments object
							' are extracted.
							Dim rasterFunctionArgs As IRasterFunctionArguments = DirectCast(myArgs, IRasterFunctionArguments)
							Dim raster As Object = rasterFunctionArgs.GetValue("Raster")

							' Create or Open the Raster variable.
							If TypeOf raster Is IRasterFunctionVariable Then
								myRasterVar = DirectCast(raster, IRasterFunctionVariable)
							Else
                                myRasterVar = New RasterFunctionVariable()
								myRasterVar.Name = "InputRaster"
								myRasterVar.Value = raster
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
		Set
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
            Throw New NotImplementedException()
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
