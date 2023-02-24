using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackState : State
{
    public IdleState idleState;

    public GameObject targetReference;
    public bool hasSetupState;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    private void Update()
    {

    }

    public override State RunCurrentState()
    {
        if (interact.ClickedObject != null)
        {
            SetUpState();
            if (targetReference == interact.ClickedObject)
            {
                var distance = interact.ClickedObject.transform.position - transform.position;
                var distMagnitude = distance.magnitude;
                Debug.Log(distMagnitude);
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

    public override void SetUpState()
    {
        if (!hasSetupState)
        {
            targetReference = interact.ClickedObject;
            navMeshAgent.SetDestination(interact.ClickedObject.transform.position);
            playerAnimationManager.SetMoveAnimationBool(true);
            hasSetupState = true;
        }
    }
}
