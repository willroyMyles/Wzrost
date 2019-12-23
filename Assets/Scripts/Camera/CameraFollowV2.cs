using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowV2 : MonoBehaviour
{

    Vector3 offset;
    
    private void Start()
    {
        offset = transform.localPosition - Global.Instance().currentPlayer.transform.position;
    }

    private void LateUpdate()
    {
        move();
        zoom();
    }

    private void zoom()
    {
    }

    private void move()
    {
        var point = findAveragePosition();
        // point += offset;
        point = point + offset;
        if (point == transform.position) return;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, point, Global.Instance().cameraMovementSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }

    private Vector3 findAveragePosition()
    {
        var vec = Global.Instance().currentPlayer.transform.position;
        foreach(var obj in Global.Instance().objectsInPlayerSpace)
        {
            vec += obj.transform.position;
        }

        vec /= Global.Instance().objectsInPlayerSpace.Count + 1;
        return vec;
    }
}
