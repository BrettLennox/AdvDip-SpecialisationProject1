using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackState : State
{
    public IdleState idleState;
    public RayCast interact;
    public PlayerAnimationManager playerAnimationManager;

    public GameObject targetReference;
    public bool hasSetupState;

    // used to initialise references as to not create errors
    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    private void Update()
    {

    }

    /// <summary>
    /// The RunCurrentState function is used to run the state specific operations
    /// This state is used to move the AI to the set targets destination and once it has reached the destination it performs the attack function
    /// Once it has performed these functions it checks agains the target object being valid in the scene and returns the correct state afterwards
    /// </summary>
    /// <returns> return is used to decide which state to move into </returns>
    public override State RunCurrentState()
    {
        if (interact.ClickedObject != null)
        {
            SetUpState(navMeshAgent, interact.ClickedObject.transform.position);
            if (targetReference == interact.ClickedObject)
            {
                var distance = interact.ClickedObject.transform.position - transform.position;
                var distMagnitude = distance.magnitude;
                //Debug.Log(distMagnitude);
                if (distMagnitude <= navMeshAgent.stoppingDistance)
                {
                    navMeshAgent.isStopped = true;
                    if (interact.ClickedObject.GetComponent<IDamageable>() != null)
                    {
                        if (playerAnimationManager.canAttack)
                        {
                            playerAnimationManager.RunAttackAnimationTrigger();

                        }
                        else if (playerAnimationManager.reachedAttackEnd && interact.ClickedObject.activeInHierarchy)
                        {
                            interact.ClickedObject.GetComponent<IDamageable>().Damage(1);
                            playerAnimationManager.reachedAttackEnd = false;
                            playerAnimationManager.canAttack = true;
                        }
                    }
                }
                else
                {
                    navMeshAgent.isStopped = false;
                }
            }
            else
            {
                navMeshAgent.isStopped = false;
                hasSetupState = false;
                return idleState;
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
            hasSetupState = false;
            interact.ClickedObject = null;
            interact.CurrentInteractType = InteractTypes.Default;
            return idleState;
        }


        if (interact.CurrentInteractType != InteractTypes.Enemy)
        {
            navMeshAgent.isStopped = false;
            hasSetupState = false;
            return idleState;
        }
        return this;
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
            targetReference = interact.ClickedObject;
            agent.SetDestination(destination);
            playerAnimationManager.SetMoveAnimationBool(true);
            hasSetupState = true;
        }
    }
}
