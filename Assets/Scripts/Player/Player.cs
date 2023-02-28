using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int health = 20;

    public InventoryObject inventory;

    public void AddToInventory(ItemObject item)
    {
        inventory.AddItem(item, 1);
    }

    public void Damage(int amount)
    {
        
    }

    private void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
