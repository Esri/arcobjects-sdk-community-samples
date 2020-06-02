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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.EditorExt;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace SetFlowByDigitizedDirection
{
    public class SetFlowByDigitizedDirectionCSharp : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private IUtilityNetworkAnalysisExt m_utilNetExt;
        private IEditor m_editorExt;

        public SetFlowByDigitizedDirectionCSharp()
        {
            UID uidUtilNet = new UIDClass();
            uidUtilNet.Value = "esriEditorExt.UtilityNetworkAnalysisExt";
            m_utilNetExt = ArcMap.Application.FindExtensionByCLSID(uidUtilNet) as IUtilityNetworkAnalysisExt;

            UID uidEditor = new UIDClass();
            uidEditor.Value = "esriEditor.Editor";
            m_editorExt = ArcMap.Application.FindExtensionByCLSID(uidEditor) as IEditor;        
        }

        protected override void OnClick()
        {
            // get the current network
            IUtilityNetwork utilNet = GetCurrentNetwork() as IUtilityNetwork;

            // create an edit operation enabling an undo for this operation
            m_editorExt.StartOperation();

            // get a list of the current EIDs for edges in the network
            IEnumNetEID edgeEIDs = GetCurrentEIDs(esriElementType.esriETEdge);

            // set the flow direction for each edge in the network
            edgeEIDs.Reset();
            for (int i = 0; i < edgeEIDs.Count; i++)
            {
                int edgeEID = edgeEIDs.Next();
                utilNet.SetFlowDirection(edgeEID, esriFlowDirection.esriFDWithFlow);
            }

            // stop the edit operation, specifying a name for this operation
            m_editorExt.StopOperation("Set Flow Direction");

            // refresh the display to update the flow direction arrows
            IActiveView mapView = ArcMap.Document.ActiveView;
            mapView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        protected override void OnUpdate()
        {
            // by default, disable the command
            Enabled = false;

            // if there is not a current edit session, then disable the command
            if (m_editorExt.EditState != esriEditState.esriStateEditing)
                return;

            // otherwise, check to see if the flow direction is properly set for each edge EID
            IUtilityNetwork utilNet = GetCurrentNetwork() as IUtilityNetwork;
            IEnumNetEID edgeEIDs = GetCurrentEIDs(esriElementType.esriETEdge);
            edgeEIDs.Reset();
            for (int i = 0; i < edgeEIDs.Count; i++)
            {
                int edgeEID = edgeEIDs.Next();
                esriFlowDirection flowDir = utilNet.GetFlowDirection(edgeEID);
                if (flowDir != esriFlowDirection.esriFDWithFlow)
                {
                    // enable the command if the flow direction is not with the digitized direction
                    Enabled = true;

                    // we can return right now, since only one edge needs to have
                    // incorrect flow direction in order to enable the command
                    return;
                }
            }
        }

        //
        // returns an enumeration of EIDs of the network elements of the given element type
        //
        private IEnumNetEID GetCurrentEIDs(esriElementType elementType)
        {
            INetwork net = GetCurrentNetwork();
            IEnumNetEID eids = net.CreateNetBrowser(elementType);
            return eids;
        }

        //
        // returns a reference to the current logical network
        //
        private INetwork GetCurrentNetwork()
        {
            // get the current network from the Utility Network Analysis extension
            INetworkAnalysisExt nax = m_utilNetExt as INetworkAnalysisExt;
            IGeometricNetwork geomNet = nax.CurrentNetwork;

            // get the geometric network's logical network
            INetwork net = geomNet.Network;

            return net;
        }
    }
}
