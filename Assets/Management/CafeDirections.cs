using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeDirections : MonoBehaviour
{
	public TableScript[] Tables;
	public StorageObject[] DisplayCases;
	public Vector3 DecisionPoint = new(29, 0, 1.5f);
	public Sprite CheckSprite;

	public void SendMoneyToPlayer(int Amount)
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMoney>().amount += Amount;
	}

	public TableScript GetFirstFreeTable()
	{
		foreach (TableScript table in Tables)
		{
			if (table.OccupiedBy == null)
			{
				return table;
			}
		}
		return null;
	}
	public GameItem[] GetDisplayedItems()
	{
		List<GameItem> items = new();
		foreach (StorageObject storageObject in DisplayCases)
		{
			foreach (GameItem item in storageObject.storedItems)
			{
				if (item != null)
				{
					items.Add(item);
				}
			}
		}
		return items.ToArray();
	}
}
