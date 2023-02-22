using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Vector3 _destination;

    private void Start()
    {
        GameEvents.current.onMoveTo += OnMoveTo;
    }

    private void Update()
    {
        _agent.SetDestination(_destination);
    }

    public void OnMoveTo(RaycastHit hitInfo)
    {
        _destination = hitInfo.point;
    }
}
