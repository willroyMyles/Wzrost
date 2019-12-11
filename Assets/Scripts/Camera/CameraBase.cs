using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    // Start is called before the first frame update
    #region variables


    public Transform player;
    protected float smoothSpeed = 4f;  //hight is faster snapping
    protected Vector3 offset;


    protected float dampTime = .2f, screenEdgeBuffer = 4f, minSize = 10f;
    protected float zoomSpeed;
    protected Vector3 moveVelocity;
    protected Vector3 desiredPosition;
    protected List<GameObject> targets = new List<GameObject>();
    protected Camera cam;


    bool objectInSphere = false;

    public bool ObjectInSphere { get => objectInSphere; set => objectInSphere = value; }

    #endregion
    #region internal functions

    protected void Start()
    {
        cam = Camera.allCameras[0];
        offset = transform.position - player.position;
    }


    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    protected void Zoom()
    {
        float requieredSize = FindRequiredSize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requieredSize, ref zoomSpeed, dampTime);
    }

    private float FindRequiredSize()
    {
        var desieredLocalPosition = transform.InverseTransformPoint(desiredPosition);
        float size = 0;
        for (int i = 0; i < targets.Count; i++)
        {
            var targetLocalPosition = transform.InverseTransformPoint(targets[i].transform.position);
            var desieredPosToTarget = targetLocalPosition - desieredLocalPosition;


            size = Mathf.Max(size, Math.Abs(desieredPosToTarget.y));
            size = Mathf.Max(size, Math.Abs(desieredPosToTarget.x) / cam.aspect);
        }

        size += screenEdgeBuffer;
        size = Mathf.Max(size, minSize);
        return size;
    }

    protected void Move()
    {
        if (targets.Count <= 1)
        {
            desiredPosition = player.position + offset;
            desiredPosition.y = .8f;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            FindAveragePosition();
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);

        }
    }

    protected void FindAveragePosition()
    {
        var avgPos = new Vector3();
        int numOfTargets = 0; // replace number with target.count

        //check if gameobjects are still alive
        foreach (var obj in targets)
        {
            if (obj == null) targets.Remove(obj);
        }

        for (int i = 0; i < targets.Count; i++)
        {
            avgPos += targets[i].transform.position;
            numOfTargets++;
        }

        avgPos /= numOfTargets;
        avgPos.y = .8f;
        desiredPosition = avgPos;
    }
    #endregion

    #region external functions

    public void AddToList(GameObject other)
    {
        if(!targets.Contains(other)) targets.Add(other);
    }

    public void removeFromList(GameObject other)
    {
        targets.Remove(other);
    }

    #endregion

}
