using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIScript : MonoBehaviour
{
    [SerializeField] GameObject StoreItemPrefab;
    private Transform StoreItems;
	private StockMarketScript Stonks;

	private void Start()
	{
		StoreItems = GetComponentInChildren<GridLayoutGroup>().transform;
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
		ClearShop();
		foreach (GameItem item in Items)
		{
			GameObject ShopItem = Instantiate(StoreItemPrefab, StoreItems);
			StoreItemScript ShopItemScript = ShopItem.GetComponent<StoreItemScript>();
			ShopItemScript.Item = item;
			ShopItemScript.Price = Stonks.STONKS(item);
			ShopItemScript.Ping();
		}
	}
}
