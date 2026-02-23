namespace WhatchaGotThere.Helpers;

/// <summary>
/// Helper to manage dependencies easier
/// </summary>
internal static class Dependencies
{
	private static readonly Dictionary<string, bool> CachedStatus = [];

	/// <summary>
	/// Checks if the plugin with the given GUID has been loaded
	/// </summary>
	public static bool IsEnabled(string guid)
	{
		if (CachedStatus.TryGetValue(guid, out var isEnabled))
			return isEnabled;

		isEnabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(guid);
		CachedStatus.Add(guid, isEnabled);
		return isEnabled;
	}

	/// <summary>
	/// Loads all the dependencies
	/// </summary>
	public static void LoadDependencies(Configuration? config)
	{
		if (WhatchaGotThere.Dependencies.RiskOfOptions.Dependency.Enabled)
			WhatchaGotThere.Dependencies.RiskOfOptions.Dependency.ApplyConfiguration(config);
	}
}