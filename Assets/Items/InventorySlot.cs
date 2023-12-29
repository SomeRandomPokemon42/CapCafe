using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public GameItem StoredItem = null;
	private Image ItemImage;
	public bool OutputOnlySlot = false;
	void Start()
	{
		ItemImage = null;
		foreach (Transform t in transform)
		{
			if (t.GetComponent<Image>() != null)
			{
				ItemImage = t.GetComponent<Image>();
			}
		}
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
