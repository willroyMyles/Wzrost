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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent == transform.parent) return;
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (!listOfObjectInSphere.Contains(other.gameObject))
            {
                if(other.gameObject != gameObject) listOfObjectInSphere.Add(other.gameObject);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent == transform.parent) return;
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (listOfObjectInSphere.Contains(other.gameObject))
            {
                listOfObjectInSphere.Remove(other.gameObject);
                transform.parent.GetComponent<EnemyController>().startFight(listOfObjectInSphere);

            }
        }
    }


}
