using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    private void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if(nextState != null)
        {
            SwitchToNextState(nextState);
        }
        else
        {
            Debug.Log("STATE NOT LINKED");
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
