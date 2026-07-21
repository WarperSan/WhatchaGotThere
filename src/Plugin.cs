using BepInEx;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere;

// ReSharper disable once StringLiteralTypo
[BepInAutoPlugin("dev.warpersan.watchagotthere")]
[BepInDependency(RiskOfOptions.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
internal partial class Plugin : BaseUnityPlugin
{
	private void Awake()
	{
		Configuration.Load(Config);
		Helpers.Dependencies.LoadDependencies(Configuration.Instance);
		Patch.ApplyAll();
		Log.Info($"{Name} v{Version} has loaded!");
	}
}