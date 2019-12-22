using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System;

public class MovementWithNavMesh : MonoBehaviour
{

    #region variables

    float pushBackDistance = 2f;
    bool pushPlayerBack = false;
    NavMeshAgent player;

    #endregion


    private void Start()
    {
        player = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            var ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var mask = LayerMask.GetMask("platform");

        if(Physics.Raycast(ray, out hit, 1000, mask))
            {
                player.SetDestination(hit.point);
               // if (hit.point != Vector3.zero) player.transform.forward = hit.point;

            }
        }

        if (pushPlayerBack)
        {
            player.Move(-transform.forward * pushBackDistance);
            //player.SetDestination(transform.position - transform.forward * pushBackDistance);
          
            pushPlayerBack = false;
        }
    }

    public void PushPlayerBack(float blowBack)
    {
        pushBackDistance = blowBack;
        pushPlayerBack = true;
    }
}
