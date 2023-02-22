using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Items/Consumable")]

public class ConsumableObject : ItemObject
{
    public int restoreHealthValue;

    private void Awake()
    {
        itemType = ItemTypes.Consumable;
    }
}
