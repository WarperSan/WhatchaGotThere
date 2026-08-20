using JetBrains.Annotations;
using RoR2;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere.API;

/// <summary>
/// Class that manages the visibility of modded equipment icons
/// </summary>
public static class DisplayHandler
{
	private static readonly List<Func<CharacterMaster, bool?>> DisplayConditions =
	[
		DisableIfNoEquipment,
		DisableIfTargetNotAllowed,
	];

	/// <summary>
	/// Defines if the equipment icon of the given <see cref="CharacterMaster"/> should be displayed
	/// </summary>
	public static bool IsDisplayed(CharacterMaster master)
	{
		foreach (var condition in DisplayConditions)
		{
			var result = condition.Invoke(master);

			if (!result.HasValue)
				continue;

			return result.Value;
		}

		return false;
	}

	/// <summary>
	/// Adds the given condition to the list of conditions
	/// </summary>
	/// <remarks>
	///	If <see langword="null"/> is returned, the next condition will be used. Otherwise, the result will be used.
	/// </remarks>
	[PublicAPI]
	public static void AddCondition(Func<CharacterMaster, bool?> condition)
	{
		DisplayConditions.Add(condition);
	}

	#region Built-in

	private static bool? DisableIfNoEquipment(CharacterMaster master)
	{
		if (master.inventory.currentEquipmentState.equipmentIndex == EquipmentIndex.None)
			return false;

		return null;
	}

	private static bool? DisableIfTargetNotAllowed(CharacterMaster master)
	{
		var config = Configuration.Instance;

		if (config == null)
			return null;

		if (!config.UseAllowedTarget.Value)
			return null;

		var type = config.AllowedTargets.Value;

		// If no target allowed, disable
		if (type == Configuration.TargetType.None)
			return false;

		if (!master.hasBody)
			return null;

		var bodyIndex = master.GetBody().bodyIndex;

		// If master is a survivor, use survivors target
		if (SurvivorCatalog.GetSurvivorIndexFromBodyIndex(bodyIndex) != SurvivorIndex.None)
			return type.HasFlag(Configuration.TargetType.Survivors);

		// If master is a drone, use drones target
		if (DroneCatalog.GetDroneIndexFromBodyIndex(bodyIndex) != DroneIndex.None)
			return type.HasFlag(Configuration.TargetType.Drones);

		// If master is an ally, use allies target
		if (master.minionOwnership.ownerMaster != null)
			return type.HasFlag(Configuration.TargetType.Allies);

		return null;
	}

	#endregion
}