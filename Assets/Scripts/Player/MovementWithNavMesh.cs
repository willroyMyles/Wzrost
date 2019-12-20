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

    float speed = 10f;
    float pushBackDistance = 2f;
    bool canMove = true;
    bool pushPlayerBack = false;
    NavMeshAgent player;
    Joystick joystick;

    #endregion


    private void Start()
    {
        player = GetComponent<NavMeshAgent>();
        
        //player.updatePosition = false;
    }

    void Update()
    {
        var hori = Input.GetAxis("Horizontal") == 0 ? Global.Instance().joystick.Horizontal : Input.GetAxis("Horizontal");
        var verti = Input.GetAxis("Vertical") == 0 ? Global.Instance().joystick.Vertical : Input.GetAxis("Vertival");

        var vec = new Vector3(hori, 0f, verti);
        vec = vec * speed * Time.deltaTime;

        if(Input.GetMouseButtonDown(0))
        {


            var ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float dist;

            //var plane = new Plane(Vector3.up, 0f);
            //if (plane.Raycast(ray, out dist))
            //{
            //    var pos = ray.GetPoint(dist);
            //    player.SetDestination(pos);
            //}

            var mask = LayerMask.GetMask("platform");

        if(Physics.Raycast(ray, out hit, 1000, mask))
            {
                player.SetDestination(hit.point);
                if (hit.point != Vector3.zero) player.transform.forward = hit.point;

            }



        }




        if (pushPlayerBack)
        {
            player.Move(-transform.forward * pushBackDistance);
            //player.SetDestination(transform.position - transform.forward * pushBackDistance);
          
            pushPlayerBack = false;
        }
    }

    private void disableAllColliders(bool val)
    {
     var    col = GetComponent<SphereCollider>();
        if (val)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }

    public void PushPlayerBack(float blowBack)
    {
        pushBackDistance = blowBack;
        pushPlayerBack = true;
    }
}
