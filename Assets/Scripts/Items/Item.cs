using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemObject item;

    public void Interact(Player player)
    {
        CollectItem(player);
    }

    private void CollectItem(Player player)
    {
        player.AddToInventory(item);
        Destroy(this.gameObject);
    }
}
