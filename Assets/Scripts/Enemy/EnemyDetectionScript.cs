using System.Collections;
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
        if (other.gameObject.transform.parent == transform.parent) return;
        if (listOfObjectInSphere.Contains(other.gameObject)) return;
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.transform.parent.GetComponentInChildren<PlayerBase>().isOnTeam == transform.parent.GetComponentInChildren<EnemyBase>().isOnTeam) return;

                addObjectsToSphere(other.gameObject);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null) Debug.Log("something dies");
        if (other.gameObject.transform.parent == transform.parent) return;
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.transform.parent.GetComponentInChildren<PlayerBase>().isOnTeam == transform.parent.GetComponentInChildren<EnemyBase>().isOnTeam) return;
            if (listOfObjectInSphere.Contains(other.gameObject))
            {
                removeObjectsFromSphere(other.gameObject);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);

            }
        }
    }


}
