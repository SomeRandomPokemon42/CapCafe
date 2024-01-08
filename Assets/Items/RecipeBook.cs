using System.Collections;
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
    public List<Recipe> AllowedRecipes = new List<Recipe>();

    public List<Recipe> RequestValidRecipes(CookingType RecipeType)
    {
        List<Recipe> output = new List<Recipe>();

        foreach (Recipe recipe in AllowedRecipes)
            if (recipe.cookingType == RecipeType)
            {
                output.Add(recipe);
            }

        return output;
    }
    public Recipe GetWhatsCooking(CookingType RecipeType, GameItem[] items)
    {
        foreach (Recipe recipe in AllowedRecipes)
        {
            
        }
    }
    public Recipe GetWhatsCooking(CookingType RecipeType, InventorySlot[] inputSlots)
    {
        List<GameItem> InputtedItems = new List<GameItem>();
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