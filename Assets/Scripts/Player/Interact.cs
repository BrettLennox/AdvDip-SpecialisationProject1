using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private RaycastHit hitInfo;
    [SerializeField] private LayerMask _walkableArea;
    [SerializeField] private bool _atDestination;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitInfo = CheckRayCast();
        }

        MoveToWorldPoint(hitInfo);
        CollectItem();
    }

    public void CollectItem()
    {
        if(_gameObject != null)
        {
            var distance = Vector3.Distance(transform.position, hitInfo.transform.position);
            if (distance < 0.4 && _gameObject.GetComponent<Item>())
            {
                GameEvents.current.CollectItem(_gameObject.GetComponent<Item>().item, 1);
                Destroy(_gameObject);
            }
        }
    }

    public void MoveToWorldPoint(RaycastHit hitInfo)
    {
        GameEvents.current.MoveTo(hitInfo);
    }

    public RaycastHit CheckRayCast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            _gameObject = hit.transform.gameObject;
            return hit;
        }
        return default;
    }
}