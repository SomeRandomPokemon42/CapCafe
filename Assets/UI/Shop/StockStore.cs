using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockStore : MonoBehaviour
{
	[SerializeField] StoreUIScript ShopUI;
	public List<GameItem> ItemsToStock = new();
	[Header("Shifting Stock")]
	// Set any to -1 to disable
	[SerializeField] private int ShiftWhen = 24;
	[SerializeField] private int HowMany = -1;
	[SerializeField] private int Varience = 0;
	private List<GameItem> StoredStock = new();
	private TimeScript GameTime;

	private void Start()
	{
		GameTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScript>();
		GameTime.HourHasPassed.AddListener(ClearItemsByTime);
	}
	private void ClearItemsByTime()
	{
		if (GameTime.hour == ShiftWhen)
		{
			StoredStock.Clear();
		}
	}
	public void Stock()
	{
		if (HowMany < 1 || ShiftWhen < 1 || Varience < 0)
		{
			ShopUI.StockShop(ItemsToStock.ToArray());
		} else if (StoredStock.Count == 0)
		{
			int StockCount = HowMany + Random.Range(-Varience, Varience+1);
			if (StockCount <= 0) { StockCount = 1; }
			if (StockCount > ItemsToStock.Count) {StockCount = ItemsToStock.Count;}
			for (int i = 0; i < StockCount; i++)
			{
				int potentialItem = Random.Range(0, ItemsToStock.Count);
				if (StoredStock.Contains(ItemsToStock[potentialItem]))
				{
					i--;
					continue;
				} else
				{
					StoredStock.Add(ItemsToStock[potentialItem]);
				}
			}
		} else
		{
			ShopUI.StockShop(StoredStock.ToArray());
		}
	}
}
