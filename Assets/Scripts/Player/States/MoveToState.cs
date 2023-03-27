using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToState : State
{
    public IdleState idleState;
    public RayCast interact;
    public PlayerAnimationManager playerAnimationManager;

    // used to initialise references as to not create errors
    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    /// <summary>
    /// The RunCurrentState function is used to run the state specific operations
    /// This state is used to move the AI to the target destination. This function is mainly used for when the user clicks on an environment piece
    /// </summary>
    /// <returns> return is used to decide which state to move into </returns>
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

    /// <summary>
    /// Used to set up the specific fields for the NavMeshAgent and animation
    /// </summary>
    /// <param name="agent"> this arg is used to pass in the NavMeshAgent on the object and fill its SetDestination argument</param>
    /// <param name="destination"> this arg is used to pass in the destination on the object and fill its SetDestination argument</param>
    public override void SetUpState(NavMeshAgent agent, Vector3 destination)
    {
        agent.SetDestination(destination);
        playerAnimationManager.SetMoveAnimationBool(true);
    }
}
