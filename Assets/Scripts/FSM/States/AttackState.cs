using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class AttackState : State
{

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<Interact>();
    }

    public override State RunCurrentState()
    {
        return this;
    }
}
