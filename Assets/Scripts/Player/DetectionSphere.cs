using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{


    bool drawGizmos = true;
    CameraBase cf;
    [SerializeField] internal List<GameObject> detectionList = new List<GameObject>();
    internal bool playerEithinDistanceToAim = false;

    public bool DrawGizmos { get => drawGizmos; set => drawGizmos = value; }

    // Start is called before the first frame update
    void Start()
    {

        var collider = GetComponent<SphereCollider>();
        collider.radius = Global.Instance().playerDectecionSphereLookRadius;  // should get from playerBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if object is player or enemy
        if (other.tag == "Player" || other.tag == "Enemy")
        {


        //check if they on our team
            var theirNum = other.transform.parent.GetComponent<PlayerBase>().teamNumber;
            var myNum = GetComponent<PlayerBase>().teamNumber;
        if (theirNum == myNum) return;
        // check if its alread in list
        if (detectionList.Contains(other.transform.parent.gameObject)) return;

        //add to list
            detectionList.Add(other.transform.parent.gameObject);
            if (detectionList.Count > 1) playerEithinDistanceToAim = true;
            GetComponent<GeneralMovement>().startFight(detectionList);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //check if object is player or enemy
        if (other.tag == "Player" || other.tag == "Enemy")
        {
        //check if they on our team
        if (other.transform.parent.GetComponent<PlayerBase>().teamNumber == GetComponent<PlayerBase>().teamNumber) return;
        //check if its already in list
        if (!detectionList.Contains(other.transform.parent.gameObject)) return;
        
        detectionList.Remove(other.transform.parent.gameObject);
        if (detectionList.Count < 1) playerEithinDistanceToAim = false;

        }
        

    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(.1f, .25f, 1, .2f);
            Gizmos.DrawSphere(transform.position, Global.Instance().playerDectecionSphereLookRadius);
        }
    }
}
