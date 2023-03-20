using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase : ScriptableObject
{
    public abstract StateBase RunStateUpdate();
}
