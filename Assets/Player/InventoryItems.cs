using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public enum ItemType
    {
        //Don't Order
        KeyItem,
        Ingredient,
        //Meal
        Sandwich,
        Soup,
        Appetizer,
        //Desert
        Pie,
        Cupcake,
        Pancake,
        //Drinks
        Juice,
        Coffee,
        Alcohol
    }
    public class Item
    {
        // Basics
        public Sprite image;
        public string name;
        public ItemType type;
        public int id;
        public Item(int id, string name, ItemType type, Sprite sprite)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.image = sprite;
        }
    }
    public Item CreateItem(int id)
    {
        return new Item(id, ItemDatabase[id].name, ItemDatabase[id].type, ItemDatabase[id].image);
    }
    public Dictionary<int, Item> ItemDatabase = new Dictionary<int, Item>();
}
