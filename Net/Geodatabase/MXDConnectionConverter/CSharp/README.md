MXDConnectionConverter
======================

**Description:**

C# code that will convert sde connections from app server to direct connections.

**Usage:**

It needs to be built on a machine that has ArcGIS on it (with Visual Studio 2010 or maybe 2012). Then to run it, you just run the exe. It has 3 parameters:

1-	The first is a folder path that has the mxd’s you’d like to convert (by default it is the current directory).

2-	The second is optional and is the characters you’d like to add after an underscore to the new mxd after conversion is complete (by default, if nothing is passed in it uses “DC”– for example an mxd named “mymap.mxd” will get saved as a new converted mxd named “mymap_DC.mxd”).

3-	The last parameter is also optional and is a boolean for verbose output (by default is “false”).

*For example:*

d:\MXDconnection_converter.exe --help
Usage: MXDconnection_converter.exe [{Current Directory | '&lt;PathToMxdDirectory>'}] [{DC | '&lt;newMxdStringToAdd>'}] [VERBOSE] [{? | --help}]

