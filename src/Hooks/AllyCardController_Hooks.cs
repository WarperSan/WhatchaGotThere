using RoR2;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WhatchaGotThere.Hooks;

/// <summary>
/// Hooks for <see cref="AllyCardController"/>
/// </summary>
// ReSharper disable once InconsistentNaming
internal static class AllyCardController_Hooks
{
	public static void Awake(On.RoR2.UI.AllyCardController.orig_Awake orig, AllyCardController self)
	{
		orig(self);
		
		var equipmentSlot = new GameObject(
			nameof(WhatchaGotThere) + " EquipmentSlot",
			typeof(RectTransform),
			typeof(LayoutElement),
			typeof(Image),
			typeof(MPEventSystemLocator),
			typeof(HGButton),
			typeof(TooltipProvider)
		);
		equipmentSlot.transform.SetParent(self.rectTransform, false);

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

	public static void UpdateInfo(On.RoR2.UI.AllyCardController.orig_UpdateInfo orig, AllyCardController self)
	{
		orig(self);
		
		var equipmentIcon = self.GetComponentInChildren<EquipmentIcon>();
		
		if (equipmentIcon == null)
			return;

		var showIcon = ShouldShowIcon(self);

		equipmentIcon.targetInventory = self.sourceMaster.inventory;
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