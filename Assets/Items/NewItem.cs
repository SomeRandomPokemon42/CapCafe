using UnityEngine;

public class NewItem : MonoBehaviour
{
    private InventoryItems database;
    [SerializeField] private Sprite ItemImage;
    [SerializeField] private string ItemText;
    [SerializeField] private int ItemId;
    [SerializeField] private InventoryItems.ItemType ItemType;
    private void Start()
    {
        database = GetComponentInParent<InventoryItems>();
        database.ItemDatabase.Add(ItemId, new InventoryItems.Item(ItemId, ItemText, ItemType, ItemImage));
    }
}
