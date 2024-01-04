using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockStore : MonoBehaviour
{
    [SerializeField] StoreUIScript ShopUI;
    public List<GameItem> ItemsToStock = new();
    
    public void Stock()
    {
        ShopUI.StockShop(ItemsToStock.ToArray());
    }
}
