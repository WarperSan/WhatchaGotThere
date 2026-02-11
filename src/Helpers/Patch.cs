using WhatchaGotThere.Hooks;

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
		On.RoR2.UI.AllyCardManager.Awake += AllyCardManager_Hooks.Awake;

		Log.Debug("All patches applied.");
	}

	/// <summary>
	///     Reverts every patch applied by this mod
	/// </summary>
	public static void RevertAll()
	{
		On.RoR2.UI.AllyCardManager.Awake -= AllyCardManager_Hooks.Awake;

		Log.Debug("All patches reverted.");
	}
}