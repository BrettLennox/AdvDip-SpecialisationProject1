using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : State
{
    public float waitTime;
    public float timer;

    protected override void OnEnable()
    {
        base.OnEnable();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        waitTime = SetRandomWaitTime();
    }

    private float SetRandomWaitTime()
    {
        return Random.Range(1f, 3f);
    }

    public override State RunCurrentState()
    {
        //code to be performed when in this state
        var distanceRemaining = navMeshAgent.remainingDistance;
        if (distanceRemaining >= navMeshAgent.stoppingDistance) { return this; }

        TimerFunction();

        //including conditions to exit the state
        return this;
    }

    private void TimerFunction()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            waitTime = SetRandomWaitTime();
            navMeshAgent.SetDestination(RandomCoordinates());
            timer = 0;
        }
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
