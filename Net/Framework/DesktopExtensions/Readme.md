## Extension implementation

This sample illustrates different implementations of application extensions in ArcGIS. The SimpleExtension class contains the minimal implementation of IExtension, which listens to document events. Examples of configurable extensions are shown by the ArcViewOnlyExtension, ArcEditorOnlyExtension, and ArcInfoOnlyExtension classes in the project. The extension state of these implementations can be toggled in the Extension Manager dialog box and is only enabled when the appropriate product license is running in the current application.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
Organization:          Esri, http://www.esri.com
Date:                  3/28/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open and compile the sample project in Visual Studio. If necessary, set up a debug application.  
1. Start ArcCatalog, ArcMap, ArcScene, or ArcGlobe (this sample works in any of these applications).   
1. Create or open a document. If you are debugging, select the Output Window in Visual Studio where the SimpleExtension class prints debugging information, which indicates it is listening to document events.  
1. Click the Customize menu, and click Extensions. The Extension Manager dialog box opens.  
1. Select one of the sample extensions to view its associated description shown at the bottom of the dialog box.  
1. Verify the product license your application is running against and select the appropriate sample extension on the dialog box. For example, if you are running an ArcInfo license, select ArcInfo Extension (C#) or ArcInfo Extension (VB .NET) from the list; extension checking will be successful, therefore, you can check the extension off.  
1. If you check the other product extension on, an error similar to the standard ArcGIS extensions appears, indicating it is unavailable.  
1. Close, then open the Extension Manager dialog box. The License not available message appears next to the extensions that you did not enable.   







#### See Also  
[Creating an application extension](http://desktop.arcgis.com/search/?q=Creating%20an%20application%20extension&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


