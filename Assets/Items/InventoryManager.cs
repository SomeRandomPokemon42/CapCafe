using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	private InventorySlot[] inventorySlots;
	// this "Second Inventory" controls where items are moved too if a move action is attempted
	public InventoryManager SecondInventory;
	// This is a QOL thing that absolutely isn't required.
	private InventorySlot SelectedSlot;

	void Start()
	{
		inventorySlots = GetComponentsInChildren<InventorySlot>();
	}

	public void DeselectOldSlot(InventorySlot slot = null, bool Bounced = false)
	{
		if (SelectedSlot != null && SelectedSlot != slot)
		{
			SelectedSlot.HideButtonArray();
		}
		SelectedSlot = slot;
		if (!Bounced && SecondInventory != null)
		{
			SecondInventory.DeselectOldSlot(Bounced:true);
		}
	}

	private InventorySlot GetFirstFreeSlot()
	{
		foreach (InventorySlot slot in inventorySlots)
		{
			if (slot.StoredItem == null && !slot.OutputOnlySlot)
			{
				return slot;
			}
		}
		return null;
	}

	public bool AddItem(GameItem Item)
	{
		InventorySlot slot = GetFirstFreeSlot();
		if (slot == null)
		{
			return false;
		}
		slot.StoredItem = Item;
		return true;
	}
}
