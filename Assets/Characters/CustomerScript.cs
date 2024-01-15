using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerScript : MonoBehaviour
{
	//[Header("Personal")]
	private enum VerbActions
	{
		EnteringHome,
		ExittingHome,
		GoHome,
		Sitting,
		GoingToCafe,
		GoingToTown,
		Idle //Original name was "WatchAnime", but apperantly thats not a good variable name...
	}
	private VerbActions WhatAmIDoing = VerbActions.Idle;
	private bool HasCafed = false;
	private float WaitTime = 0;
	private int TownStops = 0;
	private HouseScript House = null;
	private TableScript Table = null;
	private int OwedMoney = 0;
	//[Header("Connections")]
	CafeDirections directions;
	TimeScript GameTime;
	StockMarketScript Stonks;
	NavMeshAgent Me;
	TownScript Town;
	CapsuleCollider MyCollision;
	GameObject RequestBox;
	SpriteRenderer RequestA;
	SpriteRenderer RequestB;
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
		Me = GetComponent<NavMeshAgent>();
		MyCollision = GetComponent<CapsuleCollider>();
		Town = GameTime.GetComponent<TownScript>();
		RequestBox = transform.GetComponentInChildren<BillboardScript>().gameObject;
		RequestA = RequestBox.transform.GetChild(0).GetComponent<SpriteRenderer>();
		RequestB = RequestBox.transform.GetChild(1).GetComponent<SpriteRenderer>();
		RequestBox.SetActive(false);
		Setup();
	}

	private Vector3 GetEntranceOffset(TownScript.Direction direction, bool opposite = false)
	{
		switch (direction)
		{
			case TownScript.Direction.up:
				if (!opposite)
				{
					return Vector3.up;
				} else
				{
					return Vector3.down;
				}
			case TownScript.Direction.down:
				if (!opposite)
				{
					return Vector3.down;
				} else
				{
					return Vector3.up;
				}
			case TownScript.Direction.left:
				if (!opposite)
				{
					return Vector3.left;
				} else
				{
					return Vector3.right;
				}
			case TownScript.Direction.right:
				if (!opposite)
				{
					return Vector3.right;
				} else
				{
					return Vector3.left;
				}
			default:
				return Vector3.zero;
		}
	}

	public void Setup() 
	{
		AvoidMilk = Random.value >= 0.5f; // 50% chance
		AvoidMeat = Random.value >= 0.9f; // 10% chance
		AvoidAlcohol = Random.value >= 0.7f; // 30% chance
		Budget = Random.Range(15, 175);
		HasCafed = !GameTime.gameObject.GetComponent<OpenTime>().Open;
		TownStops = Random.Range(1, 7);
		House = Town.GimmeAHouse();
		transform.position = House.EntrancePosition + 3 * GetEntranceOffset(House.Direction, true);
		MyCollision.enabled = false;
		WhatAmIDoing = VerbActions.ExittingHome;
		Me.destination = House.EntrancePosition;
	}

	private static float GetCumulative(Vector3 inputedVector)
	{
		return Mathf.Abs(inputedVector.x) + Mathf.Abs(inputedVector.y) + Mathf.Abs(inputedVector.z);
	}

	private void Update()
	{
		if (WhatAmIDoing == VerbActions.Idle)
		{
			Decide(); // Wish it was this easy IRL. 
		}
		if (Vector3.Distance(Me.destination, new Vector3(transform.position.x, 0, transform.position.z)) < 0.1f && GetCumulative(Me.velocity) < 0.1f)
		{
			if (WhatAmIDoing != VerbActions.Sitting || WhatAmIDoing == VerbActions.Sitting && WaitTime <= 0)
			{
				IHaveArrived();
			}
		}
		if (WaitTime > 0)
		{
			WaitTime -= Time.deltaTime * GameTime.TimeSpeed;
		}
	}

	private void Decide()
	{
		if (!HasCafed && Random.value < 0.10f + Mathf.Clamp(Town.CafePopularity/10f, -0.08f, 0.44f))
		{ // Chance caps at 64% and lowcaps at 2%... Decided randomly in case you were curious
			Me.SetDestination(directions.DecisionPoint);
			WhatAmIDoing = VerbActions.GoingToCafe;
			HasCafed = true;
			return;
		}
		if (TownStops > 0 && Random.value > 0.3333)
		{
			WhatAmIDoing = VerbActions.GoingToTown;
			Me.SetDestination(Town.GimmeSomewhereToGo());
			return;
		}
		// Man, idk, go home and cry
		WhatAmIDoing = VerbActions.GoHome;
		Me.SetDestination(House.EntrancePosition);
	}

	public void IHaveArrived()
	{
		switch(WhatAmIDoing)
		{
			case VerbActions.GoingToCafe:
				if (!EnterTheCafe())
				{
					Town.Complain();
					WhatAmIDoing = VerbActions.Idle;
					RequestBox.SetActive(false);
					directions.SendMoneyToPlayer(OwedMoney);
					OwedMoney = 0;
					Table.OccupiedBy = null;
					return;
				} else
				{
					WhatAmIDoing = VerbActions.Sitting;
					WaitTime = 10f + Random.value * 10;
				}
				break;
			case VerbActions.ExittingHome:
				MyCollision.enabled = true;
				WhatAmIDoing = VerbActions.Idle;
				break;
			case VerbActions.EnteringHome:
				Destroy(gameObject);
				break;
			case VerbActions.GoingToTown:
				WhatAmIDoing = VerbActions.Sitting;
				WaitTime = GameTime.TimeSpeed * Random.value;
				break;
			case VerbActions.GoHome:
				MyCollision.enabled = false;
				WhatAmIDoing = VerbActions.EnteringHome;
				Me.SetDestination(GetEntranceOffset(House.Direction, true)*3 + House.EntrancePosition);
				break;
			default:
				WhatAmIDoing = VerbActions.Idle;
				break;
		}
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
		Table = directions.GetFirstFreeTable();
		Me.SetDestination(Table.SittingPosition);
		Table.OccupiedBy = this;
		RequestBox.SetActive(true);
		RequestA.sprite = desiredItems[0].Icon;
		RequestB.sprite = desiredItems[1].Icon;
		return true;
	}

	public void InteractedWith()
	{
		InventoryManager PlayerInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Directions>().PlayerInventory;
		if (desiredItems[0] != null && PlayerInventory.RemoveItem(desiredItems[0]))
		{
			OwedMoney += Stonks.STONKS(desiredItems[0]);
			desiredItems[0] = null;
			RequestA.sprite = directions.CheckSprite;
		}
		if (desiredItems[1] != null && PlayerInventory.RemoveItem(desiredItems[1]))
		{
			OwedMoney += Stonks.STONKS(desiredItems[1]);
			desiredItems[1] = null;
			RequestB.sprite = directions.CheckSprite;
		}
		if (null == desiredItems[1] && desiredItems[0] == null)
		{
			OwedMoney += Mathf.RoundToInt(WaitTime / 5);
			RequestBox.SetActive(false);
			directions.SendMoneyToPlayer(OwedMoney);
			OwedMoney = 0;
			WhatAmIDoing = VerbActions.Idle;
			Table.OccupiedBy = null;
		}
	}
}