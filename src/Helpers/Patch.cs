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
		Log.Debug("All patches applied.");
	}

	/// <summary>
	///     Reverts every patch applied by this mod
	/// </summary>
	public static void RevertAll()
	{
		Log.Debug("All patches reverted.");
	}
}