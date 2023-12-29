using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RecipeBook;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public GameItem Result;
    public GameItem[] Inputs;

    public CookingType cookingType;
    public float CookTime;
}
