using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : State
{
    public override State RunCurrentState()
    {
        //code to be performed when in this state
        //including conditions to exit the state
        return this;
    }

    public override void SetUpState()
    {
        //code to set the state up upon entry
    }
}
