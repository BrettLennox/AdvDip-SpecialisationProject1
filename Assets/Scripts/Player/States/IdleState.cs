using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public MoveToState moveToState;
    public InteractState interactState;
    public AttackState attackState;
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
    /// This primary usage of this state is to be used as an inbetween state. Once a state ends it moves back to this state which will then move it into its next appropriate state
    /// </summary>
    /// <returns> return is used to decide which state to move into </returns>
    public override State RunCurrentState()
    {
        SetUpState(navMeshAgent, this.transform.position);

        if (interact.ClickedObject ?? false)
        {
            switch (interact.CurrentInteractType)
            {
                case InteractTypes.Location:
                    return moveToState;
                case InteractTypes.Interactable:
                    return interactState;
                case InteractTypes.Enemy:
                    return attackState;
            }
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
        playerAnimationManager.SetMoveAnimationBool(false);
    }
}
