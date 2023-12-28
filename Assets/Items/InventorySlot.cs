using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public GameItem StoredItem = null;
	private Image ItemImage;
	private RectTransform MyTransform;
	private Image MyImage;
	void Start()
	{
		MyTransform = GetComponent<RectTransform>();
		MyImage = GetComponent<Image>();
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
