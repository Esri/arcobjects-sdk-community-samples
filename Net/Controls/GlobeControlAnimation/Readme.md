##Animation in the GlobeControl

###Purpose  
This sample demonstrates how to use camera and layer animation in the GlobeControl by loading a document that contains an animation file or by directly loading an ArcGlobe animation file (*.aga).  This sample also shows how to use the ToolbarControl and TOCControl, which allow you to search and select Globe documents, interact and navigate with the globe using different tools and commands, and turn layers on and off. The TOCControl and ToolbarControl property pages have been used to set the buddy controls and add the globe commands.   


###Usage
1. Open a globe document that contains animation or load the sample animation file (from the provided data folder).  
1. Run play animation for a certain duration (default 10 seconds) or a number of iterations (default 500).   





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">When a document containing an animation file (.aga) is loaded into the GlobeControl (using the IGlobeControl.Load3dFile method), you can play the animation for the set amount of time (duration) or for a number of iterations. The sample also demonstrates how to loop an animation (cycles). In addition, you can directly load an animation file using the Load Animation File command button of the sample (using the IBasicScene.LoadAnimation method). </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The two radio buttons indicate whether the animation will be played for the set duration or for the number of iterations. The Cycles text box indicates the number of times the animation plays. The sample demonstrates how to use the IGlobeControl.Load3dFile, IBasicScene.LoadAnimation, IAnimationTracks.ApplyTracks, and IAnimationTrack.InterpolateObjectProperties methods. In addition, the sample also illustrates how to access and manipulate different keyframes (IKeyframe) of different animation types (IAnimationTrack.AnimationType) in an animation track.</div>  


####See Also  
[GlobeControl class](http://desktopdev.arcgis.com/search/?q=GlobeControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IGlobeControl interface](http://desktopdev.arcgis.com/search/?q=IGlobeControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine: 3D Analyst |  
|  | ArcGIS for Desktop Basic: 3D Analyst |  
|  | ArcGIS for Desktop Standard: 3D Analyst |  
|  | ArcGIS for Desktop Advanced: 3D Analyst |  


