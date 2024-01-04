using UnityEngine;
using UnityEngine.UI;

public class StoreItemScript : MonoBehaviour
{
	public GameItem Item;
	public int Price = 0;
	[Header("Assign In Inspector")]
	[SerializeField] Image ItemImage = null;
	PriceTagScript priceTag;
	public bool OnCooldown = false;

	// Start is called before the first frame update
	void Awake()
	{
		priceTag = GetComponentInChildren<PriceTagScript>();
	}
	public void Ping()
	{
		priceTag.Price = Price;
		ItemImage.sprite = Item.Icon;
	}
	public void Purchase()
	{
		if (OnCooldown)
		{
			return;
		}
		OnCooldown = true;
		Invoke(nameof(DisableCooldown), 0.1f);
		GameObject controller = GameObject.FindGameObjectWithTag("GameController");
		InventoryManager playerInventory = controller.GetComponent<Directions>().PlayerInventory;
		PlayerMoney playerMoney = controller.GetComponent<PlayerMoney>();
		if (playerMoney.amount >= Price && playerInventory.AddItem(Item))
		{
			playerMoney.amount -= Price;
		}
	}
	private void DisableCooldown()
	{
		OnCooldown = false;
	}
}
