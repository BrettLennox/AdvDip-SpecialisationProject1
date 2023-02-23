using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToState : State
{
    public IdleState idleState;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<Interact>();
    }

    public override State RunCurrentState()
    {
        navMeshAgent.SetDestination(interact.Destination);
        var distance = new Vector2(interact.Destination.x, interact.Destination.z) - new Vector2(transform.position.x, transform.position.z);
        float distMagnitude = distance.magnitude;
        Debug.Log(distMagnitude);
        if(distMagnitude <= navMeshAgent.stoppingDistance)
        {
            interact.ClickedObject = null;
            interact.CurrentInteractType = InteractTypes.Default;
            return idleState;
        }
        return this;
    }
}
