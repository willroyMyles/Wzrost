using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPerspective : CameraBase
{

    protected float originalY, originalZ;
    Vector3 centerPoint;
    float minZoom = 35f;
    float maxZoom = 65f;
    float zoomBuffer = 5f;

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

    protected void Move()
    {
        if (targets.Count <= 1)
        {
            desiredPosition = player.localPosition + offset;
            desiredPosition.y = originalY;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {


            centerPoint = FindAveragePosition();
            var newPosition = centerPoint + offset;
            //desiredPosition.y = originalY;
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref moveVelocity, dampTime);

        }
    }

    protected new void Zoom()
    {
        if( targets.Count <= 1)
        {
            float zoomDistance = Mathf.Lerp(maxZoom, minZoom, .3f);
            cam.fieldOfView = zoomDistance;
        }
        else
        {
            var distance = getGreatestDistance();
            float zoomDistance = Mathf.Lerp(maxZoom, minZoom, (distance-zoomBuffer) / maxZoom);
            cam.fieldOfView = zoomDistance;
        }
    }

    private float getGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null) bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.size.x;
    }

    protected new Vector3 FindAveragePosition()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);


        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i] != null) bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;

    }




}
