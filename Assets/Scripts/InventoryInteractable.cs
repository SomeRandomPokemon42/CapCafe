using UnityEngine;

public class InventoryInteractable : Interactable
{
	public InventoryManager inventoryManager;
	private Directions directions;
	private void Start()
	{
		directions = GameObject.FindGameObjectWithTag("GameController").GetComponent<Directions>();
	}
	public override void Interact()
	{
		//Save items
		if (inventoryManager.GetComponentInParent<UIBoxHandler>().UIDisplayed && inventoryManager.MyStorage != null)
		{
			inventoryManager.MyStorage.OnClose();
		}
		//Load items
		inventoryManager.MyStorage = gameObject.GetComponent<StorageObject>();
		if (inventoryManager.MyStorage != null )
		{
			inventoryManager.MyStorage.OnOpen();
		}
		//Inventory Stuff 
		inventoryManager.SecondInventory = directions.PlayerInventory;
		directions.PlayerInventory.SecondInventory = inventoryManager;
		//Base
		base.Interact();
	}
}
