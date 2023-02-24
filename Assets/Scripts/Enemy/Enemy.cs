using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int currentHealth;
    public int maxHealth;

    public bool takeDamage = false;

    public void Damage(int amount)
    {
        TakeDamage(amount);
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
