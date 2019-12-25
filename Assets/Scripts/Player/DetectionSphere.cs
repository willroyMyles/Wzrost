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
        if (Camera.allCameras[0].orthographic) cf = FindObjectOfType<CameraFollow>();
        else cf = FindObjectOfType<CameraFollowPerspective>();
        Global.Instance().opponentsWithinSphere.Add(gameObject);

        var collider = GetComponent<SphereCollider>();
        collider.radius = Global.Instance().playerDectecionSphereLookRadius;
    }

    private void OnTriggerEnter(Collider other)
    {

        //position camera to view both players
        if (other.gameObject.tag == "Enemy" && !other.gameObject.transform.parent.GetComponent<PlayerBase>().isOnTeam) detectionList.Add(other.gameObject);
        if (detectionList.Count > 1) playerEithinDistanceToAim = true;

    }

    private void OnTriggerExit(Collider other)
    {

            //position camera to view both players
            if (other.gameObject.tag == "Enemy" && !other.gameObject.transform.parent.GetComponent<PlayerBase>().isOnTeam) detectionList.Remove(other.gameObject);
        if (detectionList.Count > 1) playerEithinDistanceToAim = false;

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
