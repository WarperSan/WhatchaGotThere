using BepInEx.Configuration;

namespace WhatchaGotThere.Helpers;

internal class Configuration
{
	private const string SECTION = "Mod";

	[Flags]
	public enum TargetType : byte
	{
		None = 0,
		Survivors = 1,
		Drones = 1 << 1,
		Allies = 1 << 2,
		All = Survivors | Drones | Allies
	}

	public readonly ConfigEntry<bool> UseAllowedTarget;
	public readonly ConfigEntry<TargetType> AllowedTargets;

	private Configuration(ConfigFile cfg)
	{
		UseAllowedTarget = cfg.Bind(
			new ConfigDefinition(SECTION, "UseAllowedTarget"),
			true,
			new ConfigDescription("Determines if the allowed targets should be used to determine if the equipment is shown.")
		);

		AllowedTargets = cfg.Bind(
			new ConfigDefinition(SECTION, "AllowedTargets"),
			TargetType.All,
			new ConfigDescription("Determines who can display their equipment.")
		);
	}

	/// <summary>
	/// Configuration loaded
	/// </summary>
	public static Configuration? Instance;

	/// <summary>
	/// Loads the configuration from the given configuration file
	/// </summary>
	public static void Load(ConfigFile cfg)
	{
		Instance = new Configuration(cfg);
	}
}