using HarmonyLib;

namespace WhatchaGotThere.Helpers;

/// <summary>
///     Class helping for patching stuff
/// </summary>
internal static class Patch
{
	private static Harmony? _harmony;

	/// <summary>
	///     Applies every patch needed by this mod
	/// </summary>
	public static void ApplyAll()
	{
		if (_harmony != null)
		{
			Log.Debug("Unpatching the existing harmony instance.");
			RevertAll();
		}

		_harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

		_harmony.PatchAll(typeof(Patches.Example_Patches));

		Log.Debug("All patches applied.");
	}

	/// <summary>
	///     Reverts every patch applied by this mod
	/// </summary>
	public static void RevertAll()
	{
		if (_harmony == null)
			return;

		_harmony.UnpatchSelf();

		_harmony = null;

		Log.Debug("All patches reverted.");
	}
}