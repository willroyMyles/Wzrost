using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementWithNavMesh : MonoBehaviour
{

    #region variables

    float speed = 10f;
    float pushBackDistance = 2f;
    bool canMove = true;
    bool pushPlayerBack = false;
    NavMeshAgent player;

    #endregion


    private void Start()
    {
        player = GetComponent<NavMeshAgent>();
        //player.updatePosition = false;
    }

    void Update()
    {
        var hori = Input.GetAxis("Horizontal");
        var verti = Input.GetAxis("Vertical");

        var vec = new Vector3(hori, 0f, verti);
        vec = vec * speed * Time.deltaTime;


        player.Move(vec);
        player.SetDestination(player.transform.position + vec);
        //player.transform.LookAt(vec);
        if (vec != Vector3.zero) player.transform.forward = vec;

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
