using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : CameraBase
{
    private new void Start()
    {
        base.Start();
    }
    private void LateUpdate()
    {
        Move();
        Zoom();
    }

}
