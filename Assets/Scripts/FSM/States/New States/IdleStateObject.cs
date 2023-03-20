using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleStateObject", menuName = "FSM/Idle")]
public class IdleStateObject : StateBase
{
    public override StateBase RunStateUpdate()
    {
        Debug.Log("IDLE STATE");
        return this;
    }
}
