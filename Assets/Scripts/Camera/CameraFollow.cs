using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : CameraBase
{
    private void LateUpdate()
    {
        Move();
    }

}
