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
	[SerializeField] private Button StartCraftingButton;
	private InventoryManager InventorySlots;
	private InventorySlot OutputSlots;
	private Image OutputSlotPreview = null;
	private InventorySlot[] InputSlots;
	private Slider ProgressMeter;
	private bool Cooking = false;
	private float CraftingTimer = 0;
	private void Start()
	{
		book = GameObject.FindGameObjectWithTag("GameController").GetComponent<RecipeBook>();
		gameTime = book.gameObject.GetComponent<TimeScript>();
		InventorySlots = transform.GetComponentInChildren<InventoryManager>();

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
				slot.SlotModified.AddListener(PreviewCooking);
			}
		}
		InputSlots = InpSlots.ToArray();
		OutputSlots = OutSlots[0];
		foreach (Transform child in OutputSlots.transform)
		{
			if (child.GetComponent<Image>() != null)
			{
				OutputSlotPreview = child.GetChild(0).GetComponent<Image>();
				break;
			}
		}
		ProgressMeter = OutputSlots.GetComponentInChildren<Slider>();
		StartCraftingButton.onClick.AddListener(LetEmCook);
	}
	private void Update()
	{
		if (CraftingTimer > 0)
		{
			CraftingTimer -= Time.deltaTime * gameTime.TimeSpeed;
			ProgressMeter.value = ProgressMeter.maxValue - CraftingTimer;
			if (CraftingTimer <= 0)
			{
				OutputSlots.AllowInteracting = true;
				Cooking = false;
				ProgressMeter.value = 0;
				OutputSlots.ItemImage.color = Color.white;
			}
		}
	}
	private bool CanModifyOutputWithThis(out Recipe WhatsCooking)
	{
		WhatsCooking = null;
		if (OutputSlots.StoredItem != null || Cooking)
		{
			return false;
		}
		WhatsCooking = book.GetWhatsCooking(cookingType, InputSlots);
		if (WhatsCooking == null)
		{
			return false;
		}
		return true;
	}
	private void PreviewCooking()
	{
		if (!CanModifyOutputWithThis(out Recipe WhatsCooking))
		{
			OutputSlotPreview.enabled = false;
			return;
		}
		OutputSlotPreview.enabled = true;
		OutputSlotPreview.sprite = WhatsCooking.Result.Icon; 
	}
	private void LetEmCook()
	{
		// Check if we can do this
		if (!CanModifyOutputWithThis(out Recipe WhatsCooking))
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
		CraftingTimer = (WhatsCooking.CookTime * 15 * 60);
		ProgressMeter.maxValue = (WhatsCooking.CookTime * 15 * 60);
		// Output the result
		OutputSlotPreview.enabled = false;
		OutputSlots.StoredItem = WhatsCooking.Result;
		OutputSlots.ItemImage.color = new Color(1, 1, 1, 0.5f);
	}
}
