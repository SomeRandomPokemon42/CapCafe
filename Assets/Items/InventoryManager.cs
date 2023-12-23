using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private InventorySlot[] inventorySlots;

    void Start()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
    }

    private InventorySlot GetFirstFreeSlot()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.StoredItem == null)
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
