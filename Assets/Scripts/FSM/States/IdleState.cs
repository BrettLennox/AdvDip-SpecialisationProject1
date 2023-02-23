using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public MoveToState moveToState;
    public InteractState interactState;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
    }

    public override State RunCurrentState()
    {
        if(interact.ClickedObject??false)
        {
            switch (interact.CurrentInteractType)
            {
                case InteractTypes.Location:
                    return moveToState;
                case InteractTypes.Interactable:
                    return interactState;
                case InteractTypes.Enemy:
                    break;
            }
        }
        return this;
    }
}
