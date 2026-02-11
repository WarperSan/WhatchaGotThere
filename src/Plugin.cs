using BepInEx;
using Log = WhatchaGotThere.Helpers.Log;
using Patch = WhatchaGotThere.Helpers.Patch;

namespace WhatchaGotThere;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
internal class Plugin : BaseUnityPlugin
{
	private void Awake()
	{
		Patch.ApplyAll();
		Log.Info($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
	}

	private void OnDestroy()
	{
		Patch.RevertAll();
		Log.Info($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has unloaded!");
	}
}