using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
	public enum CookingType
	{
		Cut, // Cutting Board
		Boil, // Cauldron
		Fry, // Frying Pan
		Bake, // Oven
		Grind, // Mortar and Pestle
		Mix // Mixing Bowl
	}
	public List<Recipe> AllowedRecipes = new();

	public List<Recipe> RequestValidRecipes(CookingType RecipeType)
	{
		List<Recipe> output = new();

		foreach (Recipe recipe in AllowedRecipes)
		{
			if (recipe == null)
			{
				Debug.LogError("There's a null object in the RecipeBook!!! This is extremely likely to break something!!!");
				continue;
			}
			if (recipe.cookingType == RecipeType)
			{
				output.Add(recipe);
			}
		}
		return output;
	}
	public Recipe GetWhatsCooking(CookingType RecipeType, GameItem[] items)
	{
		foreach (Recipe recipe in AllowedRecipes)
		{
			if (recipe == null)
			{
				Debug.LogError("There's a null object in the RecipeBook!!! This is extremely likely to break something!!!");
				continue;
			}
			if (recipe.cookingType == RecipeType && recipe.CompareIngredients(items))
			{
				return recipe;
			}
		}
		return null;
	}
	public Recipe GetWhatsCooking(CookingType RecipeType, InventorySlot[] inputSlots)
	{
		List<GameItem> InputtedItems = new();
		foreach (InventorySlot slot in inputSlots)
		{
			if (slot.StoredItem != null)
			{
				InputtedItems.Add(slot.StoredItem);
			}
		}
		return GetWhatsCooking(RecipeType, InputtedItems.ToArray());
	}
}