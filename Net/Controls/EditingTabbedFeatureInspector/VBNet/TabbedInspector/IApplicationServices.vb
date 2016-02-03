Imports ESRI.ArcGIS.Carto

Namespace TabbedFeatureInspector
  '/ <summary>
  '/ IApplicationServices must be implemented by the hosting engine application of the AttachTabbedInspectorExtensionCommand command.
  '/ </summary>
  Public Interface IApplicationServices
    '/ <summary>
    '/ Display a status message in the application user interface.
    '/ </summary>
    '/ <param name="message"></param>
    '/ <param name="error"></param>
    Sub SetStatusMessage(ByVal message As String, ByVal err As Boolean)

    '/ <summary>
    '/ Returns the selected layer in the application's table of contents control.
    '/ </summary>
    '/ <returns></returns>
    Function GetLayerSelectedInTOC() As IFeatureLayer
  End Interface
End Namespace
