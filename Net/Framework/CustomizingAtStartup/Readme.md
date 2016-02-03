##Applying user interface customizations at startup

###Purpose  
It is unsafe to make user interface customizations before the ArcGIS application framework has fully initialized. This sample shows you how to use IApplicationStatusEvents and IApplicationStatus inside a custom extension to monitor the framework's state and properly apply interface customizations when the extension loads.  


###Usage
1. Open and compile the solution in Visual Studio.  
1. Start ArcMap.  
1. Click Customize and click Extensions.  
1. Turn on the ACME extension. The ACME menu appears on the main menu bar.  
1. Turning off the extension removes the ACME menu.  







####See Also  
[Creating an application extension](http://desktop.arcgis.com/search/?q=Creating%20an%20application%20extension&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


