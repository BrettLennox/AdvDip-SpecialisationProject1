using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectState : State
{
    public Player player;
    public IdleState idleState;
    public GameObject objToCollect;
    public bool hasAddedRef = false;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<Interact>();
        player = GetComponentInParent<Player>();
    }

    public override State RunCurrentState()
    {
        navMeshAgent.destination = interact.ClickedObject.transform.position;
        Vector3 dist = interact.Destination - transform.position;
        float distMagnitude = dist.magnitude;
        Debug.Log(distMagnitude <= 0.6f);
        if (distMagnitude <= 0.6f)
        {
            if (interact.ClickedObject.activeInHierarchy)
            {
                bool hasAdded = false;
                Debug.Log("ADDING ITEM");
                if (!hasAdded)
                {
                    player.AddToInventory(interact.ClickedObject.GetComponent<Item>().item);
                    Destroy(interact.ClickedObject);
                    hasAdded = true;
                }
                interact.ClickedObject = null;
                interact.CurrentInteractType = InteractTypes.Default;
                return idleState;
            }
            else
            {
                Debug.Log("NO ITEM TO ADD");
                interact.ClickedObject = null;
                interact.CurrentInteractType = InteractTypes.Default;
                return idleState;
            }
        }
        if(interact.CurrentInteractType != InteractTypes.Item)
        {
            return idleState;
        }
        return this;
    }
}
