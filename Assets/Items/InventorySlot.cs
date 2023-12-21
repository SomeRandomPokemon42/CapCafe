using UnityEngine;
using UnityEngine.UI;
using static InventoryItems;

public class InventorySlot : MonoBehaviour
{
	public Item storedItem;

	private bool HasItem;
	private Image image;
	private void Start()
	{
		image = transform.GetChild(0).GetComponent<Image>();
	}
	private void Update()
	{
		if ((storedItem == null) == HasItem)
		{
			if (storedItem == null)
			{
				HasItem = false;
				image.enabled = false;
			} else
			{
				HasItem = true;
				image.enabled = true;
				image.sprite = storedItem.image;
			}
		}
	}
}
