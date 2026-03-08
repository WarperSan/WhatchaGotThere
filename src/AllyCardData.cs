using RoR2;
using RoR2.UI;
using UnityEngine;

namespace WhatchaGotThere;

/// <summary>
/// Script used to cache certain values
/// </summary>
[RequireComponent(typeof(AllyCardController))]
internal class AllyCardData : MonoBehaviour
{
	public static readonly Dictionary<AllyCardController, AllyCardData> CachedInstances = new();

	public AllyCardController? Controller     { get; private set; }
	public EquipmentIcon?      EquipmentIcon  { get; private set; }
	public EquipmentIndex      EquipmentIndex { get; private set; }

	private void Awake()
	{
		Controller = gameObject.GetComponent<AllyCardController>();
		EquipmentIcon = gameObject.GetComponentInChildren<EquipmentIcon>();
		EquipmentIndex = EquipmentIndex.None;

		CachedInstances.Add(Controller, this);
	}

	/// <summary>
	/// Updates the cached values
	/// </summary>
	public void UpdateCache()
	{
		if (Controller != null)
			EquipmentIndex = Controller.sourceMaster.inventory.currentEquipmentState.equipmentIndex;
		else
			EquipmentIndex = EquipmentIndex.None;
	}

	private void OnDestroy()
	{
		if (Controller != null)
			CachedInstances.Remove(Controller);
	}
}