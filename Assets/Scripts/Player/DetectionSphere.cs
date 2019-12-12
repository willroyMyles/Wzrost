using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{

    float lookRadius = 15f;
    bool drawGizmos = true;
    CameraBase cf;

    public bool DrawGizmos { get => drawGizmos; set => drawGizmos = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (Camera.allCameras[0].orthographic) cf = FindObjectOfType<CameraFollow>();
        else cf = FindObjectOfType<CameraFollowPerspective>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Player")
        {
            //position camera to view both players
           if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") cf.AddToList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            //position camera to view both players
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") cf.removeFromList(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(.1f, .25f, 1, .2f);
            Gizmos.DrawSphere(transform.position, lookRadius);
        }
    }
}
