using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int health = 20;

    public InventoryObject inventory;

    // this function takes an arg for ItemObject which is then added to the inventory attached to this class
    public void AddToInventory(ItemObject item)
    {
        inventory.AddItem(item, 1);
    }

    /// <summary>
    /// TakeDamage comes from the IDamagable interface
    /// Players health is then reduced by the amount passed in
    /// Paramaters for when the players health reaches or is below 0
    /// </summary>
    /// <param name="amount"> the amount passed in is the amount of damage to the player </param>
    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
