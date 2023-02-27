using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : State
{
    protected override void OnEnable()
    {
        base.OnEnable();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    public override State RunCurrentState()
    {
        var distanceRemaining = navMeshAgent.remainingDistance;
        Debug.Log(distanceRemaining);
        //Debug.Log(navMeshAgent.destination);
        //code to be performed when in this state
        if(distanceRemaining <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.SetDestination(RandomCoordinates());
        }

        //including conditions to exit the state
        return this;
    }

    private Vector3 RandomCoordinates()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomZ = Random.Range(-5f, 5f);

        Vector3 randomMoveCoordinates = new Vector3(randomX, 0, randomZ);
        return randomMoveCoordinates;
    }

    public override void SetUpState(NavMeshAgent agent, Vector3 destination)
    {
        //code to set the state up upon entry
    }
}
