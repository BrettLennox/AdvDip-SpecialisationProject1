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

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        player = GetComponentInParent<Player>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    public override State RunCurrentState()
    {
        SetUpState();
        if(objToCollect == interact.ClickedObject)
        {
            var distance = interact.ClickedObject.transform.position - transform.position;
            var distMagnitude = distance.magnitude;
            Debug.Log(distMagnitude);

            if (distMagnitude <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.isStopped = true;
                IInteractable interactable = interact.ClickedObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Debug.Log("INTERACTING");
                    interactable.Interact(player);
                    ResetState();
                    return idleState;
                }
                else
                {
                    Debug.Log("FAILED");
                    ResetState();
                    return idleState;
                }
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
            hasSetupState = false;
            return idleState;
        }
        

        if (interact.CurrentInteractType != InteractTypes.Interactable)
        {
            ResetState();
            return idleState;
        }
        return this;
    }

    private void ResetState()
    {
        navMeshAgent.isStopped = false;
        hasSetupState = false;
        interact.ClickedObject = null;
        interact.CurrentInteractType = InteractTypes.Default;
        playerAnimationManager.SetMoveAnimationBool(false);
    }

    public override void SetUpState()
    {
        if (!hasSetupState)
        {
            objToCollect = interact.ClickedObject;
            navMeshAgent.SetDestination(interact.ClickedObject.transform.position);
            playerAnimationManager.SetMoveAnimationBool(true);
            hasSetupState = true;
        }
    }
}
