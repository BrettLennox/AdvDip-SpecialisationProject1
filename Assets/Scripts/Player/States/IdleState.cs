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

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

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

    public override void SetUpState(NavMeshAgent agent, Vector3 destination)
    {
        agent.SetDestination(destination);
        playerAnimationManager.SetMoveAnimationBool(false);
    }
}
