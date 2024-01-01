using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	private InventorySlot[] inventorySlots;
	// this "Second Inventory" controls where items are moved too if a move action is attempted
	[HideInInspector] public InventoryManager SecondInventory;
	// This is a QOL thing that absolutely isn't required.
	private InventorySlot SelectedSlot;
	// Unused, used to be used, but a design decision was made against it
	[SerializeField] private bool OnlyAllowOrderables = false;

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
		if (OnlyAllowOrderables && !Item.Orderable) {return false;}
		InventorySlot slot = GetFirstFreeSlot();
		if (slot == null)
		{
			return false;
		}
		slot.StoredItem = Item;
		return true;
	}
	public bool AddItem(GameItem[] Items, out GameItem[] Leftover)
	{
		List<GameItem> Leftovers = new();
		int itemindex = 0;
		for (int cat = 0; cat < inventorySlots.Length; cat++)
		{
			InventorySlot slot = inventorySlots[cat];
			if (itemindex < Items.Length && Items[itemindex] == null)
			{
				cat--;
				itemindex++;
			} else if (slot.StoredItem == null && !slot.OutputOnlySlot && itemindex < Items.Length)
			{
				if (!Items[itemindex].Orderable && OnlyAllowOrderables)
				{
					Leftovers.Add(Items[itemindex]);
					itemindex++;
					cat--;
				} else
				{
					slot.StoredItem = Items[itemindex];
					itemindex++;
				}
			}
		}
		if (itemindex < Items.Length)
		{
			for (int i = itemindex; i < Items.Length; i++)
			{
				Leftovers.Add(Items[i]);
			}
		}
		Leftover = Leftovers.ToArray();
		return Leftover.Length != 0;
	}

	public GameItem[] ClearItems()
	{
		List<GameItem> items = new();
		foreach(InventorySlot slot in inventorySlots)
		{
			if (slot.StoredItem != null)
			{
				items.Add(slot.StoredItem.Clone());
				slot.StoredItem = null;
			}
		}
		return items.ToArray();
	}
}
