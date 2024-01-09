using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static RecipeBook;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
	public GameItem Result;
	public GameItem[] Inputs;

	public CookingType cookingType;
	[Tooltip("How many segments of 15 minutes it takes to create, decimals supported")]
	public float CookTime;

	public bool CompareIngredients(GameItem[] items)
	{
		List<GameItem>ItemsAvailable = new();
		List<GameItem>ItemsRequired = new();
		ItemsAvailable.AddRange(items);
		ItemsRequired.AddRange(Inputs);
		foreach (GameItem item in items)
		{
			foreach (GameItem requiredItem in Inputs)
			{
				if (item.Equals(requiredItem))
				{
					if (ItemsAvailable.Contains(requiredItem) && ItemsRequired.Contains(requiredItem))
					{
						ItemsAvailable.Remove(requiredItem);
						ItemsRequired.Remove(requiredItem);
					}
				}
			}
		}
		return ItemsAvailable.Count == 0 && ItemsRequired.Count == 0;
	}
}
