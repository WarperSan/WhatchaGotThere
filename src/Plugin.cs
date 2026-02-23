using BepInEx;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
// Soft
[BepInDependency(RiskOfOptions.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
internal class Plugin : BaseUnityPlugin
{
	private void Awake()
	{
		Configuration.Load(Config);
		Helpers.Dependencies.LoadDependencies(Configuration.Instance);
		Patch.ApplyAll();
		Log.Info($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
	}

	private void OnDestroy()
	{
		Patch.RevertAll();
		Log.Info($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has unloaded!");
	}
}