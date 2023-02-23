using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    public void AddToInventory(ItemObject item)
    {
        inventory.AddItem(item, 1);
    }
}
