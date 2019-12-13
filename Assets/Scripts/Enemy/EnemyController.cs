using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    float lookradiius = 5f;

    GameController gameController;
    bool canMove = true;
    bool shouldFight = false;
    private NavMeshAgent agent;
    List<GameObject> fightList;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        gameController = FindObjectOfType<GameController>();
        agent.SetDestination(GetRandompointOnPlane());
        
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
            }
        }
        if (shouldFight)
        {
            //canMove = false;
            float lowestHealthEnemy = 1000;
            GameObject obj = null;
            foreach(var en in fightList)
            {
                var health = GetComponent<EnemyBase>().Hp;
                if (lowestHealthEnemy > health)
                {
                    lowestHealthEnemy = health;
                    obj = en;
                }
            } 
            //faceEnemy
            agent.transform.LookAt(obj.transform.position);
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

 
}
