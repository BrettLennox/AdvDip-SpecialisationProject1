using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public MoveToState moveToState;
    public CollectState collectState;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<Interact>();
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
                    break;
                case InteractTypes.Item:
                    return collectState;
                case InteractTypes.Enemy:
                    break;
            }
        }
        return this;
    }
}
