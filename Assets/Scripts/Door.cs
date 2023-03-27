using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorStates
{
    Opened, Closed
}
public class Door : MonoBehaviour, IInteractable
{
    public GameObject model;
    public bool requiresKey;
    public DoorStates doorState;

    public void Interact(Player player)
    {
        ChangeDoorState(player);
    }

    // changes the door state to the opposite enum type
    private void ChangeDoorState(Player player)
    {
        if (requiresKey)
        {
            //check if player has key
            //remove key from player
        }
        else
        {
            switch (doorState)
            {
                case DoorStates.Opened:
                    doorState = DoorStates.Closed;
                    break;
                case DoorStates.Closed:
                    doorState = DoorStates.Opened;
                    break;
            }
        }
        Debug.Log(doorState);
    }

    private void Update()
    {
        ApplyDoorPositions();
    }

    //sets the doors position based on its current enum type
    private void ApplyDoorPositions()
    {
        switch (doorState)
        {
            case DoorStates.Opened:
                model.transform.localPosition = new Vector3(0, 3, 0);
                break;
            case DoorStates.Closed:
                model.transform.localPosition = new Vector3(0, 1, 0);
                break;
        }
    }
}
