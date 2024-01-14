using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
	CafeDirections directions;
	TimeScript GameTime;
	StockMarketScript Stonks;
	[Header("Prefrences")]
	bool AvoidMilk = false;
	bool AvoidMeat = false;
	bool AvoidAlcohol = false;
	int Budget = 0;
	GameItem[] desiredItems;

	private void Awake()
	{
		GameTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScript>();
		directions = GameObject.FindGameObjectWithTag("GameController").GetComponent<Directions>().Cafe;
		Stonks = GameTime.gameObject.GetComponent<StockMarketScript>();
	}

	public void Setup() 
	{
		AvoidMilk = Random.value >= 0.5f; // 50% chance
		AvoidMeat = Random.value >= 0.9f; // 10% chance
		AvoidAlcohol = Random.value >= 0.7f; // 30% chance
		Budget = Random.Range(15, 175);
	}

	public bool EnterTheCafe()
	{
		// Can I actually do this???
		if (directions.GetFirstFreeTable() == null)
		{
			// No free tables...
			return false;
		}
		// Decide on an item
		GameItem[] Items = directions.GetDisplayedItems();
		List<GameItem> AllowedItems = new();
		List<GameItem> AllowedDrinks = new();
		foreach (GameItem item in Items)
		{
			if ((item.HasAlcohol && AvoidAlcohol) ||
				(item.HasMeat && AvoidMeat) ||
				(item.HasMilk && AvoidMilk)
				)
			{ //Avoid some items.
				continue;
			}
			if (!item.Orderable && Random.value > 0.05f)
			{ //Non Orderables have a 95% chance to veto
				continue;
			}
			if (AllowedItems.Contains(item) || AllowedDrinks.Contains(item))
			{ //No Duplicates
				continue;
			}
			if (Stonks.STONKS(item) >= Budget/2)
			{ //Can't Afford
				continue;
			}
			if (item.IsBeverage)
			{
				AllowedDrinks.Add(item);
			} else
			{
				AllowedItems.Add(item);
			}
		}
		if (AllowedItems.Count + AllowedDrinks.Count <= 1)
		{
			// No items
			return false;
		}
		float whatDoIActuallyGet = Random.value; //50% chance for food and drink, 35% for 2 drinks, and 15% for 2 food.
		desiredItems = new GameItem[2];
		// First item
		if (whatDoIActuallyGet <= 0.85)
		{
			desiredItems[0] = AllowedDrinks[Random.Range(0, AllowedDrinks.Count)];
		} else
		{
			desiredItems[0] = AllowedItems[Random.Range(0, AllowedItems.Count)];
		}
		// Second item
		if (whatDoIActuallyGet <= 0.85 && whatDoIActuallyGet > 0.5)
		{
			desiredItems[1] = AllowedDrinks[Random.Range(0, AllowedDrinks.Count)];
		} else
		{
			desiredItems[1] = AllowedItems[Random.Range(0, AllowedItems.Count)];
		}
		return true;
	}
}
