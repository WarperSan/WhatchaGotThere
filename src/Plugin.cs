using BepInEx;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere;

// ReSharper disable once StringLiteralTypo
[BepInAutoPlugin("dev.warpersan.whatchagotthere")]
internal partial class Plugin : BaseUnityPlugin
{
	private void Awake()
	{
		Configuration.Load(Config);
		Patch.ApplyAll();
		Log.Info($"{Name} v{Version} has loaded!");
	}
}