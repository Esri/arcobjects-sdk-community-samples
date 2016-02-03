using ESRI.ArcGIS.Controls;

namespace NAEngine
{
	public static class CommonFunctions
	{
		public static IEngineNetworkAnalystEnvironment GetTheEngineNetworkAnalystEnvironment()
		{
			// The ArcGIS Network Analyst extension environment is a singleton, and must be accessed using the System.Activator
			System.Type t = System.Type.GetTypeFromProgID("esriControls.EngineNetworkAnalystEnvironment");
			var naEnv = System.Activator.CreateInstance(t) as IEngineNetworkAnalystEnvironment;
			return naEnv;
		}
	}
}
