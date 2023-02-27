using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class State : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    protected virtual void OnEnable()
    {

    }
    public abstract State RunCurrentState();
    public abstract void SetUpState(NavMeshAgent agent, Vector3 destination);
}
