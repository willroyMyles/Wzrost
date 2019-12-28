using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    internal Image healthImageBg;
    internal Image healthImage;
    internal GameObject panel;

    private void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.speed = 10;
        agent.angularSpeed = 700;
        agent.acceleration = 80;

        //set up canvas
        SetupCanvas();
    }

    private void SetupCanvas()
    {
        healthImageBg = Instantiate(Global.Instance().playerCanvas,Global.Instance().worldSpaceCanvas.transform).GetComponentInChildren<Image>();
        healthImage = healthImageBg.transform.GetChild(0).GetComponent<Image>();
    }

    internal void StartFight(List<GameObject> listOfObjectInSphere)
    {
        opponentsList = listOfObjectInSphere;
        SetShouldFight(true);
    }

    private void Update()
    {

        if (false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                var ray = Global.Instance().mainCamera.ScreenPointToRay(Input.mousePosition);
                var mask = LayerMask.GetMask("platform");
                if (Physics.Raycast(ray, out var hit, 100, mask))
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
                //if (agent.transform.position == agent.nextPosition && agent.isStopped) agent.isStopped = false;
                //patrol
                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    agent.SetDestination(GetRandompointOnPlane());
                    agent.transform.LookAt(agent.destination);
                }
            }
         
        }

        //auto fire if is fighting
        if (isFighting)
        {
            //canMove = false;
            float lowestHealthEnemy = 1000;
            GameObject obj = null;
            if (opponentsList.Count <= 0)
            {
                SetShouldFight(false);
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
                health = en.GetComponent<PlayerBase>().Hp;
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


            if (TryGetComponent<FireController>(out var efm))
            {
                if (efm.getCanFire())
                {
                    if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, 10f ))
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

    private void LateUpdate()
    {
        UpdateImagePosition();

    }

    private void UpdateImagePosition()
    {
        //healthImageBg.transform.position = Global.Instance().mainCamera.WorldToScreenPoint(transform.position);
        healthImageBg.transform.position =transform.position;
        //healthImageBg.rectTransform.eulerAngles = transform.up;
    }

    internal void PushPlayerBack(float bb)
    {
        pushbackDistance = bb;
        isPushedBack = true;
    }

    private void SetShouldFight(bool v)
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
        if (!agent) SetUp();
        isPlayerControlled = playerControl;
        agent.updatePosition = playerControl;
    }


}
