using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class AttackState : State
{

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
    }

    public override State RunCurrentState()
    {
        return this;
    }
}
