using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public GameItem StoredItem = null;
	private Image ItemImage;

	void Start()
	{
		ItemImage = transform.GetChild(0).GetComponent<Image>();
	}
	private void Update()
	{
		if (StoredItem != null)
		{
			ItemImage.enabled = true;
			ItemImage.sprite = StoredItem.Icon;
		}
		else
		{
			ItemImage.enabled = false;
		}
	}
}
