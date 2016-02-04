/*

   Copyright 2016 Esri

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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.EditorExt;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalysis;

namespace CustomUpstreamTraceTask
{
	[Guid("f759ebca-dcd0-4ddd-be5a-aed8408447e5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("CustomUpstreamTraceTask.CustomUpstreamTraceTaskCSharp")]
	public sealed class CustomUpstreamTraceTaskCSharp : ITraceTask, ITraceTaskResults
	{
		#region COM Registration Function(s)
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);

			//
			// TODO: Add any COM registration code here
			//
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType);

			//
			// TODO: Add any COM unregistration code here
			//
		}

		#region ArcGIS Component Category Registrar generated code
		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			UtilityNetworkTasks.Register(regKey);

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			UtilityNetworkTasks.Unregister(regKey);

		}

		#endregion
		#endregion

		private IUtilityNetworkAnalysisExt m_utilNetExt;
		private IEnumNetEID m_resultJunctions;
		private IEnumNetEID m_resultEdges;

		bool ITraceTask.EnableSolve
		{
			get
			{
				// if there are no networks loaded, then the Solve button is disabled
				INetworkAnalysisExt nax = m_utilNetExt as INetworkAnalysisExt;
				if (nax.NetworkCount == 0)
					return false;

				// if there is at least one flag on the network,
				// then enable the Solve button
				INetworkAnalysisExtFlags naxFlags = m_utilNetExt as INetworkAnalysisExtFlags;
				if (naxFlags.EdgeFlagCount == 0 && naxFlags.JunctionFlagCount == 0)
					return false;
				else
					return true;
			}
		}

		string ITraceTask.Name
		{
			get
			{
				return "Custom Upstream Trace";
			}
		}

		void ITraceTask.OnCreate(IUtilityNetworkAnalysisExt utilityNetworkAnalysis)
		{
			m_utilNetExt = utilityNetworkAnalysis;
		}

		void ITraceTask.OnTraceExecution()
		{
			// prepare the network solver
			ITraceFlowSolverGEN tfs = UTIL_coreTraceSetup();
			if (tfs == null)
				return;

			// perform the trace task
			IEnumNetEID resultJuncs, resultEdges;
			ITraceTasks traceTasks = m_utilNetExt as ITraceTasks;
			esriFlowElements flowElements = traceTasks.TraceFlowElements;
			if (traceTasks.TraceEnds)
				// find the features stopping the trace
				tfs.FindFlowEndElements(esriFlowMethod.esriFMUpstream, flowElements, out resultJuncs, out resultEdges);
			else
				// return the traced features
				tfs.FindFlowElements(esriFlowMethod.esriFMUpstream, flowElements, out resultJuncs, out resultEdges);

			// copy the results to the class level
			INetworkAnalysisExt nax = m_utilNetExt as INetworkAnalysisExt;
			if (resultJuncs == null)
			{
				// junctions were not returned -- create an empty enumeration
				IEnumNetEIDBuilder eidBuilder = new EnumNetEIDArrayClass();
				eidBuilder.Network = nax.CurrentNetwork.Network;
				eidBuilder.ElementType = esriElementType.esriETJunction;
				m_resultJunctions = eidBuilder as IEnumNetEID;
			}
			else
			{
				m_resultJunctions = resultJuncs;
			}
			if (resultEdges == null)
			{
				// edges were not returned -- create an empty enumeration
				IEnumNetEIDBuilder eidBuilder = new EnumNetEIDArrayClass();
				eidBuilder.Network = nax.CurrentNetwork.Network;
				eidBuilder.ElementType = esriElementType.esriETEdge;
				m_resultEdges = eidBuilder as IEnumNetEID;
			}
			else
			{
				m_resultEdges = resultEdges;
			}

			// update the extension with the results
			INetworkAnalysisExtResults naxResults = m_utilNetExt as INetworkAnalysisExtResults;
			naxResults.ClearResults();   // first remove the old results
			if (naxResults.ResultsAsSelection)
				naxResults.CreateSelection(resultJuncs, resultEdges);
			else
			{
				naxResults.SetResults(resultJuncs, resultEdges);
			}
		}

		IEnumNetEID ITraceTaskResults.ResultEdges
		{
			get
			{
				return m_resultEdges;
			}
		}

		IEnumNetEID ITraceTaskResults.ResultJunctions
		{
			get
			{
				return m_resultJunctions;
			}
		}

		public ITraceFlowSolverGEN UTIL_coreTraceSetup()
		{
			// get the current network's logical network
			INetworkAnalysisExt nax = m_utilNetExt as INetworkAnalysisExt;
			INetwork net = nax.CurrentNetwork.Network;

			// create a new TraceFlowSolver object and
			// set the source network for the solve
			ITraceFlowSolverGEN tfs = new TraceFlowSolverClass() as ITraceFlowSolverGEN;
			INetSolver netSolver = tfs as INetSolver;
			netSolver.SourceNetwork = net;

			// get the barriers for the network, using the element barriers and 
			// selection barriers that have been added using the user interface
			INetworkAnalysisExtBarriers naxBarriers = m_utilNetExt as INetworkAnalysisExtBarriers;
			INetElementBarriers juncElemBarriers, edgeElemBarriers;
			naxBarriers.CreateElementBarriers(out juncElemBarriers, out edgeElemBarriers);
			ISelectionSetBarriers selSetBarriers;
			naxBarriers.CreateSelectionBarriers(out selSetBarriers);
			netSolver.set_ElementBarriers(esriElementType.esriETJunction, juncElemBarriers);
			netSolver.set_ElementBarriers(esriElementType.esriETEdge, edgeElemBarriers);
			netSolver.SelectionSetBarriers = selSetBarriers;

			// set up the disabled layers for the network solver
			// for each feature layer belonging to this network, determine if it is
			// enabled or disabled; if it's disabled, then notify the network solver
			for (int i = 0; i < nax.FeatureLayerCount; i++)
			{
				IFeatureLayer featureLayer = nax.get_FeatureLayer(i);
				if (naxBarriers.GetDisabledLayer(featureLayer))
					netSolver.DisableElementClass(featureLayer.FeatureClass.FeatureClassID);
			}

			INetworkAnalysisExtWeightFilter naxWeightFilter = m_utilNetExt as INetworkAnalysisExtWeightFilter;
			INetSolverWeightsGEN netSolverWeights = netSolver as INetSolverWeightsGEN;
			INetSchema netSchema = net as INetSchema;
			
			// create the junction weight filter
			int juncFilterRangeCount = naxWeightFilter.get_FilterRangeCount(esriElementType.esriETJunction);
			if (juncFilterRangeCount > 0)
			{
				INetWeight netWeight = netSchema.get_WeightByName(naxWeightFilter.JunctionWeightFilterName);
				netSolverWeights.JunctionFilterWeight = netWeight;

				esriWeightFilterType juncWeightFilterType;
				bool juncApplyNotOperator;
				naxWeightFilter.GetFilterType(esriElementType.esriETJunction,
					out juncWeightFilterType, out juncApplyNotOperator);
				netSolverWeights.SetFilterType(esriElementType.esriETJunction,
					juncWeightFilterType, juncApplyNotOperator);

				object[] juncFromValues = new object[juncFilterRangeCount];
				object[] juncToValues = new object[juncFilterRangeCount];
				for (int i = 0; i < juncFilterRangeCount; i++)
				{
					naxWeightFilter.GetFilterRange(esriElementType.esriETJunction, i,
						out juncFromValues[i], out juncToValues[i]);
				}
				netSolverWeights.SetFilterRanges(esriElementType.esriETJunction,
					ref juncFromValues, ref juncToValues);
			}

			// create the edge weight filters
			int edgeFilterRangeCount = naxWeightFilter.get_FilterRangeCount(esriElementType.esriETEdge);
			if (edgeFilterRangeCount > 0)
			{
				INetWeight fromToNetWeight = netSchema.get_WeightByName(naxWeightFilter.FromToEdgeWeightFilterName);
				netSolverWeights.FromToEdgeFilterWeight = fromToNetWeight;
				INetWeight toFromNetWeight = netSchema.get_WeightByName(naxWeightFilter.ToFromEdgeWeightFilterName);
				netSolverWeights.ToFromEdgeFilterWeight = toFromNetWeight;

				esriWeightFilterType edgeWeightFilterType;
				bool edgeApplyNotOperator;
				naxWeightFilter.GetFilterType(esriElementType.esriETEdge,
					out edgeWeightFilterType, out edgeApplyNotOperator);
				netSolverWeights.SetFilterType(esriElementType.esriETEdge,
					edgeWeightFilterType, edgeApplyNotOperator);

				object[] edgeFromValues = new object[edgeFilterRangeCount];
				object[] edgeToValues = new object[edgeFilterRangeCount];
				for (int i = 0; i < edgeFilterRangeCount; i++)
				{
					naxWeightFilter.GetFilterRange(esriElementType.esriETEdge, i,
						out edgeFromValues[i], out edgeToValues[i]);
				}
				netSolverWeights.SetFilterRanges(esriElementType.esriETEdge,
					ref edgeFromValues, ref edgeToValues);
			}

			INetworkAnalysisExtFlags naxFlags = m_utilNetExt as INetworkAnalysisExtFlags;

			// assign the edge flags to the network solver
			int edgeFlagCount = naxFlags.EdgeFlagCount;
			if (edgeFlagCount > 0)
			{
				IEdgeFlag[] edgeFlags = new IEdgeFlag[edgeFlagCount];
				for (int i = 0; i < edgeFlagCount; i++)
				{
					IEdgeFlagDisplay edgeFlagDisplay = naxFlags.get_EdgeFlag(i);
					IFlagDisplay flagDisplay = edgeFlagDisplay as IFlagDisplay;
					IEdgeFlag edgeFlag = new EdgeFlagClass();
					edgeFlag.Position = Convert.ToSingle(edgeFlagDisplay.Percentage);
					INetFlag netFlag = edgeFlag as INetFlag;
					netFlag.UserClassID = flagDisplay.FeatureClassID;
					netFlag.UserID = flagDisplay.FID;
					netFlag.UserSubID = flagDisplay.SubID;
					edgeFlags[i] = edgeFlag;
				}
				tfs.PutEdgeOrigins(ref edgeFlags);
			}

			// assign the junction flags to the network solver
			int juncFlagCount = naxFlags.JunctionFlagCount;
			if (juncFlagCount > 0)
			{
				IJunctionFlag[] juncFlags = new IJunctionFlag[juncFlagCount];
				for (int i = 0; i < juncFlagCount; i++)
				{
					IJunctionFlagDisplay juncFlagDisplay = naxFlags.get_JunctionFlag(i);
					IFlagDisplay flagDisplay = juncFlagDisplay as IFlagDisplay;
					IJunctionFlag juncFlag = new JunctionFlagClass();
					INetFlag netFlag = juncFlag as INetFlag;
					netFlag.UserClassID = flagDisplay.FeatureClassID;
					netFlag.UserID = flagDisplay.FID;
					netFlag.UserSubID = flagDisplay.SubID;
					juncFlags[i] = juncFlag;
				}
				tfs.PutJunctionOrigins(ref juncFlags);
			}

			// set the option for tracing on indeterminate flow
			ITraceTasks traceTasks = m_utilNetExt as ITraceTasks;
			tfs.TraceIndeterminateFlow = traceTasks.TraceIndeterminateFlow;

			return tfs;
		}
	}
}
