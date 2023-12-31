using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStoredItems : MonoBehaviour
{
	[SerializeField] private SpriteRenderer[] Plates;
	[SerializeField] private StorageObject Storage;

	private void Start()
	{
		if (Storage == null)
		{
			Storage = GetComponent<StorageObject>();
		}
		Storage.Adjusted.AddListener(UpdateDisplay);
	}
	public void UpdateDisplay()
	{
		int PlatesUsed = 0;
		foreach (SpriteRenderer plate in Plates)
		{
			if (PlatesUsed < Storage.storedItems.Length && Storage.storedItems[PlatesUsed] != null)
			{
				plate.enabled = true;
				plate.sprite = Storage.storedItems[PlatesUsed].Icon;
				PlatesUsed++;
			} else
			{
				plate.enabled = false;
			}
		}
	}
}
