using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingScript : MonoBehaviour
{
	public RecipeBook.CookingType cookingType;
	// Framework
	private RecipeBook book;
	private TimeScript gameTime;
	private Button StartCraftingButton;
	private InventoryManager InventorySlots;
	private InventorySlot OutputSlots;
	private InventorySlot[] InputSlots;
	private Slider ProgressMeter;
	private bool Cooking = false;
	private float CraftingTimer = 0;
	private void Start()
	{
		book = GameObject.FindGameObjectWithTag("GameController").GetComponent<RecipeBook>();
		gameTime = book.gameObject.GetComponent<TimeScript>();
		StartCraftingButton = GetComponent<Button>();
		InventorySlots = transform.parent.GetComponentInChildren<InventoryManager>();
		ProgressMeter = transform.parent.GetComponentInChildren<Slider>();

		InventorySlot[] AllSlots = InventorySlots.GetComponentsInChildren<InventorySlot>();
		List<InventorySlot> InpSlots = new();
		List<InventorySlot> OutSlots = new();
		foreach (InventorySlot slot in AllSlots)
		{
			if (slot.OutputOnlySlot)
			{
				OutSlots.Add(slot);
			} else
			{
				InpSlots.Add(slot);
			}
		}
		InputSlots = InpSlots.ToArray();
		OutputSlots = OutSlots[0];
		StartCraftingButton.onClick.AddListener(LetEmCook);
	}
	private void Update()
	{
		if (CraftingTimer > 0)
		{
			CraftingTimer -= Time.deltaTime;
			ProgressMeter.value = ProgressMeter.maxValue - CraftingTimer;
		}
	}
	private void LetEmCook()
	{
		// Check if we can do this
		// GetWhatsCooking is an expensive function, cache that shit.
		Recipe WhatsCooking = book.GetWhatsCooking(cookingType, InputSlots);
		if (Cooking || WhatsCooking == null || OutputSlots.StoredItem != null)
		{
			return;
		}
		// Cooking has been permitted
		foreach (InventorySlot slot in InputSlots)
		{
			slot.StoredItem = null;
		}
		// Setup the timer
		OutputSlots.AllowInteracting = false;
		Cooking = true;
		CraftingTimer = (WhatsCooking.CookTime * 60) / gameTime.TimeSpeed;
		ProgressMeter.maxValue = (WhatsCooking.CookTime * 60) / gameTime.TimeSpeed;
		// Output the result
		OutputSlots.StoredItem = WhatsCooking.Result;
	}
}
