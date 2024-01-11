using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public bool Reusable = false;
	public int Uses = 1;
	public GameItem item;

	public void Pickup()
	{
		Uses -= 1;
		if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<Directions>().PlayerInventory.AddItem(item))
		{
			//This returns false if the adding the item failed, so you get the use back
			Uses += 1;
		}
		if (Reusable)
		{
			Uses += 1;
		}
		else
		{
			if (Uses < 1)
			{
				Destroy(gameObject);
			}
		}
	}
}
