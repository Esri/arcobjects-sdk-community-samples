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
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;
using System.Reflection;
namespace PersistExtensionAddIn
{
  public class LogExtension : ESRI.ArcGIS.Desktop.AddIns.Extension
  {
    Dictionary<string, string> _log;

    public LogExtension()
    {
    }

    protected override void OnStartup()
    {
      WireDocumentEvents();
    }

    private void WireDocumentEvents()
    {
      // Named event handler
      ArcMap.Events.OpenDocument += new ESRI.ArcGIS.ArcMapUI.IDocumentEvents_OpenDocumentEventHandler(Events_OpenDocument);
    }


    void Events_OpenDocument()
    {
      string logText = "Document was saved by " + _log["userName"]
                            + " at " + _log["time"];
      LogMessage(logText);
    }

    //Get called when saving document.
    protected override void OnSave(Stream outStrm)
    {
        // Override OnSave and uses a binary formatter to serialize the log.
      Dictionary<string, string> log = new Dictionary<string, string>();
      log.Add("userName", Environment.UserName);
      log.Add("time", DateTime.Now.ToLongTimeString());
      var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
      bf.Serialize(outStrm, log);
    }
    
    //Get called when opening a document with persisted stream.
    protected override void OnLoad(Stream inStrm)
    {
        // Override OnLoad and uses a binary formatter to deserialize the log.
      var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
      _log = (Dictionary<string, string>)bf.Deserialize(inStrm);
    }

    private void LogMessage(string message)
    {
      System.Windows.Forms.MessageBox.Show(message);
    }
  }

}
