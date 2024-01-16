using UnityEngine;
using UnityEngine.Events;

public class StorageObject : MonoBehaviour
{
	[SerializeField] private InventoryManager inventoryManager;

	[SerializeField] private int InventoryLimit = 4;
	public GameItem[] storedItems;
	public UnityEvent Adjusted = new();

	private void Start()
	{
		storedItems = new GameItem[InventoryLimit];
	}
	public void OnOpen()
	{
		inventoryManager.AddItem(storedItems, out _);
	}
	public void OnClose()
	{
		storedItems = inventoryManager.ClearItems();
		inventoryManager.DeselectOldSlot(Bounced: true);
		Adjusted.Invoke();
	}
}
