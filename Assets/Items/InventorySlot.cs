using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public GameItem StoredItem = null;
	public Image ItemImage;
	public bool OutputOnlySlot = false;
	public bool AllowInteracting = true;
	public UnityEvent SlotModified;

	void Start()
	{
		SlotModified = new UnityEvent();

		ItemImage = null;
		foreach (Transform t in transform)
		{
			if (t.GetComponent<Image>() != null)
			{
				ItemImage = t.GetComponent<Image>();
				break;
			}
		}
	}
	private void Update()
	{
		if (StoredItem != null)
		{
			if (!ItemImage.enabled)
			{
				SlotModified.Invoke();
			}
			ItemImage.enabled = true;
			ItemImage.sprite = StoredItem.Icon;
		}
		else
		{
			if (ItemImage.enabled)
			{
				SlotModified.Invoke();
			}
			ItemImage.enabled = false;
		}
	}

	public void HideButtonArray()
	{
		gameObject.GetComponentInChildren<ButtonArrayScript>().CancelButton();
	}
}
