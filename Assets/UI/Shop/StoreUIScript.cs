using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUIScript : MonoBehaviour
{
    [SerializeField] GameObject StoreItemPrefab;
    private Transform StoreItems;
	private StockMarketScript Stonks;

	private void Start()
	{
		StoreItems = GetComponentInChildren<GridLayout>().transform;
		Stonks = GameObject.FindGameObjectWithTag("GameController").GetComponent<StockMarketScript>();
	}

	public void ClearShop()
	{
		foreach (Transform child in StoreItems)
		{ 
			// Its ok, its done with cyanide, quick and painless...
			Destroy(child.gameObject);
		}
	}
	public void StockShop(GameItem[] Items)
	{
		foreach (GameItem item in Items)
		{
			GameObject ShopItem = Instantiate(StoreItemPrefab, StoreItems);
			StoreItemScript ShopItemScript = ShopItem.GetComponent<StoreItemScript>();
			ShopItemScript.Item = item;
			ShopItemScript.Price = Stonks.STONKS(item.BaseValue);
		}
	}
}
