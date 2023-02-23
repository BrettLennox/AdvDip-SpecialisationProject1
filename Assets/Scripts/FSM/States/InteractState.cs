using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractState : State
{
    public Player player;
    public IdleState idleState;
    public GameObject objToCollect;
    public bool hasAddedRef = false;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        player = GetComponentInParent<Player>();
    }

    public override State RunCurrentState()
    {
        navMeshAgent.destination = interact.ClickedObject.transform.position;
        var distance = new Vector2(interact.Destination.x, interact.Destination.z) - new Vector2(transform.position.x, transform.position.z);
        float distMagnitude = distance.magnitude;
        Debug.Log(distMagnitude);

        if (distMagnitude <= navMeshAgent.stoppingDistance)
        {
            IInteractable interactable = interact.ClickedObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(player);
                interact.ClickedObject = null;
                interact.CurrentInteractType = InteractTypes.Default;
                return idleState;
            }
        }

        #region Old
        //Vector3 dist = interact.Destination - transform.position;
        //float distMagnitude = dist.magnitude;
        //Debug.Log(distMagnitude <= 0.6f);
        //if (distMagnitude <= 0.6f)
        //{
        //    if (interact.ClickedObject.activeInHierarchy)
        //    {
        //        bool hasAdded = false;
        //        Debug.Log("ADDING ITEM");
        //        if (!hasAdded)
        //        {
        //            player.AddToInventory(interact.ClickedObject.GetComponent<Item>().item);
        //            Destroy(interact.ClickedObject);
        //            hasAdded = true;
        //        }
        //        interact.ClickedObject = null;
        //        interact.CurrentInteractType = InteractTypes.Default;
        //        return idleState;
        //    }
        //    else
        //    {
        //        Debug.Log("NO ITEM TO ADD");
        //        interact.ClickedObject = null;
        //        interact.CurrentInteractType = InteractTypes.Default;
        //        return idleState;
        //    }
        //}
        //if(interact.CurrentInteractType != InteractTypes.Item)
        //{
        //    return idleState;
        //}
        #endregion
        return this;
    }
}