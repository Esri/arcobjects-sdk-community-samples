using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace RasterSyncExtensionReg
{
	/// <summary>
	/// A static class with a Main method for registering the RasterSyncExtension.RasterSyncWorkspaceExtension class.
	/// </summary>
	public class RegisterExtension
	{
		/// <summary>
		/// A path to the geodatabase's connection file.
		/// </summary>
		private static readonly String workspacePath = @"C:\MyGeodatabase.sde";
		
		/// <summary>
		/// The type of geodatabase the extension is being registered with.
		/// </summary>
		private static readonly GeodatabaseType geodatabaseType = GeodatabaseType.ArcSDE;

		/// <summary>
		/// The GUID of the workspace extension to register.
		/// </summary>
		private static readonly String extensionGuid = "{97CD2883-37CB-4f76-BD0F-945279C783DC}";

		public static void Main(string[] args)
		{
			#region Licensing
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
			IAoInitialize aoInitialize = new AoInitializeClass();
			esriLicenseStatus licenseStatus = aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
			if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
			{
				MessageBox.Show("An Advanced License could not be checked out.");
				return;
			}
			#endregion

			try
			{
				// Open the workspace.
				IWorkspaceFactory workspaceFactory = null;
				switch (geodatabaseType)
				{
					case GeodatabaseType.ArcSDE:
						workspaceFactory = new SdeWorkspaceFactoryClass();
						break;
					case GeodatabaseType.FileGDB:
						workspaceFactory = new FileGDBWorkspaceFactoryClass();
						break;
					case GeodatabaseType.PersonalGDB:
						workspaceFactory = new AccessWorkspaceFactoryClass();
						break;
				}
				IWorkspace workspace = workspaceFactory.OpenFromFile(workspacePath, 0);
				IWorkspaceExtensionManager workspaceExtensionManager = (IWorkspaceExtensionManager)workspace;

				// Create a UID for the workspace extension.
				UID uid = new UIDClass();
				uid.Value = extensionGuid;

                // Determine whether there are any existing geodatabase-register extensions.
				// To disambiguate between GDB-register extensions and component category extensions,
				// check the extension count of a new scratch workspace.
				IScratchWorkspaceFactory scratchWorkspaceFactory = new FileGDBScratchWorkspaceFactoryClass();
				IWorkspace scratchWorkspace = scratchWorkspaceFactory.CreateNewScratchWorkspace();
				IWorkspaceExtensionManager scratchExtensionManager = (IWorkspaceExtensionManager)scratchWorkspace;
				Boolean workspaceExtensionApplied = false;
				UID gdbRegisteredUID = null;
				try
				{
					workspaceExtensionApplied = (workspaceExtensionManager.ExtensionCount > scratchExtensionManager.ExtensionCount);
				}
				catch (COMException comExc)
				{
					// This is necessary in case the existing extension could not be initiated.
					if (comExc.ErrorCode == (int)fdoError.FDO_E_WORKSPACE_EXTENSION_CREATE_FAILED)
					{
						// Parse the error message for the current extension's GUID.
						Regex regex = new Regex("(?<guid>{[^}]+})");
						MatchCollection matchCollection = regex.Matches(comExc.Message);
						if (matchCollection.Count > 0)
						{
							Match match = matchCollection[0];
							gdbRegisteredUID = new UIDClass();
							gdbRegisteredUID.Value = match.Groups["guid"].Value;
							workspaceExtensionApplied = true;
						}
						else
						{
							throw comExc;
						}
					}
					else
					{
						throw comExc;
					}
				}

				if (workspaceExtensionApplied)
				{
					if (gdbRegisteredUID == null)
					{
						// There is GDB-registered extension on the SDE workspace. Find the SDE extension that is not
						// applied to the scratch workspace. 
						for (int i = 0; i < workspaceExtensionManager.ExtensionCount; i++)
						{
							IWorkspaceExtension workspaceExtension = workspaceExtensionManager.get_Extension(i);
							IWorkspaceExtension scratchExtension = scratchExtensionManager.FindExtension(workspaceExtension.GUID);
							if (scratchExtension == null)
							{
								gdbRegisteredUID = workspaceExtension.GUID;
							}
						}
					}
				}

				// If the extension could be located, remove it.
				if (gdbRegisteredUID != null)
				{
					workspaceExtensionManager.UnRegisterExtension(gdbRegisteredUID);
				}

				// Register the extension.
				workspaceExtensionManager.RegisterExtension("RasterSyncExtension.RasterSyncWorkspaceExtension", uid);
			}
			catch (COMException comExc)
			{
				switch (comExc.ErrorCode)
				{
					case (int)fdoError.FDO_E_WORKSPACE_EXTENSION_NO_REG_PRIV:
						MessageBox.Show("The connection file's privileges are insufficient to perform this operation.",
							"Register Workspace Extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					case (int)fdoError.FDO_E_WORKSPACE_EXTENSION_CREATE_FAILED:
						String createErrorMessage = String.Concat("The workspace extension could not be created.",
							Environment.NewLine, "Ensure that it has been registered for COM Interop.");
						MessageBox.Show(createErrorMessage, "Register Workspace Extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					case (int)fdoError.FDO_E_WORKSPACE_EXTENSION_DUP_GUID:
					case (int)fdoError.FDO_E_WORKSPACE_EXTENSION_DUP_NAME:
						String dupErrorMessage = String.Concat("A duplicate name or GUID was detected. Make sure any existing GDB-registered",
							"workspaces are not component category-registered as well.");
						MessageBox.Show(dupErrorMessage, "Register Workspace Extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					default:
						String otherErrorMessage = String.Format("An unexpected error has occurred:{0}{1}{2}", Environment.NewLine, comExc.Message,
							comExc.ErrorCode);
						MessageBox.Show(otherErrorMessage);
						break;
				}
			}
			catch (Exception exc)
			{
				String errorMessage = String.Format("An unexpected error has occurred:{0}{1}", Environment.NewLine, exc.Message);
				MessageBox.Show(errorMessage);
			}

			// Shutdown the AO initializer.
			aoInitialize.Shutdown();
		}
	}

	/// <summary>
	/// An enumeration containing geodatabase types.
	/// </summary>
	public enum GeodatabaseType
	{
		/// <summary>
		/// ArcSDE geodatabases.
		/// </summary>
		ArcSDE = 0,
		/// <summary>
		/// File geodatabases.
		/// </summary>
		FileGDB = 1,
		/// <summary>
		/// Personal geodatabases.
		/// </summary>
		PersonalGDB = 2
	}
}
