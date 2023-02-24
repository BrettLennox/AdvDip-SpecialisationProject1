using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool canAttack;
    public bool reachedAttackEnd;
    public void RunIdleAnimation()
    {

    }

    public void SetMoveAnimationBool(bool state)
    {
        _animator.SetBool("IsMoving", state);
    }

    public void RunAttackAnimationTrigger()
    {
        _animator.SetTrigger("Attack");
        canAttack = false;
    }

    public void FinishedAttack()
    {
        reachedAttackEnd = true;
    }
}
