/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
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