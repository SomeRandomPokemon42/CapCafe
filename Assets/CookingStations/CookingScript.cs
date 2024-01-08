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
	private UIBoxHandler MyUI;
	private Button StartCraftingButton;
	private InventoryManager InventorySlots;
	private InventorySlot[] OutputSlots;
	private InventorySlot[] InputSlots;
	private void Start()
	{
		book = GameObject.FindGameObjectWithTag("GameController").GetComponent<RecipeBook>();
		gameTime = book.gameObject.GetComponent<TimeScript>();
		MyUI = GetComponentInParent<UIBoxHandler>();
		StartCraftingButton = GetComponent<Button>();
		InventorySlots = transform.parent.GetComponentInChildren<InventoryManager>();
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
		OutputSlots = OutSlots.ToArray();
	}
}
