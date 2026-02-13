using HarmonyLib;
using RoR2;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

namespace WhatchaGotThere.Patches;

[HarmonyPatch(typeof(AllyCardController))]
internal static class AllyCardController_Patches
{
	[HarmonyPatch(nameof(AllyCardController.Awake))]
	[HarmonyPostfix]
	private static void Awake_Postfix(AllyCardController __instance)
	{
		var equipmentSlot = new GameObject(
			nameof(WhatchaGotThere) + " EquipmentSlot",
			typeof(RectTransform),
			typeof(LayoutElement),
			typeof(Image),
			typeof(TooltipProvider)
		);
		equipmentSlot.transform.SetParent(__instance.rectTransform, false);
		equipmentSlot.AddComponent<MPEventSystemLocator>();
		equipmentSlot.AddComponent<HGButton>();

		var siblingIndex = __instance.rectTransform.Find("Portrait")?.GetSiblingIndex() ?? -1;

		if (siblingIndex != -1)
			equipmentSlot.transform.SetSiblingIndex(siblingIndex + 1);
		
		var equipmentSlotRect = equipmentSlot.GetComponent<RectTransform>();
		equipmentSlotRect.sizeDelta = new Vector2(48f, 48f);
		
		var equipmentSlotLayoutElement = equipmentSlot.GetComponent<LayoutElement>();
		equipmentSlotLayoutElement.preferredWidth = equipmentSlotRect.rect.width;
		equipmentSlotLayoutElement.preferredHeight = equipmentSlotRect.rect.height;

		var equipmentSlotImage = equipmentSlot.GetComponent<Image>();
		equipmentSlotImage.color = Color.clear;
		equipmentSlotImage.raycastTarget = false;
		
		var equipmentSlotButton = equipmentSlot.GetComponent<HGButton>();
		equipmentSlotButton.image = equipmentSlotImage;
		
		var equipmentIcon = equipmentSlot.AddComponent<EquipmentIcon>();
		equipmentIcon.tooltipProvider = equipmentSlot.GetComponent<TooltipProvider>();
		
		var displayRoot = new GameObject("DisplayRoot", typeof(RectTransform));
		displayRoot.transform.SetParent(equipmentSlot.transform, false);
		equipmentIcon.displayRoot = displayRoot;
		
		var displayRootRect = displayRoot.GetComponent<RectTransform>();
		displayRootRect.anchorMin = Vector2.zero;
		displayRootRect.anchorMax = Vector2.one;
		displayRootRect.offsetMin = Vector2.zero;
		displayRootRect.offsetMax = Vector2.zero;

		var iconPanel = new GameObject(
			"IconPanel",
			typeof(RectTransform),
			typeof(RawImage)
		);
		iconPanel.transform.SetParent(displayRoot.transform, false);
		equipmentIcon.iconImage = iconPanel.GetComponent<RawImage>();
		
		var iconPanelRect = iconPanel.GetComponent<RectTransform>();
		iconPanelRect.anchorMin = Vector2.zero;
		iconPanelRect.anchorMax = Vector2.one;
		iconPanelRect.offsetMin = Vector2.zero;
		iconPanelRect.offsetMax = Vector2.zero;

		var cooldownText = new GameObject(
			"Cooldown Text",
			typeof(RectTransform),
			typeof(HGTextMeshProUGUI)
		);
		cooldownText.transform.SetParent(displayRoot.transform, false);
		
		var cooldownTextRect = cooldownText.GetComponent<RectTransform>();
		cooldownTextRect.anchorMin = Vector2.zero;
		cooldownTextRect.anchorMax = Vector2.one;
		cooldownTextRect.offsetMin = Vector2.zero;
		cooldownTextRect.offsetMax = Vector2.zero;
		
		var cooldownTextGUI = cooldownText.GetComponent<HGTextMeshProUGUI>();
		cooldownTextGUI.alignment = TextAlignmentOptions.Center;
		equipmentIcon.cooldownText = cooldownTextGUI;
	}

	[HarmonyPatch(nameof(AllyCardController.UpdateInfo))]
	[HarmonyPostfix]
	private static void UpdateInfo_Postfix(AllyCardController __instance)
	{
		var equipmentIcon = __instance.GetComponentInChildren<EquipmentIcon>();
		
		if (equipmentIcon == null)
			return;

		var showIcon = ShouldShowIcon(__instance);

		equipmentIcon.targetInventory = __instance.sourceMaster.inventory;
		equipmentIcon.gameObject.SetActive(showIcon);
	}

	private static bool ShouldShowIcon(AllyCardController controller)
	{
		if (controller.sourceMaster.inventory == null)
			return false;

		var equipmentIndex = controller.sourceMaster.inventory.GetEquipmentIndex();

		// If no equipment, skip
		if (equipmentIndex == EquipmentIndex.None)
			return false;

		return true;
	}
}