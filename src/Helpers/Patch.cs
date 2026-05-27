using HarmonyLib;

namespace WhatchaGotThere.Helpers;

/// <summary>
///     Class helping for patching stuff
/// </summary>
internal static class Patch
{
	/// <summary>
	///     Applies every patch needed by this mod
	/// </summary>
	public static void ApplyAll()
	{
		var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

		harmony.PatchAll(typeof(Patches.AllyCardController_Patches));
		harmony.PatchAll(typeof(Patches.AllyCardManager_Patches));

		Log.Debug("All patches applied.");
	}
}