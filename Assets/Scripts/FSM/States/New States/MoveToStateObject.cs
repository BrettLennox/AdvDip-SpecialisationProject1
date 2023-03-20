using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewMoveToStateObject", menuName = "FSM/MoveTo")]
public class MoveToStateObject : StateBase
{
    public NavMeshAgent navMeshAgent;
    public StateBase idleState;

    public override StateBase RunStateUpdate()
    {
        Debug.Log("MOVE TO STATE");
        if(navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance)
        {
            return this;
        }

        return idleState;
    }
}
