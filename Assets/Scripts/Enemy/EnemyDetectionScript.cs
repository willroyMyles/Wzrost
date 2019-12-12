using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScript : MonoBehaviour
{
    List<GameObject> listOfObjectInSphere = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (!listOfObjectInSphere.Contains(other.gameObject))
            {
                if(other.gameObject != gameObject) listOfObjectInSphere.Add(other.gameObject);
                GetComponent<EnemyController>().startFight(listOfObjectInSphere);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (listOfObjectInSphere.Contains(other.gameObject))
            {
                listOfObjectInSphere.Remove(other.gameObject);
                GetComponent<EnemyController>().startFight(listOfObjectInSphere);

            }
        }
    }


}
