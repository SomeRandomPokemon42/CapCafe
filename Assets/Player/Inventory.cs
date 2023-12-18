using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
    private InventoryItems ItemDatabase;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private void Start()
    {
        ItemDatabase = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryItems>();
    }

    public bool TestForSlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].storedItem == null)
            {
                return true;
            }
        }
        return false;
    }
    public bool AddItem(int id)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].storedItem == null)
            {
                slots[i].storedItem = ItemDatabase.CreateItem(id);
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(int id)
    {
        for(int i = 0;i < slots.Count;i++)
        {
            if (slots[i].storedItem != null)
            {
                if (slots[i].storedItem.id == id)
                {
                    slots[i].storedItem = null;
                    return true;
                }
            }
        }
        return false;
    }

}
