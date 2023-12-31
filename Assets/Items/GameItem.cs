using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameItem : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    [Header("Merchant")]
    public int BaseValue = 0;
    public bool Sellable = true;
    [Header("Tags")]
    public bool KeyItem = false;
    public bool Orderable = false;
    public bool IsBeverage = false;
    public bool IsAlcohol = false;
    [Header("Contents")]
    public bool HasMeat = false;
    public bool HasMilk = false;

    public GameItem Clone()
    {
        return (GameItem)this.MemberwiseClone();
    }
}
