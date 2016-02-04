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
''' <summary>
''' A static class with a Main method for registering the RasterSyncExtension.RasterSyncWorkspaceExtension class.
''' </summary>
Module RegisterExtension

	''' <summary>
	''' A path to the geodatabase's connection file.
	''' </summary>
	Private ReadOnly workspacePath As String = "C:\MyGeodatabase.sde"

	''' <summary>
	''' The type of geodatabase the extension will be applied to.
	''' </summary>
	Private ReadOnly geodatabaseType As GeodatabaseTypes = GeodatabaseTypes.ArcSDE

	''' <summary>
	''' The GUID of the workspace extension to register.
	''' </summary>
	Private ReadOnly extensionGuid As String = "{A9FC5EB2-33F9-4941-AE69-775ECF688647}"

	Sub Main()
        ' Licensing.
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
		Dim aoInitialize As IAoInitialize = New AoInitializeClass()
		Dim licenseStatus As esriLicenseStatus = aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
		If licenseStatus <> esriLicenseStatus.esriLicenseCheckedOut Then
			MessageBox.Show("An Advanced License could not be checked out.")
			Return
		End If

		Try
			' Open the workspace.
			Dim workspaceFactory As IWorkspaceFactory = Nothing
			Select Case geodatabaseType
				Case GeodatabaseTypes.ArcSDE
					workspaceFactory = New SdeWorkspaceFactory
				Case GeodatabaseTypes.FileGDB
					workspaceFactory = New FileGDBWorkspaceFactory
				Case GeodatabaseTypes.PersonalGDB
					workspaceFactory = New AccessWorkspaceFactory
			End Select
			Dim workspace As IWorkspace = workspaceFactory.OpenFromFile(workspacePath, 0)
			Dim workspaceExtensionManager As IWorkspaceExtensionManager = CType(workspace, IWorkspaceExtensionManager)

			' Create a UID for the workspace extension.
			Dim uid As UID = New UIDClass()
			uid.Value = extensionGuid

            ' Determine whether there are any existing geodatabase-register extensions.
			' To disambiguate between GDB-register extensions and component category extensions,
			' check the extension count of a new scratch workspace.
			Dim scratchWorkspaceFactory As IScratchWorkspaceFactory = New FileGDBScratchWorkspaceFactoryClass()
			Dim scratchWorkspace As IWorkspace = scratchWorkspaceFactory.CreateNewScratchWorkspace()
			Dim scratchExtensionManager As IWorkspaceExtensionManager = CType(scratchWorkspace, IWorkspaceExtensionManager)
			Dim workspaceExtensionApplied As Boolean = False
			Dim gdbRegisteredUID As UID = Nothing
			Try
				workspaceExtensionApplied = (workspaceExtensionManager.ExtensionCount > scratchExtensionManager.ExtensionCount)
			Catch comExc As COMException
				' This is necessary in case the existing extension could not be initiated.
				If comExc.ErrorCode = fdoError.FDO_E_WORKSPACE_EXTENSION_CREATE_FAILED Then
					' Parse the error message for the current extension's GUID.
					Dim regex As Regex = New Regex("(?<guid>{[^}]+})")
					Dim matchCollection As MatchCollection = regex.Matches(comExc.Message)
					If matchCollection.Count > 0 Then
						Dim match As Match = matchCollection(0)
						gdbRegisteredUID = New UIDClass()
						gdbRegisteredUID.Value = match.Groups("guid").Value
						workspaceExtensionApplied = True
					Else
						Throw comExc
					End If
				Else
					Throw comExc
				End If
			End Try

			If workspaceExtensionApplied Then
				If gdbRegisteredUID Is Nothing Then
					' There is GDB-registered extension on the SDE workspace. Find the SDE extension that is not
					' applied to the scratch workspace. 
					Dim i As Integer = 0
					For i = 0 To workspaceExtensionManager.ExtensionCount - 1
						Dim workspaceExtension As IWorkspaceExtension = workspaceExtensionManager.Extension(i)
						Dim scratchExtension As IWorkspaceExtension = scratchExtensionManager.FindExtension(workspaceExtension.GUID)
						If scratchExtension Is Nothing Then
							gdbRegisteredUID = workspaceExtension.GUID
						End If
					Next i
				End If

				' If the extension could be located, remove it.
				If Not gdbRegisteredUID Is Nothing Then
					workspaceExtensionManager.UnRegisterExtension(gdbRegisteredUID)
				End If
			End If

			' Register the extension.
			workspaceExtensionManager.RegisterExtension("RasterSyncExtensionVB.RasterSyncWorkspaceExtension", uid)
		Catch comExc As COMException
			Select Case comExc.ErrorCode
				Case fdoError.FDO_E_WORKSPACE_EXTENSION_NO_REG_PRIV
					MessageBox.Show("The connection file's privileges are insufficient to perform this operation.", _
					 "Register Workspace Extension", MessageBoxButtons.OK, MessageBoxIcon.Error)
				Case fdoError.FDO_E_WORKSPACE_EXTENSION_CREATE_FAILED
					Dim createErrorMessage As String = String.Concat("The workspace extension could not be created.", _
					 Environment.NewLine, "Ensure that it has been registered for COM Interop.")
					MessageBox.Show(createErrorMessage, "Register Workspace Extension", MessageBoxButtons.OK, MessageBoxIcon.Error)
				Case fdoError.FDO_E_WORKSPACE_EXTENSION_DUP_GUID
				Case fdoError.FDO_E_WORKSPACE_EXTENSION_DUP_NAME
					Dim dupErrorMessage As String = String.Concat("A duplicate name or GUID was detected. Make sure any existing GDB-registered", _
					 "workspaces are not component category-registered as well.")
					MessageBox.Show(dupErrorMessage, "Register Workspace Extension", MessageBoxButtons.OK, MessageBoxIcon.Error)
				Case Else
					Dim otherErrorMessage As String = String.Format("An unexpected error has occurred:{0}{1}{2}", Environment.NewLine, comExc.Message, _
					 comExc.ErrorCode)
					MessageBox.Show(otherErrorMessage)
			End Select
		Catch exc As Exception
			Dim errorMessage As String = String.Format("An unexpected error has occurred:{0}{1}", Environment.NewLine, exc.Message)
			MessageBox.Show(errorMessage)
		End Try

		' Shutdown the AO initializer.
		aoInitialize.Shutdown()
	End Sub

	Public Enum GeodatabaseTypes
		ArcSDE = 0
		FileGDB = 1
		PersonalGDB = 2
	End Enum

End Module
