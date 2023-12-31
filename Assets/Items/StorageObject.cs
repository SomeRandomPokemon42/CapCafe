using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StorageObject : MonoBehaviour
{
	[SerializeField] private InventoryManager inventoryManager;
	private UIBoxHandler boxHandler;

	[SerializeField] private int InventoryLimit = 4;
	public GameItem[] storedItems;
	private bool PreviousStatus = false;
	private bool DoIExist = false;
	public UnityEvent Adjusted = new();

	public void Exist()
	{
		DoIExist = true;
	}
	private void Start()
	{
		boxHandler = inventoryManager.gameObject.GetComponent<UIBoxHandler>();
		storedItems = new GameItem[InventoryLimit];
		GetComponent<InventoryInteractable>().TriggerThis.AddListener(Exist);
	}
	private void Update()
	{
		if (boxHandler.UIDisplayed != PreviousStatus)
		{
			PreviousStatus = boxHandler.UIDisplayed;
			if (PreviousStatus && DoIExist)
			{
				OnOpen();
				Adjusted.Invoke();
			} else if (DoIExist)
			{
				DoIExist = false;
				OnClose();
				Adjusted.Invoke();
			}
		}
	}
	private void OnOpen()
	{
		inventoryManager.AddItem(storedItems, out _);
	}
	private void OnClose()
	{
		storedItems = inventoryManager.ClearItems();
	}
}
