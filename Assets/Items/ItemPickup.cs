using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool Reusable = false;
    public int Uses = 1;
    public GameItem item;

    public void Pickup()
    {
        Uses -= 1;
        //TODO: Add item to player
        if (Reusable)
        {
            Uses += 1;
        }
        else
        {
            if (Uses < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
