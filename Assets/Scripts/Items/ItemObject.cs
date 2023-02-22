using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Equipment,
    Consumable,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public ItemTypes itemType;
    public GameObject gamePrefab;
    public Sprite uISprite;
}
