using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonArrayScript : MonoBehaviour
{
	private InventorySlot ThisSlot;
	private InventoryManager Manager;
	[SerializeField] Button CancelButtonObject;
	[SerializeField] Button MoveButtonObject;
	[SerializeField] Button DiscardButtonObject;

	public void Start()
	{
		ThisSlot = transform.parent.GetComponent<InventorySlot>();
		Manager = transform.parent.parent.GetComponent<InventoryManager>();
	}
	public void AttemptToActivate()
	{
		// This is the bare minimum for item actions to be available
		if (ThisSlot.StoredItem != null && ThisSlot.AllowInteracting)
		{
			// Reset
			DiscardButtonObject.gameObject.SetActive(true);
			MoveButtonObject.gameObject.SetActive(true);
			CancelButtonObject.gameObject.SetActive(true);
			DiscardButtonObject.interactable = true;
			MoveButtonObject.interactable = true;
			CancelButtonObject.interactable = true;

			// There isn't a second inventory to move to.
			if (Manager.SecondInventory == null)
			{
				MoveButtonObject.interactable = false;
			}

			// Key Items are undiscardable
			if (ThisSlot.StoredItem.KeyItem)
			{
				DiscardButtonObject.interactable = false;
			}
			Manager.DeselectOldSlot(ThisSlot);
		}
	}
	public void DiscardButton()
	{
		ThisSlot.StoredItem = null;
		CancelButton();
	}
	public void MoveButton()
	{
		if (Manager.SecondInventory.AddItem(ThisSlot.StoredItem))
		{
			ThisSlot.StoredItem = null;
		}
		CancelButton();
	}
	public void CancelButton()
	{
		MoveButtonObject.gameObject.SetActive(false);
		DiscardButtonObject.gameObject.SetActive(false);
		CancelButtonObject.gameObject.SetActive(false);
	}
}
