using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    private void Start()
    {
        GameEvents.current.onCollectItem += OnCollectItem;
    }

    private void OnCollectItem(ItemObject item, int amount)
    {
        inventory.AddItem(item, amount);
    }
}
