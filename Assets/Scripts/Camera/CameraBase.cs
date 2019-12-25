using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    // Start is called before the first frame update
    #region variables


    internal Transform player;
    protected float smoothSpeed = 4f;  //hight is faster snapping
    protected Vector3 offset;


    protected float dampTime = .2f, screenEdgeBuffer = 4f, minSize = 10f;
    protected float zoomSpeed;
    protected Vector3 moveVelocity;
    protected Vector3 desiredPosition;
    protected Camera cam;


    bool objectInSphere = false;

    public bool ObjectInSphere { get => objectInSphere; set => objectInSphere = value; }

    #endregion
    #region internal functions

    public void assignPlayerToFollow()
    {
        
        cam = Global.Instance().mainCamera;
        transform.position = Global.Instance().currentPlayer.transform.position;
        transform.position += Global.Instance().defaultCameraPosition;
        player = Global.Instance().currentPlayer.transform;
        offset = transform.position - Global.Instance().currentPlayer.transform.position;
    }
    protected void Start()
    {
       // assignPlayerToFollow();
    }


    private void LateUpdate()
    {

    }

    protected void Zoom()
    {
        float requieredSize = getGreatestDistance();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requieredSize, ref zoomSpeed, dampTime);
    }

    private float FindRequiredSize()
    {
        if (Global.Instance().opponentsWithinSphere.Count <= 1) return Mathf.Lerp( Global.Instance().orthoMinZoom, Global.Instance().orthoMaxZoom, Global.Instance().playerPreferredZoomLevel);

        var desieredLocalPosition = transform.InverseTransformPoint(desiredPosition);
        float size = 0;
        for (int i = 0; i < Global.Instance().opponentsWithinSphere.Count; i++)
        {
            if (Global.Instance().opponentsWithinSphere[i] == null) continue;
            var targetLocalPosition = transform.InverseTransformPoint(Global.Instance().opponentsWithinSphere[i].transform.position);
            var desieredPosToTarget = targetLocalPosition - desieredLocalPosition;


            size = Mathf.Max(size, Math.Abs(desieredPosToTarget.y));
            size = Mathf.Max(size, Math.Abs(desieredPosToTarget.x) / cam.aspect);
        }

        size += screenEdgeBuffer;
        size = Mathf.Max(size, minSize);
        return size;
    }

    private float getGreatestDistance()
    {
        if (Global.Instance().opponentsWithinSphere.Count <= 1) return Mathf.Lerp(Global.Instance().orthoMinZoom, Global.Instance().orthoMaxZoom, Global.Instance().playerPreferredZoomLevel);

        var bounds = new Bounds(Global.Instance().opponentsWithinSphere[0].transform.position, Vector3.zero);


        for (int i = 0; i < Global.Instance().opponentsWithinSphere.Count; i++)
        {
            if (Global.Instance().opponentsWithinSphere[i] != null) bounds.Encapsulate(Global.Instance().opponentsWithinSphere[i].transform.position);
        }
        return bounds.size.x + 5;
    }

    protected void Move()
    {
        var pos = FindAveragePosition() + offset;
        //transform.LookAt(Global.Instance().currentPlayer.transform);
        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
    }

    protected Vector3 FindAveragePosition()
    {
        var vec = player.localPosition;
        int amount = 0;

        foreach (var obj in Global.Instance().currentPlayer.GetComponent<DetectionSphere>().detectionList)
        {
            if (obj == null) vec += Vector3.zero;
            else vec += obj.transform.position;
            amount++;
        }

        vec /= amount + 1;
        return vec;
    }
    #endregion



}
