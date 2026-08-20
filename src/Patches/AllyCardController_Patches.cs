using HarmonyLib;
using RoR2;
using RoR2.UI;
using WhatchaGotThere.API;
using WhatchaGotThere.Helpers;

// ReSharper disable InconsistentNaming

namespace WhatchaGotThere.Patches;

[HarmonyPatch(typeof(AllyCardController))]
internal static class AllyCardController_Patches
{
	[HarmonyPatch(nameof(AllyCardController.Awake))]
	[HarmonyPostfix]
	private static void Awake_Postfix(AllyCardController __instance)
	{
		AllyCardEquipmentIconBuilder.CreateIcon(__instance.rectTransform);
		__instance.gameObject.AddComponent<AllyCardData>();
	}

	[HarmonyPatch(nameof(AllyCardController.ShouldWeUpdate))]
	[HarmonyPostfix]
	private static void ShouldWeUpdate_Postfix(AllyCardController __instance, ref bool __result)
	{
		// Keep result
		if (__result)
			return;

		if (!AllyCardData.TryGet(__instance, out var data))
			return;

		var currentEquipmentIndex = __instance.sourceMaster.inventory.currentEquipmentIndex;

		if (currentEquipmentIndex == data.EquipmentIndex)
			return;

		__result = true;
	}

	[HarmonyPatch(nameof(AllyCardController.UpdateInfo))]
	[HarmonyPostfix]
	private static void UpdateInfo_Postfix(AllyCardController __instance)
	{
		// Get data
		if (!AllyCardData.TryGet(__instance, out var data))
			return;

		// Get icon
		var equipmentIcon = data.EquipmentIcon;

		if (equipmentIcon == null)
			return;

		data.UpdateCache();

		// Update UI
		if (!__instance.sourceMaster)
			return;

		var shouldDisplay = DisplayHandler.IsDisplayed(__instance.sourceMaster);

		equipmentIcon.targetInventory = __instance.sourceMaster.inventory;
		equipmentIcon.gameObject.SetActive(shouldDisplay);
	}
}