using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackState : State
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool canAttack;
    public IdleState idleState;
    public float distance;

    protected override void OnEnable()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        interact = GetComponentInParent<RayCast>();
        playerAnimationManager = GetComponentInParent<PlayerAnimationManager>();
    }

    private void Update()
    {
        if (interact.ClickedObject != null)
        {
            distance = (interact.ClickedObject.transform.position.magnitude - transform.position.magnitude);
        }
    }

    public override State RunCurrentState()
    {
        SetUpState();
        if (interact.ClickedObject.activeInHierarchy)
        {
            if (playerAnimationManager.canAttack)
            {
                playerAnimationManager.RunAttackAnimationTrigger();
                if (playerAnimationManager.reachedAttackEnd)
                {
                    interact.ClickedObject.GetComponent<IDamageable>().Damage(1);
                    playerAnimationManager.reachedAttackEnd = false;
                }
            }
        }
        else
        {
            interact.CurrentInteractType = InteractTypes.Default;
            return idleState;
        }
        return this;
    }

    public override void SetUpState()
    {
        navMeshAgent.SetDestination(interact.ClickedObject.transform.position);
        playerAnimationManager.SetMoveAnimationBool(true);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        canAttack = false;
    }
}
