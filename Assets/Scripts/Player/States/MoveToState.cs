using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToState : State
{
    public IdleState idleState;
    public RayCast interact;
    public PlayerAnimationManager playerAnimationManager;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    public override State RunCurrentState()
    {
        if(interact.CurrentInteractType != InteractTypes.Location)
        {
            return idleState;
        }

        SetUpState(navMeshAgent, interact.Destination);
        var distance = new Vector2(interact.Destination.x, interact.Destination.z) - new Vector2(transform.position.x, transform.position.z);
        float distMagnitude = distance.magnitude;
        //Debug.Log(distMagnitude);
        if(distMagnitude <= navMeshAgent.stoppingDistance)
        {
            interact.ClickedObject = null;
            interact.CurrentInteractType = InteractTypes.Default;
            playerAnimationManager.SetMoveAnimationBool(false);
            return idleState;
        }
        return this;
    }

    public override void SetUpState(NavMeshAgent agent, Vector3 destination)
    {
        agent.SetDestination(destination);
        playerAnimationManager.SetMoveAnimationBool(true);
    }
}
