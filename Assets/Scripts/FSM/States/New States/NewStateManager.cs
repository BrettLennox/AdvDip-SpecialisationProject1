using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStateManager : MonoBehaviour
{
    public StateBase currentState = null;

    private void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        StateBase nextState = currentState?.RunStateUpdate();

        if(nextState != null)
        {
            SwitchState(nextState);
        }
        else
        {
            Debug.Log("STATE NOT SET");
        }
    }

    private void SwitchState(StateBase nextState)
    {
        currentState = nextState;
    }
}
