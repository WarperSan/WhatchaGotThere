using HarmonyLib;
using RoR2.UI;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

namespace WhatchaGotThere.Patches;

[HarmonyPatch(typeof(AllyCardManager))]
internal static class AllyCardManager_Patches
{
	[HarmonyPatch(nameof(AllyCardManager.Awake))]
	[HarmonyPostfix]
	private static void Awake_Postfix(AllyCardManager __instance)
	{
		var canvas = __instance.transform.GetComponentInParent<Canvas>();
		
		if (canvas == null)
			return;
		
		if (canvas.gameObject.GetComponent<GraphicRaycaster>() != null)
			return;
		
		canvas.gameObject.AddComponent<GraphicRaycaster>();
	}
}