using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public int attackBonus, defenceBonus;

    private void Awake()
    {
        itemType = ItemTypes.Equipment;
    }
}
