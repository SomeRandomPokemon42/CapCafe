using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private InventorySlot[] inventorySlots;
    // this "Second Inventory" controls where items are moved too if a move action is attempted
    public InventoryManager SecondInventory;

    void Start()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
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
