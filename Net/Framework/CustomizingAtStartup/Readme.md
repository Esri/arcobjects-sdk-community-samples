## Applying user interface customizations at startup

It is unsafe to make user interface customizations before the ArcGIS application framework has fully initialized. This sample shows you how to use IApplicationStatusEvents and IApplicationStatus inside a custom extension to monitor the framework's state and properly apply interface customizations when the extension loads.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open and compile the solution in Visual Studio.  
1. Start ArcMap.  
1. Click Customize and click Extensions.  
1. Turn on the ACME extension. The ACME menu appears on the main menu bar.  
1. Turning off the extension removes the ACME menu.  







#### See Also  
[Creating an application extension](http://desktop.arcgis.com/search/?q=Creating%20an%20application%20extension&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


