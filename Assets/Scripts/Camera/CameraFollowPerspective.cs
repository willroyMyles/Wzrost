using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPerspective : CameraBase
{

    protected float originalY, originalZ;
    Vector3 centerPoint;
    float minZoom = 45f;
    float maxZoom = 72f;
    float zoomBuffer = 35f;

    private new void Start()
    {
        offset = transform.position - player.position;
        cam = Camera.allCameras[0] ;
        originalY = offset.y;
        originalZ = offset.z;
    }

    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    protected new void Move()
    {
            var pos = FindAveragePosition() + offset;
            transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
    }

    protected new void Zoom()
    {
        float zoomDistance;
        if( targets.Count <= 1)       zoomDistance = Mathf.Lerp(maxZoom, minZoom, Global.Instance().playerPreferredZoomLevel);
        else                          zoomDistance = Mathf.Lerp(minZoom, maxZoom, getGreatestDistance() / Global.Instance().playerDectecionSphereLookRadius);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomDistance, smoothSpeed * Time.deltaTime);
    }



    private float getGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
   

        for (int i = 0; i < Global.Instance().opponentsWithinSphere.Count; i++)
        {
            if ( Global.Instance().opponentsWithinSphere[i] != null) bounds.Encapsulate(Global.Instance().opponentsWithinSphere[i].transform.position);
        }
        return bounds.size.x;
    }

    protected new Vector3 FindAveragePosition()
    {
        var vec = player.localPosition;
        int amount = 0;

        foreach(var obj in Global.Instance().opponentsWithinSphere)
        {
            if (obj == null) vec += Vector3.zero;
            else vec += obj.transform.position;
            amount++;
        }

        vec /= amount + 1;
        return vec;
    }




}
