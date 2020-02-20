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
Imports System.IO
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Server
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports System.Collections.Generic
Imports System.Net
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Web
''' <summary>
''' 1. Description:
''' This sample demonstrate how to create an image service and set configurations based on data source types (raster dataset, mosaic dataset, raster layer); it also have additional features to start,stop,delete image service programmatically.
''' The user running this utility needs access to data source.
''' The application can be run locally on AGSServer machine or remotely.
''' Source data must be accessible by the account that runs ArcGIS Server. Data is not copied to ArcGIS Server through this tool.
''' CachingScheme/Metadata/thumbnail/iteminfo can't be defined through this tool.
''' CachingScheme can be done through Caching geoprocessing tools. REST Metadata/thumbnail/iteminfo are not available through this app but can be developed using a similar approach to server admin endpoint
''' The sample uses Raster API to populate dataset properties (used to construct service configuration), and can eitehr directly invoke rest Admin API to create service; or using AGSClient to create service.
''' 2. Case sensitivity:  
''' (1) switches are case sensitive. 
''' (2) when publish a service, the service name is case sensitive
''' 3. Usage:
''' Run from command line environment. Usage. <>: required parameter; |: pick one.
''' isconfig -o publish -h <host_adminurl> -d <datapath> -n <serviceName>
''' isconfig -o <delete|start|stop|pause> -h <host> -n <serviceName>
''' isconfig -o <list> -h <host>
''' Example 1: isconfig -o publish -h "http://host:6080/arcgis/admin" -u "adminuser" -p "adminpassword" -d \\myserver\data\test.gdb\mdtest -n mdtest
''' Example 2: isconfig -o stop -h myservername -u "adminuser" -p "adminpassword" -n mdtest
''' Example 3: isconfig -o list -h myservername -u "adminuser" -p "adminpassword"
''' </summary>
Class ISConfig
	#Region "static variables"
	Private Shared sourcePath As String = ""
	'data source path: a raster dataset, a mosaic dataset
	Private Shared restAdmin As String = ""
	'host admin url e.g. http://host:6080/arcgis/admin
	Private Shared serviceName As String = ""
	'image service name        
	Private Shared rasterDataset As IRasterDataset = Nothing
	Private Shared soAdmin As IServerObjectAdmin = Nothing
	Private Shared username As String = ""
	'user name for publisher/admin
	Private Shared password As String = ""
	'password for publisher/admin
	#End Region
	<STAThread> _
	Public Shared Sub Main(args As String())
		Try
            'args = {"-o", "publish", "-h", "http://server:6080/arcgis/admin", "-u", "adminuser", "-p", "adminpassword", "-d", "\\server\test\rgb.tif", "-n", "mdtest12356"}
            'args = {"-o", "list", "-h", "http://server:6080/arcgis/admin", "-u", "adminuser", "-p", "adminpassword"}
			'validation
			If Not ValidateParams(args) Then
				Return
			End If
			'license           
			If Not InitLicense() Then
				Return
			End If

			'retrieve parameters
			Retrieve_Params(args)
			Dim operation As String = args(1)
			Select Case operation.ToLower()
				Case "publish"
					'CreateISConfig();
					CreateISConfig_RESTAdmin()
					Exit Select
				Case "delete"
					'DeleteService();
					DeleteService_RESTAdmin()
					Exit Select
				Case "start"
					'StartService();
					StartService_RESTAdmin()
					Exit Select
				Case "stop"
					'StopService();
					StopService_RESTAdmin()
					Exit Select
				Case "pause"
					PauseService()
					Exit Select
				Case "list"
					'ListServices();
					ListServices_RESTAdmin()
					Exit Select
			End Select
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub


	#Region "management operations"
	''' <summary>
	''' create image service configuration
	''' </summary>
	Private Shared Sub CreateISConfig()
		Try
			If Not ConnectAGS(restAdmin) Then
				Return
			End If

			'get source type
			Dim sourceType As esriImageServiceSourceType = GetSourceType(sourcePath)

			'connect to ArcGIS Server and create configuration
			Dim soConfig As IServerObjectConfiguration5 = DirectCast(soAdmin.CreateConfiguration(), IServerObjectConfiguration5)

			'set general service parameters
			soConfig.Name = serviceName
			soConfig.TypeName = "ImageServer"
			soConfig.TargetCluster = "default"

			soConfig.StartupType = esriStartupType.esriSTAutomatic
			soConfig.IsPooled = True
			soConfig.IsolationLevel = esriServerIsolationLevel.esriServerIsolationHigh
			soConfig.MinInstances = 1
			soConfig.MaxInstances = 2

			'customize recycle properties
			Dim propertySet_Recycle As IPropertySet = soConfig.RecycleProperties
			propertySet_Recycle.SetProperty("Interval", "24")


			'path to the data
			Dim propertySet As IPropertySet = soConfig.Properties
			Dim workspace As IWorkspace = DirectCast(rasterDataset, IDataset).Workspace
			If workspace.WorkspaceFactory.WorkspaceType = esriWorkspaceType.esriRemoteDatabaseWorkspace Then
				Dim wsName2 As IWorkspaceName2 = TryCast(DirectCast(workspace, IDataset).FullName, IWorkspaceName2)
				Dim connString As String = wsName2.ConnectionString
				propertySet.SetProperty("ConnectionString", connString)
				propertySet.SetProperty("Raster", DirectCast(rasterDataset, IDataset).Name)
			Else
				propertySet.SetProperty("Path", sourcePath)
			End If
			propertySet.SetProperty("EsriImageServiceSourceType", sourceType.ToString())

			'MIME+URL (virtual directory)
			propertySet.SetProperty("SupportedImageReturnTypes", "MIME+URL")
			Dim dirs As IEnumServerDirectory = soAdmin.GetServerDirectories()
			dirs.Reset()
			Dim serverDir As IServerDirectory = dirs.[Next]()
			While serverDir IsNot Nothing
				If DirectCast(serverDir, IServerDirectory2).Type = esriServerDirectoryType.esriSDTypeOutput Then
					propertySet.SetProperty("OutputDir", serverDir.Path)
					propertySet.SetProperty("VirtualOutputDir", serverDir.URL)
					Exit While
				End If
				serverDir = dirs.[Next]()
			End While

			'copy right
			propertySet.SetProperty("CopyRight", "")

			'properties for a mosaic dataset;
			If sourceType = esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset Then
				Dim functionRasterDataset As IFunctionRasterDataset = DirectCast(rasterDataset, IFunctionRasterDataset)
				Dim propDefaults As IPropertySet = functionRasterDataset.Properties

				Dim names As Object, values As Object
				propDefaults.GetAllProperties(names, values)
				Dim propNames As New List(Of String)()
				propNames.AddRange(DirectCast(names, String()))
				If propNames.Contains("MaxImageHeight") Then
					propertySet.SetProperty("MaxImageHeight", propDefaults.GetProperty("MaxImageHeight"))
				End If
				'4100
				If propNames.Contains("MaxImageWidth") Then
					propertySet.SetProperty("MaxImageWidth", propDefaults.GetProperty("MaxImageWidth"))
				End If
				'15000
				If propNames.Contains("AllowedCompressions") Then
					propertySet.SetProperty("AllowedCompressions", propDefaults.GetProperty("AllowedCompressions"))
				End If
				'"None,JPEG,LZ77,LERC"
				If propNames.Contains("DefaultResamplingMethod") Then
					propertySet.SetProperty("DefaultResamplingMethod", propDefaults.GetProperty("DefaultResamplingMethod"))
				End If
				'0
				If propNames.Contains("DefaultCompressionQuality") Then
					propertySet.SetProperty("DefaultCompressionQuality", propDefaults.GetProperty("DefaultCompressionQuality"))
				End If
				'75
				If propNames.Contains("MaxRecordCount") Then
					propertySet.SetProperty("MaxRecordCount", propDefaults.GetProperty("MaxRecordCount"))
				End If
				'500
				If propNames.Contains("MaxMosaicImageCount") Then
					propertySet.SetProperty("MaxMosaicImageCount", propDefaults.GetProperty("MaxMosaicImageCount"))
				End If
				'20
				If propNames.Contains("MaxDownloadSizeLimit") Then
					propertySet.SetProperty("MaxDownloadSizeLimit", propDefaults.GetProperty("MaxDownloadSizeLimit"))
				End If
				'20
				If propNames.Contains("MaxDownloadImageCount") Then
					propertySet.SetProperty("MaxDownloadImageCount", propDefaults.GetProperty("MaxDownloadImageCount"))
				End If
				'20
				If propNames.Contains("AllowedFields") Then
					propertySet.SetProperty("AllowedFields", propDefaults.GetProperty("AllowedFields"))
				End If
				'"Name,MinPS,MaxPS,LowPS,HighPS,CenterX,CenterY"
				If propNames.Contains("AllowedMosaicMethods") Then
					propertySet.SetProperty("AllowedMosaicMethods", propDefaults.GetProperty("AllowedMosaicMethods"))
				End If
				'"Center,NorthWest,LockRaster,ByAttribute,Nadir,Viewpoint,Seamline"
				If propNames.Contains("AllowedItemMetadata") Then
					propertySet.SetProperty("AllowedItemMetadata", propDefaults.GetProperty("AllowedItemMetadata"))
				End If
				'"Full"
				If propNames.Contains("AllowedMensurationCapabilities") Then
					propertySet.SetProperty("AllowedMensurationCapabilities", propDefaults.GetProperty("AllowedMensurationCapabilities"))
				End If
				'"Full"
				If propNames.Contains("DefaultCompressionTolerance") Then
					propertySet.SetProperty("DefaultCompressionTolerance", propDefaults.GetProperty("DefaultCompressionTolerance"))
					'"0.01" LERC compression
					'propertySet.SetProperty("RasterFunctions", @"\\server\dir\rft1.rft.xml,\\server\dir\rft2.rft.xml");//"put raster function templates here, the first one is applied to exportimage request by default"
					'propertySet.SetProperty("RasterTypes", @"Raster Dataset,\\server\dir\art1.art.xml,\\server\dir\art2.art");//"put raster types here"
					'propertySet.SetProperty("DynamicImageWorkspace", @"\\server\dynamicImageDir"); //put the workspace that holds uploaded imagery here
					'propertySet.SetProperty("supportsOwnershipBasedAccessControl", true); //ownership based access control
					'propertySet.SetProperty("AllowOthersToUpdate", true); //allow others to update a catalog item
					'propertySet.SetProperty("AllowOthersToDelete", true); //allow others to delete a catalog item
					'propertySet.SetProperty("DownloadDir", ""); //put the download directory here
					'propertySet.SetProperty("VirutalDownloadDir", ""); //put the virtual download directory here
				End If
			Else
				propertySet.SetProperty("MaxImageHeight", 4100)
				propertySet.SetProperty("MaxImageWidth", 15000)
				propertySet.SetProperty("AllowedCompressions", "None,JPEG,LZ77")
				propertySet.SetProperty("DefaultResamplingMethod", 0)
				propertySet.SetProperty("DefaultCompressionQuality", 75)
				'for jpg compression
				propertySet.SetProperty("DefaultCompressionTolerance", 0.01)
				'for LERC compression                 
				'    rasterDataset = OpenRasterDataset(sourcePath);
				Dim measure As IMensuration = New MensurationClass()
				measure.Raster = DirectCast(rasterDataset, IRasterDataset2).CreateFullRaster()
				Dim mensurationCaps As String = ""
				If measure.CanMeasure Then
					mensurationCaps = "Basic"
				End If
				If measure.CanMeasureHeightBaseToTop Then
					mensurationCaps += ",Base-Top Height"
				End If
				If measure.CanMeasureHeightBaseToTopShadow Then
					mensurationCaps += ",Base-Top Shadow Height"
				End If
				If measure.CanMeasureHeightTopToTopShadow Then
					mensurationCaps += ",Top-Top Shadow Height"
				End If
					'set mensuration here
				propertySet.SetProperty("AllowedMensurationCapabilities", mensurationCaps)
			End If

			'not cached
			propertySet.SetProperty("IsCached", False)
			propertySet.SetProperty("IgnoreCache", True)
			propertySet.SetProperty("UseLocalCacheDir", False)
			propertySet.SetProperty("ClientCachingAllowed", False)

			'propertySet.SetProperty("DEM", ""); //put the elevation raster dataset or service for 3D mensuration, may need to add 3D to AllowedMensurationCapabilities

			'convert colormap to RGB or not
			propertySet.SetProperty("ColormapToRGB", False)

			'whether to return jpgs for all jpgpng request or not
			propertySet.SetProperty("ReturnJPGPNGAsJPG", False)

			'allow server to process client defined function
			propertySet.SetProperty("AllowFunction", True)
			'allow raster function

			'capabilities
			If sourceType = esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset Then
				soConfig.Info.SetProperty("Capabilities", "Image,Catalog,Metadata,Mensuration")
			Else
				'Full set: Image,Catalog,Metadata,Download,Pixels,Edit,Mensuration
				soConfig.Info.SetProperty("Capabilities", "Image,Metadata,Mensuration")
			End If

			'enable wcs, assume data has spatial reference
			soConfig.set_ExtensionEnabled("WCSServer", True)
			Dim wcsInfo As IPropertySet = New PropertySetClass()
			wcsInfo.SetProperty("WebEnabled", "true")
			soConfig.set_ExtensionInfo("WCSServer", wcsInfo)
			Dim propertySetWCS As IPropertySet = New PropertySetClass()
			propertySetWCS.SetProperty("CustomGetCapabilities", False)
			propertySetWCS.SetProperty("PathToCustomGetCapabilitiesFiles", "")
			soConfig.set_ExtensionProperties("WCSServer", propertySetWCS)

			'enable wms
			soConfig.set_ExtensionEnabled("WMSServer", True)
			Dim wmsInfo As IPropertySet = New PropertySetClass()
			wmsInfo.SetProperty("WebEnabled", "true")
			soConfig.set_ExtensionInfo("WMSServer", wmsInfo)
			Dim propertySetWMS As IPropertySet = New PropertySetClass()
			propertySetWMS.SetProperty("name", "WMS")
			'set other properties here
			soConfig.set_ExtensionProperties("WMSServer", propertySetWMS)


			'add configuration and start
			soAdmin.AddConfiguration(soConfig)
			soAdmin.StartConfiguration(serviceName, "ImageServer")

			If soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status = esriConfigurationStatus.esriCSStarted Then
				Console.WriteLine("{0} on {1} has been configured and started.", serviceName, restAdmin)
			Else
				Console.WriteLine("{0} on {1} was configured but can not be started, please investigate.", serviceName, restAdmin)
			End If

			If rasterDataset IsNot Nothing Then
				System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterDataset)
			End If
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub

	''' <summary>
	''' delete a service
	''' </summary>
	Private Shared Sub DeleteService()
		Try
			If Not ConnectAGS(restAdmin) Then
				Return
			End If
			If Not ValidateServiceName(soAdmin, serviceName, restAdmin) Then
				Return
			End If
			soAdmin.DeleteConfiguration(serviceName, "ImageServer")
			Console.WriteLine("{0} on {1} was deleted successfully.", serviceName, restAdmin)
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub

	''' <summary>
	''' start a service
	''' </summary>
	Private Shared Sub StartService()
		Try
			If Not ConnectAGS(restAdmin) Then
				Return
			End If
			If Not ValidateServiceName(soAdmin, serviceName, restAdmin) Then
				Return
			End If
			soAdmin.StartConfiguration(serviceName, "ImageServer")
			If soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status = esriConfigurationStatus.esriCSStarted Then
				Console.WriteLine("{0} on {1} was started successfully.", serviceName, restAdmin)
			Else
				Console.WriteLine("{0} on {1} couldn't be started, please investigate.", serviceName, restAdmin)
			End If
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub

	''' <summary>
	''' stop a service
	''' </summary>
	Private Shared Sub StopService()
		Try
			If Not ConnectAGS(restAdmin) Then
				Return
			End If
			If Not ValidateServiceName(soAdmin, serviceName, restAdmin) Then
				Return
			End If
			soAdmin.StopConfiguration(serviceName, "ImageServer")
			If soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status = esriConfigurationStatus.esriCSStopped Then
				Console.WriteLine("{0} on {1} was stopped successfully.", serviceName, restAdmin)
			Else
				Console.WriteLine("{0} on {1} couldn't be stopped, please investigate.", serviceName, restAdmin)
			End If
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub

	''' <summary>
	''' pause a service
	''' </summary>
	Private Shared Sub PauseService()
		Try
			If Not ConnectAGS(restAdmin) Then
				Return
			End If
			If Not ValidateServiceName(soAdmin, serviceName, restAdmin) Then
				Return
			End If
			If (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status = esriConfigurationStatus.esriCSStopped) Then
				Console.WriteLine("{0} on {1} is currently stopped --- not paused.", serviceName, restAdmin)
				Return
			End If
			soAdmin.PauseConfiguration(serviceName, "ImageServer")
			If soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status = esriConfigurationStatus.esriCSPaused Then
				Console.WriteLine("{0} on {1} was paused successfully.", serviceName, restAdmin)
			Else
				Console.WriteLine("{0} on {1} couldn't be paused, please investigate.", serviceName, restAdmin)
			End If
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub

	''' <summary>
	''' List services
	''' </summary>
	Private Shared Sub ListServices()
		Try
			If Not ConnectAGS(restAdmin) Then
				Return
			End If
			Dim enumConfigs As IEnumServerObjectConfiguration = soAdmin.GetConfigurations()
			enumConfigs.Reset()
			Dim soConfig As IServerObjectConfiguration = enumConfigs.[Next]()
			Console.WriteLine("ArcGIS Server {0} has the following image services:", restAdmin)
			While soConfig IsNot Nothing
				If soConfig.TypeName = "ImageServer" Then
					Console.WriteLine("{0}", soConfig.Name)
				End If
				soConfig = enumConfigs.[Next]()
			End While
		Catch exc As Exception
			Console.WriteLine("Error: {0}", exc.Message)
		End Try
	End Sub
	#End Region


	#Region "validation etc."
	''' <summary>
	''' connect to ags server
	''' </summary>
	''' <param name="host">host</param>
	''' <returns>true if connected</returns>
	Private Shared Function ConnectAGS(host As String) As Boolean
		Try
			Dim propertySet As IPropertySet = New PropertySetClass()
			propertySet.SetProperty("url", host)
			propertySet.SetProperty("ConnectionMode", esriAGSConnectionMode.esriAGSConnectionModePublisher)
			propertySet.SetProperty("ServerType", esriAGSServerType.esriAGSServerTypeDiscovery)
			propertySet.SetProperty("user", username)
			propertySet.SetProperty("password", password)
			propertySet.SetProperty("ALLOWINSECURETOKENURL", True)

			Dim connectName As IAGSServerConnectionName3 = TryCast(New AGSServerConnectionNameClass(), IAGSServerConnectionName3)
			connectName.ConnectionProperties = propertySet

			Dim agsAdmin As IAGSServerConnectionAdmin = TryCast(DirectCast(connectName, IName).Open(), IAGSServerConnectionAdmin)
			soAdmin = agsAdmin.ServerObjectAdmin
			Return True
		Catch exc As Exception
			Console.WriteLine("Error: Couldn't connect to AGSServer: {0}. Message: {1}", host, exc.Message)
			Return False
		End Try
	End Function

	''' <summary>
	''' Validate ServiceName
	''' </summary>
	''' <returns>Convert the config name to the correct case and returns true; if not exist in any cases, returns false </returns>
	Private Shared Function ValidateServiceName(soAdmin As IServerObjectAdmin, ByRef serviceName As String, host As String) As Boolean
		Dim enumConfigs As IEnumServerObjectConfiguration = soAdmin.GetConfigurations()
		enumConfigs.Reset()
		Dim soConfig As IServerObjectConfiguration = enumConfigs.[Next]()
		While soConfig IsNot Nothing
			If soConfig.Name.ToUpper() = serviceName.ToUpper() Then
				serviceName = soConfig.Name
				Return True
			End If
			soConfig = enumConfigs.[Next]()
		End While
		Console.WriteLine("Configuration {0} on {1} can not be found.", serviceName, host)
		Return False
	End Function

	''' <summary>
	''' Validate input parameters
	''' </summary>
	''' <param name="args">args</param>
	''' <returns>validation result</returns>
	Private Shared Function ValidateParams(args As String()) As Boolean
		'at least two params
		If args.Length = 0 Then
			ShowUsage()
			Console.WriteLine("press any key to continue ...")
			Console.ReadKey()
			Return False
		ElseIf args.Length < 2 Then
			' at least -o action
			ShowUsage()
			Return False
		End If

		' must start with -o
		Dim operations As String() = New String() {"publish", "delete", "start", "stop", "pause", "list"}
		If (Not args(0).StartsWith("-o")) OrElse (Not strInArray(args(1).ToLower(), operations)) Then
			Console.WriteLine("Incorrect operation")
			ShowUsage()
			Return False
		End If

		' for stop/start/pause/list, must contains "-n" and argument length is 4
		If (args(1).ToLower() = "stop") OrElse (args(1).ToLower() = "start") OrElse (args(1).ToLower() = "pause") OrElse (args(1).ToLower() = "delete") Then
			If Not strInArray("-h", args) Then
				Console.WriteLine("Missing host server -h")
				Return False
			End If
			If Not strInArray("-n", args) Then
				Console.WriteLine("Missing service name switch -n")
				Return False
			End If
			If Not strInArray("-u", args) Then
				Console.WriteLine("Missing admin/publisher username switch -u")
				Return False
			End If
			If Not strInArray("-p", args) Then
				Console.WriteLine("Missing admin/publisher name switch -p")
				Return False
				'if (args.Length > 8)
				'{
				'    Console.WriteLine("Too many arguments");
				'    return false;
				'}
			End If
		End If
		' for publish, must contains "-d" "-n" and argument length is 6
		If args(1).ToLower() = "publish" Then
			If Not strInArray("-d", args) Then
				Console.WriteLine("Missing data source switch -d")
				Return False
			End If
			If Not strInArray("-n", args) Then
				Console.WriteLine("Missing service name switch -n")
				Return False
			End If
			If Not strInArray("-u", args) Then
				Console.WriteLine("Missing admin/publisher username switch -u")
				Return False
			End If
			If Not strInArray("-p", args) Then
				Console.WriteLine("Missing admin/publisher name switch -p")
				Return False
				'if (args.Length > 12)
				'{
				'    Console.WriteLine("Too many arguments");
				'    return false;
				'}
			End If
		End If
		' validate each parameter: host, sourcepath, configname
		Dim parameters As String() = New String() {"-h", "-d", "-n", "-u", "-p"}
		For i As Integer = 2 To args.Length - 1
			Select Case args(i)
				Case "-h"
					If i = args.Length - 1 Then
						Console.WriteLine("Missing host parameter, switch -h")
						Return False
					ElseIf strInArray(args(i + 1), parameters) Then
						Console.WriteLine("Missing host parameter, switch -h")
						Return False
					End If
					i += 1
					Exit Select
				Case "-d"
					If i = args.Length - 1 Then
						Console.WriteLine("Missing data source parameter, switch -d")
						Return False
					ElseIf strInArray(args(i + 1), parameters) Then
						Console.WriteLine("Missing data source parameter, switch -d")
						Return False
					End If
					i += 1
					Exit Select
				Case "-n"
					If i = args.Length - 1 Then
						Console.WriteLine("Missing service name parameter, switch -n")
						Return False
					ElseIf strInArray(args(i + 1), parameters) Then
						Console.WriteLine("Missing service name parameter, switch -n")
						Return False
					End If
					i += 1
					Exit Select
				Case "-u"
					If i = args.Length - 1 Then
						Console.WriteLine("Missing admin/publisher username parameter, switch -u")
						Return False
					ElseIf strInArray(args(i + 1), parameters) Then
						Console.WriteLine("Missing admin/publisher username parameter, switch -u")
						Return False
					End If
					i += 1
					Exit Select
				Case "-p"
					If i = args.Length - 1 Then
						Console.WriteLine("Missing admin/publisher password parameter, switch -p")
						Return False
					ElseIf strInArray(args(i + 1), parameters) Then
						Console.WriteLine("Missing admin/publisher password parameter, switch -p")
						Return False
					End If
					i += 1
					Exit Select
				Case Else
					Console.WriteLine("Incorrect parameter switch: {0} is not a recognized.", args(i))
					Return False
			End Select
		Next
		Return True
	End Function

	''' <summary>
	''' string in array
	''' </summary>
	''' <param name="name"></param>
	''' <param name="nameArray"></param>
	''' <returns></returns>
	Private Shared Function strInArray(name As String, nameArray As String()) As Boolean
		For i As Integer = 0 To nameArray.Length - 1
			If nameArray(i) = name Then
				Return True
			End If
		Next
		Return False
	End Function

	''' <summary>
	''' initialize license
	''' </summary>
	''' <returns>status</returns>
	Private Shared Function InitLicense() As Boolean
		RuntimeManager.Bind(ProductCode.Desktop)
		Dim aoInit As IAoInitialize = New AoInitializeClass()
		Dim status As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic)
		If status <> esriLicenseStatus.esriLicenseCheckedOut Then
			Console.WriteLine("License initialization error")
			Return False
		Else
			Return True
		End If
	End Function
	#End Region


	#Region "helper methods"
	''' <summary>
	''' Retrieve parameters
	''' </summary>
	''' <param name="args">args</param>
	Private Shared Sub Retrieve_Params(args As String())
		For i As Integer = 2 To args.Length - 1
			Select Case args(i)
				Case "-h"
					restAdmin = args(System.Threading.Interlocked.Increment(i))
					Exit Select
				Case "-d"
					sourcePath = args(System.Threading.Interlocked.Increment(i))
					Exit Select
				Case "-n"
					serviceName = args(System.Threading.Interlocked.Increment(i))
					Exit Select
				Case "-u"
					username = args(System.Threading.Interlocked.Increment(i))
					Exit Select
				Case "-p"
					password = args(System.Threading.Interlocked.Increment(i))
					Exit Select
			End Select
		Next
	End Sub

	''' <summary>
	''' Get Source Type
	''' </summary>
	''' <param name="sourcePath">path of the data source</param>
	''' <returns>data source type</returns>
	Private Shared Function GetSourceType(sourcePath As String) As esriImageServiceSourceType
		If sourcePath.ToLower().EndsWith(".lyr") Then
			Return esriImageServiceSourceType.esriImageServiceSourceTypeLayer
		Else
			Dim fileInfo As New FileInfo(sourcePath)
			OpenRasterDataset(fileInfo.DirectoryName, fileInfo.Name)
			If TypeOf rasterDataset Is IMosaicDataset Then
				Return esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset
			Else
				Return esriImageServiceSourceType.esriImageServiceSourceTypeDataset
			End If
		End If
	End Function

	''' <summary>
	''' Open Raster Dataset
	''' </summary>
	''' <param name="path">path of the dataset</param>
	''' <param name="rasterDSName">name of the dataset</param>        
	Private Shared Sub OpenRasterDataset(path As [String], rasterDSName As [String])
		'this is why the utility user needs access to data source. image service configurations varies among data sources.
		Dim workspaceFactory As IWorkspaceFactory = Nothing
		Dim workspace As IWorkspace = Nothing
		Dim rasterWorkspaceEx As IRasterWorkspaceEx = Nothing
		Dim factoryType As Type = Nothing
		Try
			Select Case path.Substring(path.Length - 4, 4).ToLower()
				' a path can never be shorter than 4 characters, isn't it? c:\a
				Case ".gdb"
					factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")
					workspaceFactory = TryCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
					workspace = workspaceFactory.OpenFromFile(path, 1)
					rasterWorkspaceEx = DirectCast(workspace, IRasterWorkspaceEx)
					rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterDSName)
					Exit Select
				Case ".sde"
					factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory")
					workspaceFactory = TryCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
					workspace = workspaceFactory.OpenFromFile(path, 1)
					rasterWorkspaceEx = DirectCast(workspace, IRasterWorkspaceEx)
					rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterDSName)
					Exit Select
				Case Else
					factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
					workspaceFactory = TryCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
					workspace = workspaceFactory.OpenFromFile(path, 1)
					Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspace, IRasterWorkspace)
					rasterDataset = rasterWorkspace.OpenRasterDataset(rasterDSName)
					Exit Select
			End Select
		Catch generatedExceptionName As Exception
			Throw New ArgumentException("Failed to open source data")
		End Try
	End Sub

	''' <summary>
	''' Show usage
	''' </summary>
	Private Shared Sub ShowUsage()
		Console.WriteLine()
        Console.WriteLine("ArcObject Sample: command line image service publishing and configuration utility (10.2 ArcGIS Server). Data is not copied to server using this tool. source data must be accessible by ArcGIS Server running account. CachingScheme can't be defined through this tool but can be done through Caching geoprocessing tools. REST Metadata/thumbnail/iteminfo resource are not available through this app but can be developed using a similar approach to server admin endpoint.")
		Console.WriteLine()
		Console.WriteLine("Usage. <>: required parameter; |: pick one.")
		Console.WriteLine("isconfig -o publish -h <host_admin_url> -u <adminuser> -p <adminpassword> -d <datapath> -n <serviceName>")
		Console.WriteLine("isconfig -o <delete|start|stop> -h <host_admin_url> -u <adminuser> -p <adminpassword> -n <serviceName>")
		Console.WriteLine("isconfig -o <list> -h <host_admin_url> -u <adminuser> -p <adminpassword>")
		Console.WriteLine("e.g. isconfig -o list -h http://myserver:6080/arcgis/admin -u username -p password")
	End Sub
	#End Region

	#Region "REST Admin based http requests"
	''' <summary>
	''' Generate a token
	''' </summary>
	''' <param name="restAdmin">REST admin url: http://server:port/arcigs/admin</param>
	''' <returns>A token that has default expiration time</returns>
	Public Shared Function GenerateAGSToken_RESTAdmin() As String
		Try
			Dim loginUrl As String = restAdmin & "/generateToken"
			Dim request As WebRequest = WebRequest.Create(loginUrl)
			request.Method = "POST"
			Dim credential As String = "username=" & username & "&password=" & password & "&client=requestip&expiration=&f=json"
			Dim content As Byte() = Encoding.UTF8.GetBytes(credential)
			request.ContentLength = content.Length
			request.ContentType = "application/x-www-form-urlencoded"
			Dim requestStream As Stream = request.GetRequestStream()
			requestStream.Write(content, 0, content.Length)
			requestStream.Close()
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Dim index1 As Integer = result.IndexOf("token"":""") + "token"":""".Length
			Dim index2 As Integer = result.IndexOf("""", index1)
			'Dictionary<string, object> dictResult = DeserializeJSON(result, true);
			Dim token As String = result.Substring(index1, index2 - index1)
			Return token
		Catch
			Return ""
		End Try
	End Function

	''' <summary>
	''' Create arcgis server folder
	''' </summary>
	''' <param name="restAdmin">REST admin url, e.g. http://server:port/arcgis/admin</param>
	''' <param name="folderName">Folder name</param>
	''' <param name="description">Description of the folder</param>
	''' <returns>True if successfully created</returns>
	Private Shared Function CreateServerFolder_RESTAdmin(folderName As String, description As String) As Boolean
		Try
			Dim token As String = GenerateAGSToken_RESTAdmin()
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim folderUrl As String = restAdmin & "/services/" & folderName & "?f=json&token=" & token
			Dim request As WebRequest = WebRequest.Create(folderUrl)
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			If Not result.Contains("error") Then
				Return True
			Else
				Dim createFolderUrl As String = restAdmin & "/services/createFolder"
				request = WebRequest.Create(createFolderUrl)
				Dim postcontent As String = String.Format("folderName={0}&description={1}&f=pjson&token={2}", folderName, description, token)
				Dim content As [Byte]() = Encoding.UTF8.GetBytes(postcontent)
				request.ContentLength = content.Length
				'((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
				request.ContentType = "application/x-www-form-urlencoded"
				request.Method = "POST"
				Dim requestStream As Stream = request.GetRequestStream()
				requestStream.Write(content, 0, content.Length)
				requestStream.Close()
				response = request.GetResponse()
				responseStream = response.GetResponseStream()
				reader = New StreamReader(responseStream)
				result = reader.ReadToEnd()
				Return result.Contains("success")
			End If
		Catch
			Return False
		End Try
	End Function
	Private Shared Sub GetServerDirectory_RESTAdmin(dirType As String, ByRef physicalPath As String, ByRef virtualPath As String)
		physicalPath = ""
		virtualPath = ""
		Try
			Dim token As String = GenerateAGSToken_RESTAdmin()
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim directoryAlias As String = dirType.ToString().ToLower().Replace("esrisdtype", "arcgis")
			Dim directoryUrl As String = restAdmin & "/system/directories/" & directoryAlias & "?f=json&token=" & token
			Dim request As WebRequest = WebRequest.Create(directoryUrl)
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Try
				Dim index As Integer = result.IndexOf(dirType)
				Dim index1 As Integer = result.IndexOf("physicalPath"":""", index) + "physicalPath"":""".Length
				Dim index2 As Integer = result.IndexOf("""", index1)
				physicalPath = result.Substring(index1, index2 - index1)

				index1 = result.IndexOf("virtualPath"":""", index) + "virtualPath"":""".Length
				index2 = result.IndexOf("""", index1)
				virtualPath = result.Substring(index1, index2 - index1)
			Catch
			End Try
		Catch
		End Try
	End Sub
	''' <summary>
	''' Delete Service
	''' </summary>
	''' <param name="restAdmin">REST admin url, e.g. http://server:port/arcgis/admin</param>
	''' <param name="serviceName">Service Name</param>
	''' <param name="serviceType">Server Type, e.g. ImageServer, MapServer, GeoDataServer, GeoprocessingServer, GeometryServer, etc</param>
	''' <returns>True if successfully deleted</returns>
	Public Shared Function DeleteService_RESTAdmin() As Boolean
		Try
			Dim token As String = GenerateAGSToken_RESTAdmin()
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim serviceUrl As String = restAdmin & "/services/" & serviceName & "." & "ImageServer" & "/delete"
			Dim request As WebRequest = WebRequest.Create(serviceUrl)
			Dim postcontent As String = "f=pjson&token=" & token
			Dim content As [Byte]() = Encoding.UTF8.GetBytes(postcontent)
			request.ContentLength = content.Length
			'((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
			request.ContentType = "application/x-www-form-urlencoded"
			request.Method = "POST"
			Dim requestStream As Stream = request.GetRequestStream()
			requestStream.Write(content, 0, content.Length)
			requestStream.Close()
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Console.WriteLine("delete service {0}, result: {1}", serviceName, result)
			Return result.Contains("success")
		Catch
			Return False
		End Try
	End Function

	Public Shared Function StartService_RESTAdmin() As Boolean
		Try
			Dim token As String = GenerateAGSToken_RESTAdmin()
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim serviceUrl As String = restAdmin & "/services/" & serviceName & "." & "ImageServer" & "/start"
			Dim request As WebRequest = WebRequest.Create(serviceUrl)
			Dim postcontent As String = "f=pjson&token=" & token
			Dim content As [Byte]() = Encoding.UTF8.GetBytes(postcontent)
			request.ContentLength = content.Length
			'((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
			request.ContentType = "application/x-www-form-urlencoded"
			request.Method = "POST"
			Dim requestStream As Stream = request.GetRequestStream()
			requestStream.Write(content, 0, content.Length)
			requestStream.Close()
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Console.WriteLine("start service {0}, result: {1}", serviceName, result)
			Return result.Contains("success")
		Catch
			Return False
		End Try
	End Function

	Public Shared Function StopService_RESTAdmin() As Boolean
		Try
			Dim token As String = GenerateAGSToken_RESTAdmin()
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim serviceUrl As String = restAdmin & "/services/" & serviceName & "." & "ImageServer" & "/stop"
			Dim request As WebRequest = WebRequest.Create(serviceUrl)
			Dim postcontent As String = "f=pjson&token=" & token
			Dim content As [Byte]() = Encoding.UTF8.GetBytes(postcontent)
			request.ContentLength = content.Length
			'((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
			request.ContentType = "application/x-www-form-urlencoded"
			request.Method = "POST"
			Dim requestStream As Stream = request.GetRequestStream()
			requestStream.Write(content, 0, content.Length)
			requestStream.Close()
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Console.WriteLine("stop service {0}, result: {1}", serviceName, result)
			Return result.Contains("success")
		Catch
			Return False
		End Try
	End Function
	Public Shared Sub ListServices_RESTAdmin()
		Console.WriteLine("List of image services: ")
		ListServices_RESTAdmin(restAdmin & "/services", "")
	End Sub
	Public Shared Sub ListServices_RESTAdmin(root As String, folder As String)
		Try
			Dim token As String = GenerateAGSToken_RESTAdmin()
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim serviceUrl As String = root & "/" & folder
			Dim request As WebRequest = WebRequest.Create(serviceUrl)
			Dim postcontent As String = "f=json&token=" & token
			Dim content As [Byte]() = Encoding.UTF8.GetBytes(postcontent)
			request.ContentLength = content.Length
			'((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
			request.ContentType = "application/x-www-form-urlencoded"
			request.Method = "POST"
			Dim requestStream As Stream = request.GetRequestStream()
			requestStream.Write(content, 0, content.Length)
			requestStream.Close()
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Dim indexfolder1 As Integer = result.IndexOf("folders"":[")
			If indexfolder1 <> -1 Then
				indexfolder1 += "folders"":[".Length
				Dim indexfolder2 As Integer = result.IndexOf("]", indexfolder1)
				Dim folderlist As String = result.Substring(indexfolder1, indexfolder2 - indexfolder1)
				Dim folders As String() = folderlist.Replace("""", "").Split(","C)
				For Each subfolder As String In folders
					ListServices_RESTAdmin(serviceUrl, subfolder)
				Next
			End If


			Dim index As Integer = result.IndexOf("services")
			While index > 0
				Try
					Dim index1 As Integer = result.IndexOf("folderName"":""", index)
                    If index1 = -1 Then
                        Exit While
                    End If
					index1 += "folderName"":""".Length
					Dim index2 As Integer = result.IndexOf("""", index1)
					Dim folderName As String = result.Substring(index1, index2 - index1)

					index1 = result.IndexOf("serviceName"":""", index2) + "serviceName"":""".Length
					index2 = result.IndexOf("""", index1)
					Dim serviceName As String = result.Substring(index1, index2 - index1)

					index1 = result.IndexOf("type"":""", index2) + "type"":""".Length
					index2 = result.IndexOf("""", index1)
					Dim serviceType As String = result.Substring(index1, index2 - index1)
					If serviceType = "ImageServer" Then
						If folderName = "/" Then
							'root
							Console.WriteLine(serviceName)
						Else
							Console.WriteLine(folderName & "/" & serviceName)
						End If
					End If
					index = index2
				Catch
				End Try
			End While
		Catch
		End Try
	End Sub
	''' <summary>
	''' create image service
	''' </summary>
	''' <param name="restAdmin">host machine name (windows or linux)</param>
	''' <param name="sourcePath">data source path, must be windows path (linux path is constructed automaticlly by windows path)</param>
	''' <param name="serviceName">configuration name</param>
	''' <param name="createImageServiceParams">Cration parameters, e.g. raster functions, colormaptorgb, raster types, dem, dynamicimageworkspace, customized propertyset etc</param>
	''' <returns>true if created successfully</returns>
	Public Shared Function CreateISConfig_RESTAdmin() As Boolean
		'string restAdmin, string username, string password, string sourcePath, string serviceName
		Try
			Dim sourceType As esriImageServiceSourceType = GetSourceType(sourcePath)

			Dim serviceType As String = "ImageServer"
			'DeleteService_RESTAdmin();
			restAdmin = If(restAdmin.EndsWith("/"), restAdmin.Substring(0, restAdmin.Length - 1), restAdmin)
			Dim serviceFolder As String = ""
			If serviceName.Contains("/") Then
				serviceFolder = serviceName.Substring(0, serviceName.IndexOf("/"))
				CreateServerFolder_RESTAdmin(serviceFolder, "")
				serviceName = serviceName.Substring(serviceFolder.Length + 1, serviceName.Length - serviceFolder.Length - 1)
			End If
			Dim createServiceUrl As String = ""
			If serviceFolder = "" Then
				createServiceUrl = restAdmin & "/services/createService"
			Else
				createServiceUrl = restAdmin & "/services/" & serviceFolder & "/createService"
			End If
			'createServiceUrl = "http://wenxue:6080/arcgis/admin/services/createService";
			Dim request As WebRequest = WebRequest.Create(createServiceUrl)

			'DataSourceIsReadOnly                                                                                                                                                                                                                                                                                                                                                                                                            
			Dim sBuilder As New StringBuilder()
			sBuilder.Append("{")
			sBuilder.AppendFormat("{0}: {1},", QuoteString("serviceName"), QuoteString(serviceName))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("type"), QuoteString(serviceType))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("description"), QuoteString(""))

			sBuilder.AppendFormat("{0}: {1},", QuoteString("clusterName"), QuoteString("default"))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("minInstancesPerNode"), 1)
			sBuilder.AppendFormat("{0}: {1},", QuoteString("maxInstancesPerNode"), 2)
			sBuilder.AppendFormat("{0}: {1},", QuoteString("maxWaitTime"), 10000)
			sBuilder.AppendFormat("{0}: {1},", QuoteString("maxIdleTime"), 1800)
			sBuilder.AppendFormat("{0}: {1},", QuoteString("maxUsageTime"), 600)
			sBuilder.AppendFormat("{0}: {1},", QuoteString("loadBalancing"), QuoteString("ROUND_ROBIN"))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("isolationLevel"), QuoteString("HIGH"))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("configuredState"), QuoteString("STARTED"))

			Dim webCapabilities As String = ""

			If sourceType = esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset Then
				webCapabilities = "Image,Catalog,Metadata,Mensuration"
			Else
				'full list "Image,Catalog,Metadata,Download,Pixels,Edit,Mensuration"
				webCapabilities = "Image,Metadata,Mensuration"
			End If
			sBuilder.AppendFormat("{0}: {1},", QuoteString("capabilities"), QuoteString(webCapabilities))


			sBuilder.AppendFormat("{0}: {1}", QuoteString("properties"), "{")
			sBuilder.AppendFormat("{0}: {1},", QuoteString("supportedImageReturnTypes"), QuoteString("MIME+URL"))

			Dim workspace As IWorkspace = DirectCast(rasterDataset, IDataset).Workspace
			If workspace.WorkspaceFactory.WorkspaceType = esriWorkspaceType.esriRemoteDatabaseWorkspace Then
				Dim wsName2 As IWorkspaceName2 = TryCast(DirectCast(workspace, IDataset).FullName, IWorkspaceName2)
				Dim connString As String = wsName2.ConnectionString
				sBuilder.AppendFormat("{0}: {1},", QuoteString("connectionString"), QuoteString(connString))
				sBuilder.AppendFormat("{0}: {1},", QuoteString("raster"), QuoteString(DirectCast(rasterDataset, IDataset).Name))
			Else
				sBuilder.AppendFormat("{0}: {1},", QuoteString("path"), QuoteString(sourcePath.Replace("\", "\\")))
			End If

			sBuilder.AppendFormat("{0}: {1},", QuoteString("esriImageServiceSourceType"), QuoteString(sourceType.ToString()))

			Dim outputDir As String = ""
			Dim virtualDir As String = ""

			GetServerDirectory_RESTAdmin("arcgisoutput", outputDir, virtualDir)

			Dim cacheDir As String = ""
			Dim virtualCacheDir As String = ""
			GetServerDirectory_RESTAdmin("arcgisoutput", cacheDir, virtualCacheDir)


			If outputDir <> "" Then
				sBuilder.AppendFormat("{0}: {1},", QuoteString("outputDir"), QuoteString(outputDir))
					'http://istest2:6080/arcgis/server/arcgisoutput"));
					'sBuilder.AppendFormat("{0}: {1},", QuoteString("dynamicImageWorkspace"), QuoteString(@"D:\UploadDir"));                    
				sBuilder.AppendFormat("{0}: {1},", QuoteString("virtualOutputDir"), QuoteString(virtualDir))
			End If
			'if (cacheDir != "")
			'{
			'    sBuilder.AppendFormat("{0}: {1},", QuoteString("cacheDir"), QuoteString(cacheDir));
			'    sBuilder.AppendFormat("{0}: {1},", QuoteString("virtualCacheDir"), QuoteString(virtualCacheDir));//http://istest2:6080/arcgis/server/arcgisoutput"));                    
			'}
			sBuilder.AppendFormat("{0}: {1},", QuoteString("copyright"), QuoteString(""))

			'properties for a mosaic Dataset;
			If sourceType = esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset Then
				Dim functionRasterDataset As IFunctionRasterDataset = DirectCast(rasterDataset, IFunctionRasterDataset)
				Dim propDefaults As IPropertySet = functionRasterDataset.Properties
				Dim names As Object, values As Object
				propDefaults.GetAllProperties(names, values)
				Dim propNames As New List(Of String)()
				propNames.AddRange(DirectCast(names, String()))
				If propNames.Contains("AllowedCompressions") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedCompressions"), QuoteString(propDefaults.GetProperty("AllowedCompressions").ToString()))
				End If
				'string
				If propNames.Contains("MaxImageHeight") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageHeight"), QuoteString(propDefaults.GetProperty("MaxImageHeight").ToString()))
				End If
				'should be int     
				If propNames.Contains("MaxImageWidth") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageWidth"), QuoteString(propDefaults.GetProperty("MaxImageWidth").ToString()))
				End If
				'should be int
				If propNames.Contains("DefaultResamplingMethod") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultResamplingMethod"), QuoteString(propDefaults.GetProperty("DefaultResamplingMethod").ToString()))
				End If
				'should be int
				If propNames.Contains("DefaultCompressionQuality") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionQuality"), QuoteString(propDefaults.GetProperty("DefaultCompressionQuality").ToString()))
				End If
				'should be int
				If propNames.Contains("MaxRecordCount") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("maxRecordCount"), QuoteString(propDefaults.GetProperty("MaxRecordCount").ToString()))
				End If
				'should be int
				If propNames.Contains("MaxMosaicImageCount") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("maxMosaicImageCount"), QuoteString(propDefaults.GetProperty("MaxMosaicImageCount").ToString()))
				End If
				'should be int
				If propNames.Contains("MaxDownloadImageCount") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("maxDownloadImageCount"), QuoteString(propDefaults.GetProperty("MaxDownloadImageCount").ToString()))
				End If
				'should be int
				If propNames.Contains("MaxDownloadSizeLimit") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("MaxDownloadSizeLimit"), QuoteString(propDefaults.GetProperty("MaxDownloadSizeLimit").ToString()))
				End If
				'should be int
				If propNames.Contains("AllowedFields") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedFields"), QuoteString(propDefaults.GetProperty("AllowedFields").ToString()))
				End If
				'string
				If propNames.Contains("AllowedMosaicMethods") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedMosaicMethods"), QuoteString(propDefaults.GetProperty("AllowedMosaicMethods").ToString()))
				End If
				'string
				If propNames.Contains("AllowedItemMetadata") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedItemMetadata"), QuoteString(propDefaults.GetProperty("AllowedItemMetadata").ToString()))
				End If
				'string
				If propNames.Contains("AllowedMensurationCapabilities") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("AllowedMensurationCapabilities"), QuoteString(propDefaults.GetProperty("AllowedMensurationCapabilities").ToString()))
				End If
				'string
				If propNames.Contains("DefaultCompressionTolerance") Then
					sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionTolerance"), QuoteString(propDefaults.GetProperty("DefaultCompressionTolerance").ToString()))
					'string                    
					'sBuilder.AppendFormat("{0}: {1},", QuoteString("downloadDir"), QuoteString(@"c:\temp"));//string
					'sBuilder.AppendFormat("{0}: {1},", QuoteString("virutalDownloadDir"), QuoteString(@"http://localhost/temp");//string
				End If
			ElseIf sourceType <> esriImageServiceSourceType.esriImageServiceSourceTypeCatalog Then
				'not iscdef
				sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedCompressions"), QuoteString("None,JPEG,LZ77"))
				sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageHeight"), QuoteString("4100"))
				'should be int     
				sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageWidth"), QuoteString("15000"))
				'should be int
				sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultResamplingMethod"), QuoteString("0"))
				'should be int
				sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionQuality"), QuoteString("75"))
				'should be int
				sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionTolerance"), QuoteString("0.01"))
				'should be int
				Dim measure As IMensuration = New MensurationClass()
				measure.Raster = DirectCast(rasterDataset, IRasterDataset2).CreateFullRaster()
				Dim mensurationCaps As String = ""
				If measure.CanMeasure Then
					mensurationCaps = "Basic"
				End If
				If measure.CanMeasureHeightBaseToTop Then
					mensurationCaps += ",Base-Top Height"
				End If
				If measure.CanMeasureHeightBaseToTopShadow Then
					mensurationCaps += ",Base-Top Shadow Height"
				End If
				If measure.CanMeasureHeightTopToTopShadow Then
					mensurationCaps += ",Top-Top Shadow Height"
				End If
					'string
				sBuilder.AppendFormat("{0}: {1},", QuoteString("AllowedMensurationCapabilities"), QuoteString(mensurationCaps))
			End If

			'sBuilder.AppendFormat("{0}: {1},", QuoteString("dEM"), QuoteString(@"c:\elevation\elevation.tif"));                
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("supportsOwnershipBasedAccessControl"), QuoteString("true"));
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("allowOthersToUpdate"), QuoteString("true"));               
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("allowOthersToDelete"), QuoteString("true"));
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("cacheOnDemand"), QuoteString("false"));
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("isCached"), QuoteString("false"));
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("ignoreCache"), QuoteString("true"));
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("useLocalCacheDir"), QuoteString("false"));
			'sBuilder.AppendFormat("{0}: {1},", QuoteString("clientCachingAllowed"), QuoteString("false"));


			sBuilder.AppendFormat("{0}: {1},", QuoteString("colormapToRGB"), QuoteString("false"))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("returnJPGPNGAsJPG"), QuoteString("false"))
			sBuilder.AppendFormat("{0}: {1},", QuoteString("allowFunction"), QuoteString("true"))
			Dim rasterFunctions As String = ""
			sBuilder.AppendFormat("{0}: {1},", QuoteString("rasterFunctions"), QuoteString(rasterFunctions).Replace("\", "\\"))
			Dim rasterTypes As String = ""
			sBuilder.AppendFormat("{0}: {1}", QuoteString("rasterTypes"), QuoteString(rasterTypes).Replace("\", "\\"))

			sBuilder.Append("},")
			Dim enableWCS As Boolean = True
			Dim enableWMS As Boolean = True
			sBuilder.AppendFormat("{0}: {1}", QuoteString("extensions"), "[{""typeName"":""WCSServer"",""enabled"":""" & enableWCS & """,""capabilities"":null,""properties"":{}},{""typeName"":""WMSServer"",""enabled"":""" & enableWMS & """,""capabilities"":null,""properties"":{""title"":""WMS"",""name"":""WMS"",""inheritLayerNames"":""false""}}")
			sBuilder.Append("],")
			sBuilder.AppendFormat("{0}: {1}", QuoteString("datasets"), "[]")
			sBuilder.Append("}")
			Dim postcontent As String = HttpUtility.UrlEncode(sBuilder.ToString())
			Dim token As String = GenerateAGSToken_RESTAdmin()
			postcontent = "service=" & postcontent & "&startAfterCreate=on&f=pjson&token=" & token
			Dim content As [Byte]() = Encoding.UTF8.GetBytes(postcontent)
			request.ContentLength = content.Length
			request.ContentType = "application/x-www-form-urlencoded"
			request.Method = "POST"
			Dim requestStream As Stream = request.GetRequestStream()
			requestStream.Write(content, 0, content.Length)
			requestStream.Close()
			Dim response As WebResponse = request.GetResponse()
			Dim responseStream As Stream = response.GetResponseStream()
			Dim reader As New StreamReader(responseStream)
			Dim result As String = reader.ReadToEnd()
			Console.WriteLine("create service:" & serviceName & " result:" & result)
			'wait for 5 seconds to reduce latency issue
			'System.Threading.Thread.Sleep(5000);
			Return result.Contains("success")
		Catch exc As Exception
			Console.WriteLine(exc.Message)
			Return False
		End Try
	End Function
	Private Shared Function QuoteString(input As String) As String
		Return """" & input & """"
	End Function
	Private Shared Function DeQuoteString(input As String) As String
		If input.StartsWith("""") Then
			Return input.Substring(1, input.Length - 2).Trim()
		Else
			Return input
		End If
	End Function

	#End Region

End Class
