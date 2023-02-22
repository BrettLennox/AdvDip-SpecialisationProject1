using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<ItemObject, int> onCollectItem;
    public void CollectItem(ItemObject item, int amount)
    {
        if (onCollectItem != null)
        {
            onCollectItem(item, amount);
        }
    }

    public event Action<RaycastHit> onMoveTo;
    public void MoveTo(RaycastHit hitInfo)
    {
        if(onMoveTo != null)
        {
            onMoveTo(hitInfo);
        }
    }
}
