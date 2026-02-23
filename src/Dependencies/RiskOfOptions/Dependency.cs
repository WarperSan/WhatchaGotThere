using System.Runtime.CompilerServices;
using RiskOfOptions;
using RiskOfOptions.Options;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere.Dependencies.RiskOfOptions;

// https://github.com/WhiteSpike/InteractiveTerminalAPI
internal static class Dependency
{
	public static bool Enabled => Helpers.Dependencies.IsEnabled(PluginInfo.PLUGIN_GUID);

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static void ApplyConfiguration(Configuration? configuration)
	{
		if (configuration == null)
		{
			Log.Info($"Tried to load the configurations into '{nameof(RiskOfOptions)}', but none were given.");
			return;
		}

		ModSettingsManager.AddOption(
			new ChoiceOption(
				configuration.AllowedTargets,
				false
			)
		);
	}
}