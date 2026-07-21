namespace WhatchaGotThere.Helpers;

/// <summary>
/// Helper to manage dependencies easier
/// </summary>
internal static class Dependencies
{
	/// <summary>
	/// Checks if the plugin with the given GUID has been loaded
	/// </summary>
	public static bool IsEnabled(string guid) => BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(guid);

	/// <summary>
	/// Loads all the dependencies
	/// </summary>
	public static void LoadDependencies(Configuration? config)
	{
		if (WhatchaGotThere.Dependencies.RiskOfOptions.Dependency.Enabled)
			WhatchaGotThere.Dependencies.RiskOfOptions.Dependency.ApplyConfiguration(config);
	}
}