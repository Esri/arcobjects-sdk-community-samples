using ESRI.ArcGIS.Carto;

namespace TabbedFeatureInspector
{
  /// <summary>
  /// IApplicationServices must be implemented by the hosting engine application of the AttachTabbedInspectorExtensionCommand command.
  /// </summary>
  public interface IApplicationServices
  {
    /// <summary>
    /// Display a status message in the application user interface.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="error"></param>
    void SetStatusMessage(string message, bool error);

    /// <summary>
    /// Returns the selected layer in the application's table of contents control.
    /// </summary>
    /// <returns></returns>
    IFeatureLayer GetLayerSelectedInTOC();
  }
}