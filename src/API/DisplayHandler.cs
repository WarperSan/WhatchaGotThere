using JetBrains.Annotations;
using RoR2;
using WhatchaGotThere.Helpers;

namespace WhatchaGotThere.API;

/// <summary>
/// Class that manages the visibility of modded equipment icons
/// </summary>
public static class DisplayHandler
{
	private static readonly List<Predicate<CharacterMaster>> DisplayPredicates = [];

	/// <summary>
	/// Defines if the equipment icon of the given <see cref="CharacterMaster"/> should be displayed
	/// </summary>
	public static bool IsDisplayed(CharacterMaster master)
	{
		// If no equipment, disable
		if (master.inventory.currentEquipmentState.equipmentIndex == EquipmentIndex.None)
			return false;

		// If no configuration, disable
		if (Configuration.Instance != null)
		{
			var type = Configuration.Instance.AllowedTargets.Value;

			// If no target allowed, disable
			if (type == Configuration.TargetType.None)
				return false;

			if (master.hasBody)
			{
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
			}
		}

		// If no predicate, enable
		if (DisplayPredicates.Count == 0)
			return true;

		// If any predicate enable, enable
		return DisplayPredicates.Any(predicate => predicate.Invoke(master));
	}

	/// <summary>
	/// Adds the given <see cref="Predicate{T}"/> to the list of conditions
	/// </summary>
	[PublicAPI]
	public static void AddCondition(Predicate<CharacterMaster> predicate)
	{
		DisplayPredicates.Add(predicate);
	}
}