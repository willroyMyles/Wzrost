using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    MovementWithNavMesh playerNav;
    EnemyController enemyNav;

[SerializeField]    internal bool isPlayerControlled = false;
    void Start()
    {
        playerNav = GetComponent<MovementWithNavMesh>();
        enemyNav = GetComponent<EnemyController>();
        playerNav.enabled = false;
        enemyNav.enabled = false;

        if (isPlayerControlled) playerNav.enabled = true;
        else enemyNav.enabled = true;
        
    }

    public void setIsPlayerEnabled( bool playerEnabled)
    {
        if(playerNav == null)
            playerNav = GetComponent<MovementWithNavMesh>();
        if(enemyNav == null)
            enemyNav = GetComponent<EnemyController>();

        

        playerNav.enabled = playerEnabled;
            enemyNav.setPlayerControlled( !playerEnabled);
        isPlayerControlled = playerEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
