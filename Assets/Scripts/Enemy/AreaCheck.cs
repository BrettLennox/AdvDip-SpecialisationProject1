using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaCheck : MonoBehaviour
{
    public bool canSeePlayer;
    public List<GameObject> sortedList;

    // Update is called once per frame
    void Update()
    {
        CastSphereFromPosition();
    }

    public void CastSphereFromPosition()
    {
        RaycastHit hit;

        List<Collider> sphereList = Physics.OverlapSphere(transform.position, 10f).ToList();

        for(int i = 0; i < sphereList.Count; i++)
        {
            if(sphereList[i].gameObject.GetComponent<IDamageable>() != null)
            {
                if(sortedList.Contains(sphereList[i].gameObject)) { return; }
                sortedList.Add(sphereList[i].gameObject);
                Debug.Log($"ADDING {sphereList[i].name.ToUpper()} TO NEW LIST");
            }
        }
    }
}
