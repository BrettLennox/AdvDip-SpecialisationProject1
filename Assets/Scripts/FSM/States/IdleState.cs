using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public MoveToState moveToState;
    public InteractState interactState;
    public AttackState attackState;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    public override State RunCurrentState()
    {
        SetUpState();

        if(interact.ClickedObject??false)
        {
            switch (interact.CurrentInteractType)
            {
                case InteractTypes.Location:
                    return moveToState;
                case InteractTypes.Interactable:
                    return interactState;
                case InteractTypes.Enemy:
                    return attackState;
                    break;
            }
        }
        return this;
    }

    public override void SetUpState()
    {
        playerAnimationManager.SetMoveAnimationBool(false);
    }
}
