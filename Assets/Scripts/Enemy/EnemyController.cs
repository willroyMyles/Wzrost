using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    float lookradiius = 5f;

    GameController gameController;
    bool canMove = true;
    private NavMeshAgent agent;

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
    }

    public IEnumerator stunPlayer(float stunTime)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTime);
        agent.isStopped = false;
    }

 
}
