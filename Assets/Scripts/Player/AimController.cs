﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{

    float distance;
    DetectionSphere ds;

    private void Start()
    {
        ds = GetComponent<DetectionSphere>();
    }

    private void LateUpdate()
    {
        aim();
    }

    private void aim()
    {
        if (ds.playerEithinDistanceToAim)
        {
            //get shorest distance
            distance = 16;
            Transform opp = default;
            foreach(var obj in ds.detectionList)
            {
                if (obj == gameObject) continue;
                if (obj == null) continue;
                var dis = Vector3.Distance(transform.position, obj.transform.position);
                if (distance > dis)
                {
                    distance = dis;
                    opp = obj.transform;
                }
            }

            //turn to opponent?
            transform.LookAt(opp);

            //draw line to target
            //var line = GetComponent<LineRenderer>();
            //if(!line.enabled) line.enabled = true;
            //line.material.mainTextureScale = new Vector3(2, 10, 3);
            //Vector3[] vecs = new[] { transform.position, opp.parent.position };
          
            //line.SetPositions(vecs);
        }

    }
}
