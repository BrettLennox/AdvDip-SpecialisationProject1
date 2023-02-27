using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : State
{
    public EnemyWanderState enemyWanderState;

    protected override void OnEnable()
    {
        base.OnEnable();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    public override State RunCurrentState()
    {
        //code to be performed when in this state
        //including conditions to exit the state
        return enemyWanderState;
    }

    public override void SetUpState(UnityEngine.AI.NavMeshAgent agent, Vector3 destination)
    {
        //code to set the state up upon entry
    }
}
