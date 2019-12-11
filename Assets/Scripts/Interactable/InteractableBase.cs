using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{

    #region variables

    bool interactable = true;
    float maxDistance = 1.5f;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Interactable";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            //show controls. auto pickup for now;
    
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            var distance = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);
            Debug.Log(distance);
            if(distance < maxDistance)
            {
                other.gameObject.GetComponent<PlayerBase>().PickUp(gameObject);
            }

        }
    }
}
