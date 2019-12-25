using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct EnemyStats
{
    public float aggression;

}

public class EnemyController : MonoBehaviour
{

    float lookradiius = 5f;

    GameController gameController;
    [SerializeField] bool canMove = true;
    [SerializeField] bool shouldFight = false;
    private NavMeshAgent agent;
    [SerializeField] List<GameObject> fightList;
    EnemyStats stats;

    [SerializeField] internal bool isPlayerControlled = false;

    public void setShouldFight(bool val)
    {
        shouldFight = val;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        gameController = FindObjectOfType<GameController>();
        agent.SetDestination(GetRandompointOnPlane());
        stats.aggression = UnityEngine.Random.Range(0f, 1f);
    }

    Vector3 GetRandompointOnPlane()
    {
        var point = Global.Instance().playgroundSize;
        point.x = UnityEngine.Random.Range(0, point.x / 2);
        point.z = UnityEngine.Random.Range(0, point.z / 2);
        return point/50;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerControlled)
        {
            if (agent.updatePosition) agent.updatePosition = false;

            if (canMove)
            {
                agent.transform.position = agent.nextPosition;
                agent.transform.LookAt(agent.nextPosition);
                if (agent.transform.position == agent.nextPosition && agent.isStopped) agent.isStopped = false;
                //patrol
                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    agent.SetDestination(GetRandompointOnPlane());
                    stats.aggression = UnityEngine.Random.Range(0f, 1f);
                }
            }
            if (shouldFight)
            {
                //canMove = false;
                float lowestHealthEnemy = 1000;
                GameObject obj = null;
                checkFightList();
                if(fightList.Count <= 0)
                {
                    setShouldFight(false);
                    return;
                }
;                foreach (var en in fightList)
                {
                    float health;
                    if (en == null)
                    {
                        fightList.Remove(en);
                        agent.isStopped = false;
                        return;
                    }
                    if (en.tag == "Interactor") health = en.GetComponent<PlayerBase>().Hp;
                    else health = en.GetComponent<PlayerBase>().Hp;
                    if (lowestHealthEnemy > health)
                    {
                        lowestHealthEnemy = health;
                        obj = en;
                    }
                }
                //faceEnemy
                if (obj != null) agent.transform.LookAt(obj.transform.position);


               
                //fire bullet if i have a stright line
                
                var efm = GetComponent<EnemyFireController>();
                if (efm.getCanFire())
                {
                    if(Physics.Raycast(transform.position, transform.forward, out var hitInfo))
                    {
                        if(hitInfo.collider.gameObject == obj.transform.Find("Cube").gameObject)
                        {
                            efm.Fire();
                        }
                    }
                }
            }
        }
    }

    private void checkFightList()
    {
        for (int i =0; i < fightList.Count; i++)
        {
            if (fightList[i] == null) fightList.Remove(fightList[i]);
        }
    }

    public IEnumerator stunPlayer(float stunTime)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTime);
        agent.isStopped = false;
    }

    public void startFight(List<GameObject> list)
    {
        //set agent to fight instead of stopping
        
        fightList = list;
        if(list.Count > 0)
        {
            setShouldFight(true);
            agent.isStopped = true;

        }
        else
        {
            setShouldFight(false);
            agent.isStopped = false;
        }
    }

    private void OnDestroy()
    {
        this.enabled = false;
    }
    public void setPlayerControlled(bool enabled)
    {

        if (!enabled)
        {
           if(agent) agent.SetDestination(transform.position);
            canMove = false;
        }
        else
        {
            if(agent) agent.SetDestination(GetRandompointOnPlane());
            canMove = true;
        }

        this.enabled = enabled;
    }


}
