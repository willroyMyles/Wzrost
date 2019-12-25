﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScript : MonoBehaviour
{
    List<GameObject> listOfObjectInSphere = new List<GameObject>();
    float lookRadius = 9;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = lookRadius;
    }

    public void checkFightList()
    {
        foreach(var child in listOfObjectInSphere)
        {
            if (child == null)
            {
                listOfObjectInSphere.Remove(child);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);
            }
        }
    }

    private void addObjectsToSphere(GameObject obj)
    {
        listOfObjectInSphere.Add(obj);
    }

    private void removeObjectsFromSphere(GameObject obj)
    {
        listOfObjectInSphere.Remove(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Interactor") return;
        if( other.gameObject.TryGetComponent<PlayerBase>(out var comp))
        {
            if(comp.teamNumber != transform.parent.GetComponent<PlayerBase>().teamNumber)
            {
                addObjectsToSphere(other.gameObject);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Interactor") return;
        if (other.gameObject.TryGetComponent<PlayerBase>(out var comp))
        {
            if (comp.teamNumber != transform.parent.GetComponent<PlayerBase>().teamNumber && listOfObjectInSphere.Contains(other.gameObject))
            {
                removeObjectsFromSphere(other.gameObject);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);
            }
        }
    }


}
