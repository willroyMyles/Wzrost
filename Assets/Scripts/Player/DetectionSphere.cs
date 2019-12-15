using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{

    
    bool drawGizmos = true;
    CameraBase cf;

    public bool DrawGizmos { get => drawGizmos; set => drawGizmos = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (Camera.allCameras[0].orthographic) cf = FindObjectOfType<CameraFollow>();
        else cf = FindObjectOfType<CameraFollowPerspective>();
        cf.AddToList(gameObject);

        var collider = GetComponent<SphereCollider>();
        collider.radius = Global.Instance().playerDectecionSphereLookRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
      
            //position camera to view both players
           if(other.gameObject.tag == "Enemy") cf.AddToList(other.gameObject);
        
    }

    private void OnTriggerExit(Collider other)
    {

            //position camera to view both players
            if (other.gameObject.tag == "Enemy") cf.removeFromList(other.gameObject);
        
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
