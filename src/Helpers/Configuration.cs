using BepInEx.Configuration;

namespace WhatchaGotThere.Helpers;

internal class Configuration
{
	private const string SECTION = "Mod";

	public readonly ConfigEntry<bool> AllowSurvivorsTarget;
	public readonly ConfigEntry<bool> AllowDronesTarget;
	public readonly ConfigEntry<bool> AllowAlliesTarget;

	private Configuration(ConfigFile cfg)
	{
		AllowSurvivorsTarget = cfg.Bind(
			new ConfigDefinition(SECTION, "AllowSurvivorsTarget"),
			true,
			new ConfigDescription("Determines if survivors can display their equipment.")
		);

		AllowDronesTarget = cfg.Bind(
			new ConfigDefinition(SECTION, "AllowDronesTarget"),
			true,
			new ConfigDescription("Determines if drones can display their equipment.")
		);

		AllowAlliesTarget = cfg.Bind(
			new ConfigDefinition(SECTION, "AllowAlliesTarget"),
			true,
			new ConfigDescription("Determines if allies can display their equipment.")
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