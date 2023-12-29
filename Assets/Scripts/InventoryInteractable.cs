using UnityEngine;
using UnityEngine.Events;

public class InventoryInteractable : Interactable
{
	[SerializeField] private InventoryManager inventoryManager;
	private Directions directions;
    private void Start()
    {
        directions = GameObject.FindGameObjectWithTag("GameController").GetComponent<Directions>();
    }
    public override void Interact()
	{
        //Inventory Stuff 
        inventoryManager.SecondInventory = directions.PlayerInventory;
        directions.PlayerInventory.SecondInventory = inventoryManager;
        //Base
        base.Interact();
	}
}
