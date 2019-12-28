using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class BattleController : MonoBehaviour
{

    public GameObject fieldPrefab;
    public TMPro.TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    private void Update()
    {
        text.text = (1 / Time.deltaTime).ToString();
    }
    private void SetUp()
    {
        //spawn three fields
        var field1 = Instantiate(fieldPrefab, Vector3.zero, fieldPrefab.transform.rotation);
      

        //move fields to proper spots
        var size = field1.GetComponent<Renderer>().bounds.size;
        Global.Instance().playgroundSize = size;

        //spawn random objects
        GetComponent<SpawnController>().SpawnRandomObjects();

        //activate nav mesh
        field1.GetComponent<NavMeshSurface>().BuildNavMesh();

        //spawn player
        Global.Instance().FindObjects();


        //get start point
        var playerStartpoint = new Vector3(-size.x/2 + 5, 0, 0);
        var enemyStartpoint = new Vector3(size.x / 2 - 5, 0, 0);

        //spawn in player
        Global.Instance().SetPlayer(Instantiate(Global.Instance().playerPrefab1, playerStartpoint, Global.Instance().playerPrefab1.transform.rotation));

        //spawn team
        GetComponent<SpawnController>().SpawnTeam(Global.Instance().defaultPeopleOnTeam, playerStartpoint);
        GetComponent<SpawnController>().SpawnEnemyTeam(Global.Instance().amountOfPeopleOnTeam, enemyStartpoint);
        Global.Instance().setPlayerPosition(playerStartpoint);

        //spawn buttons
        FindObjectOfType<SwitchController>().setUpCanvas();


        //spawnFlag
        var myflag = Instantiate(Global.Instance().flagPrefab, playerStartpoint + new Vector3(0, Global.Instance().flagPrefab.GetComponent<Renderer>().bounds.size.y, -5), Quaternion.identity);
        var enemyflag = Instantiate(Global.Instance().flagPrefab, enemyStartpoint + new Vector3(-10, Global.Instance().flagPrefab.GetComponent<Renderer>().bounds.size.y, -5), Quaternion.identity);

        Global.Instance().teamFlagPosition = myflag.transform.position;
        Global.Instance().enemyFlagPosition = enemyflag.transform.position;


    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(fieldPrefab.GetComponent<Renderer>().bounds.center, fieldPrefab.GetComponent<Renderer>().bounds.size);
    }
}
