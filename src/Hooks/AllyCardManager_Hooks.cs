using RoR2.UI;
using UnityEngine;
using UnityEngine.UI;

namespace WhatchaGotThere.Hooks;

/// <summary>
/// Hooks for <see cref="AllyCardManager"/>
/// </summary>
// ReSharper disable once InconsistentNaming
internal static class AllyCardManager_Hooks
{
	public static void Awake(On.RoR2.UI.AllyCardManager.orig_Awake orig, AllyCardManager self)
	{
		orig(self);

		var canvas = self.transform.GetComponentInParent<Canvas>();
		
		if (canvas == null)
			return;
		
		if (canvas.gameObject.GetComponent<GraphicRaycaster>() != null)
			return;
		
		canvas.gameObject.AddComponent<GraphicRaycaster>();
	}
}