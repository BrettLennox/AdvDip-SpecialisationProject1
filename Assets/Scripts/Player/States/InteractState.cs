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

    // used to initialise references as to not create errors
    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        player = GetComponentInParent<Player>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    /// <summary>
    /// The RunCurrentState function is used to run the state specific operations
    /// This state is used to move the player to the targeted interactable object and then perform the objects interact function once it has reached it
    /// </summary>
    /// <returns> this function then returns the specific state to remain in or move to </returns>
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

    /// <summary>
    /// Used to set up the specific fields for the NavMeshAgent and animation
    /// </summary>
    /// <param name="agent"> this arg is used to pass in the NavMeshAgent on the object and fill its SetDestination argument</param>
    /// <param name="destination"> this arg is used to pass in the destination on the object and fill its SetDestination argument</param>
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
