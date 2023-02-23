using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum InteractTypes
{
    Location,
    Interactable,
    Item,
    Enemy,
    Default
}
public class RayCast : MonoBehaviour
{
    [SerializeField] private InteractTypes _currentInteractType;
    [SerializeField] private GameObject _clickedObject;
    [SerializeField] private Vector3 _destination;
    Ray ray;

    public Vector3 Destination { get => _destination; }
    public GameObject ClickedObject { get => _clickedObject; set => _clickedObject = value; }
    public InteractTypes CurrentInteractType { get => _currentInteractType; set => _currentInteractType = value; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickedObject = CastRayFromMouse();
        }
    }

    public GameObject CastRayFromMouse()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 3f);
            _destination = hit.point;
            //if (hit.transform.CompareTag("Item"))
            //{
            //    _currentInteractType = InteractTypes.Item;
            //}
            if (hit.transform.CompareTag("Enemy"))
            {
                _currentInteractType = InteractTypes.Enemy;
            }
            else if (hit.transform.GetComponent<IInteractable>() != null)
            {
                _currentInteractType = InteractTypes.Interactable;
            }
            else
            {
                _currentInteractType = InteractTypes.Location;
            }
            return hit.transform.gameObject;
        }
        return null;
    }
}