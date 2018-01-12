## Cloning an object using persistence

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrate an implementation of a clonable object through serialization. Sometimes, when you are writing your objects, you might need to support serialization. For example, a custom symbol or custom element needs to be saved with the map document and reloaded when the map document is opened. In most cases, you can achieve this functionality by implementing the IPersistVariant interface, which gives you an elegant solution. However, in some cases, IPersistVariant is not enough and your object must support IPersisStream, for example, if you need to support cloning through serialization. By temporarily saving your object to an ObjectStream, you can duplicate the object by creating an instance of your class and loading its properties from the temporary ObjectStream. To do so, use an ObjectCopy class (which uses ObjectStream internally). </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#
Subject:               SDK General
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution.  
1. Build the PeristStreamHelper.csproj, ClonableObj.csproj, and TestApp.csproj projects.  
1. Make sure the TestApp.csproj project is set as the startup project (in the Solution Explorer, right-click the project and select Set as StartUp Project).  
1. Press F5 to run the test application. A console window appears listing the flow of the test application (note the different messages reporting the state of the original and  cloned objects).  
1. When done, press any key to close the application.  







#### See Also  
[Implement IPersistStream in a .NET class](http://desktop.arcgis.com/search/?q=Implement%20IPersistStream%20in%20a%20.NET%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Implementing persistence](http://desktop.arcgis.com/search/?q=Implementing%20persistence&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Implementing cloning](http://desktop.arcgis.com/search/?q=Implementing%20cloning&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Triangle graphic element](../../../Net/GraphicsPipeline/TriangleElement)  
[Sample: Clonable object](../../../Net/SDK_General/ClonableObject)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


