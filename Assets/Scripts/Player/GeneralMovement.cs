using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class GeneralMovement : MonoBehaviour
{

    internal float pushbackDistance = 2;
    internal bool isPushedBack = false;
    internal bool isPlayerControlled = false;
    internal bool ableToMove = true;
    internal bool isFighting = false;
    internal List<GameObject> opponentsList;
    internal NavMeshAgent agent;

    private void Awake()
    {
        setUp();
    }

    private void setUp()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.speed = 10;
        agent.angularSpeed = 700;
        agent.acceleration = 80;
    }

    internal void startFight(List<GameObject> listOfObjectInSphere)
    {
    }

    private void Update()
    {

        if (isPlayerControlled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                var ray = Global.Instance().mainCamera.ScreenPointToRay(Input.mousePosition);
                var mask = LayerMask.GetMask("platform");
                if (Physics.Raycast(ray, out var hit, 1000, mask))
                {
                    agent.SetDestination(hit.point);
                }
            }

            if (isPushedBack)
            {
                agent.Move(-transform.forward * pushbackDistance);
                //player.SetDestination(transform.position - transform.forward * pushBackDistance);
                isPushedBack = false;
            }
        }
        else
        {
            if (ableToMove)
            {
                agent.transform.position = agent.nextPosition;
                agent.transform.LookAt(agent.nextPosition);
                if (agent.transform.position == agent.nextPosition && agent.isStopped) agent.isStopped = false;
                //patrol
                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    agent.SetDestination(GetRandompointOnPlane());
                }
            }
            if (isFighting)
            {
                //canMove = false;
                float lowestHealthEnemy = 1000;
                GameObject obj = null;
                if (opponentsList.Count <= 0)
                {
                    setShouldFight(false);
                    return;
                }
               foreach (var en in opponentsList)
                {
                    float health;
                    if (en == null)
                    {
                        opponentsList.Remove(en);
                        agent.isStopped = false;
                        return;
                    }
                    if (en.tag == "Interactor") health = en.GetComponent<PlayerBase>().Hp;
                    else health = en.transform.parent.GetComponent<PlayerBase>().Hp;
                    if (lowestHealthEnemy > health)
                    {
                        lowestHealthEnemy = health;
                        obj = en;
                    }
                }
                //faceEnemy
                if (obj == null) return;
                agent.transform.LookAt(obj.transform.position);
                //fire bullet if i have a stright line

                var efm = GetComponent<EnemyFireController>();
                if (efm.getCanFire())
                {
                    if (Physics.Raycast(transform.position, transform.forward, out var hitInfo))
                    {
                        if (hitInfo.collider.gameObject == obj.transform.Find("Cube").gameObject)
                        {
                            efm.Fire();
                        }
                    }
                }
            }
        }
    }

    internal void PushPlayerBack(float bb)
    {
        throw new NotImplementedException();
    }

    private void setShouldFight(bool v)
    {
        isFighting = v;
    }

    Vector3 GetRandompointOnPlane()
    {
        var point = Global.Instance().playgroundSize;
        point.x = UnityEngine.Random.Range(-point.x / 2, point.x / 2);
        point.z = UnityEngine.Random.Range(-point.z / 2, point.z / 2);
        return point;
    }

    public void setPlayerControlled(bool playerControl)
    {
        if (!agent) setUp();
        isPlayerControlled = playerControl;
        agent.updatePosition = playerControl;
    }


}
