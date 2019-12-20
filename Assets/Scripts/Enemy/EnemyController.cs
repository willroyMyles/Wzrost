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
    bool canMove = true;
    bool shouldFight = false;
    private NavMeshAgent agent;
    List<GameObject> fightList;
    EnemyStats stats;
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
        var point = gameController.PlaygroundSize;
        point.x = UnityEngine.Random.Range(0, point.x / 2);
        point.z = UnityEngine.Random.Range(0, point.z / 2);
        return point/50;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            agent.transform.position = agent.nextPosition;
            agent.transform.LookAt(agent.nextPosition);
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
            foreach(var en in fightList)
            {
                float health;
                if(en.tag == "Player") health = en.transform.parent.GetComponent<PlayerBase>().Hp;
                else health = en.transform.parent.GetComponent<EnemyBase>().Hp;
                if (lowestHealthEnemy > health)
                {
                    lowestHealthEnemy = health;
                    obj = en;
                }
            } 
            //faceEnemy
            if(obj != null) agent.transform.LookAt(obj.transform.position);

            //check aggreesion
            if (stats.aggression < .5f)
            {
                shouldFight = false;
                return;
            }

            //fire bullet
            var efm = GetComponent<EnemyFireController>();
            efm.Fire();
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
        
        
        fightList = list;
        if(list.Count > 0)
        {
            shouldFight = true;
            agent.isStopped = true;

        }
        else
        {
            shouldFight = false;
            agent.isStopped = false;
        }
    }

    private void OnDestroy()
    {
        this.enabled = false;
    }

}
