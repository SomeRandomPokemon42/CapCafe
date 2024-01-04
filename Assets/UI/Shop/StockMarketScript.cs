using System.Collections.Generic;
using UnityEngine;

public class StockMarketScript : MonoBehaviour
{
	private Dictionary<string, int> StockPrices = new();
	private TimeScript GameTime;
	public float Varience = 2/3*100;

	private void Start()
	{
		GameTime = GetComponent<TimeScript>();
		GameTime.HourHasPassed.AddListener(ClearStocks);
	}
	private void ClearStocks()
	{
		if (GameTime.hour == 24)
		{
			StockPrices.Clear();
		}
	}
	public int STONKS(string ItemName, int baseValue)
	{	
		if (StockPrices.ContainsKey(ItemName))
		{
			return StockPrices[ItemName];
		} else
		{
			// Create New Price
			int lowEnd = Mathf.RoundToInt(baseValue / 100f * Varience);
			int highEnd = Mathf.CeilToInt(baseValue / 100f * (100 + Varience));
			if (lowEnd <= 0) { lowEnd = 1; }
			int price = Random.Range(lowEnd, highEnd + 1);
			StockPrices[ItemName] = price;
			return price;
		}
	}
	public int STONKS(GameItem item)
	{
		return STONKS(item.Name, item.BaseValue);
	}
}
