using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class State : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public RayCast interact;
    public PlayerAnimationManager playerAnimationManager;

    protected virtual void OnEnable()
    {

    }
    public abstract State RunCurrentState();
    public abstract void SetUpState();
}
