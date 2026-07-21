using System.Runtime.CompilerServices;
using RiskOfOptions;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere.Dependencies.RiskOfOptions;

/// <summary>
/// Class handling the dependency with <see cref="RiskOfOptionsPlugin"/>
/// </summary>
internal static class Dependency
{
	/// <summary>
	/// Checks if <see cref="RiskOfOptionsPlugin"/> is loaded
	/// </summary>
	public static bool Enabled => Helpers.Dependencies.IsEnabled(PluginInfo.PLUGIN_GUID);

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static void ApplyConfiguration(Configuration? config)
	{
		throw new NotImplementedException();
	}
}