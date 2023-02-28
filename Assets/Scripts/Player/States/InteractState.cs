using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractState : State
{
    public Player player;
    public IdleState idleState;
    public GameObject objToCollect;
    public bool hasAddedRef = false;
    public bool hasSetupState;
    public RayCast interact;
    public PlayerAnimationManager playerAnimationManager;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        player = GetComponentInParent<Player>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    public override State RunCurrentState()
    {
        SetUpState(navMeshAgent, interact.ClickedObject.transform.position);
        if (interact.CurrentInteractType != InteractTypes.Interactable)
        {
            navMeshAgent.isStopped = false;
            hasSetupState = false;
            playerAnimationManager.SetMoveAnimationBool(false);
            return idleState;
        }

        if (objToCollect != interact.ClickedObject)
        {
            navMeshAgent.isStopped = false;
            hasSetupState = false;
            return idleState;
        }

        var distance = interact.ClickedObject.transform.position - transform.position;
        var distMagnitude = distance.magnitude;
        Debug.Log(distMagnitude);
        if (distMagnitude >= navMeshAgent.stoppingDistance) { return this; }

        navMeshAgent.isStopped = true;
        IInteractable interactable = interact.ClickedObject.GetComponent<IInteractable>();
        if (interactable == null)
        {
            Debug.Log("FAILED");
            ResetState();
            return idleState;
        }

        Debug.Log("INTERACTING");
        interactable.Interact(player);
        ResetState();
        return idleState;
    }

    private void ResetState()
    {
        navMeshAgent.isStopped = false;
        hasSetupState = false;
        interact.ClickedObject = null;
        interact.CurrentInteractType = InteractTypes.Default;
        playerAnimationManager.SetMoveAnimationBool(false);
    }

    public override void SetUpState(NavMeshAgent agent, Vector3 destination)
    {
        if (!hasSetupState)
        {
            objToCollect = interact.ClickedObject;
            agent.SetDestination(destination);
            playerAnimationManager.SetMoveAnimationBool(true);
            hasSetupState = true;
        }
    }
}
