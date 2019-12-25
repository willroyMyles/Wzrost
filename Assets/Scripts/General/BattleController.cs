using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class BattleController : MonoBehaviour
{

    public GameObject fieldPrefab;


    // Start is called before the first frame update
    void Start()
    {
        setUp();
    }

    private void setUp()
    {
        //spawn three fields
        var field1 = Instantiate(fieldPrefab, Vector3.zero, fieldPrefab.transform.rotation);
        var field2 = Instantiate(fieldPrefab, Vector3.zero, fieldPrefab.transform.rotation);
        var field3 = Instantiate(fieldPrefab, Vector3.zero, fieldPrefab.transform.rotation);

        //move fields to proper spots
        var size = field1.GetComponent<Renderer>().bounds.size;

        //move field 2
        var pos = field2.transform.position;
        pos.x += size.x / 6 * 4;
        pos.z -= 1.5f;
        field2.transform.position = pos;
        //move and rotate field 3
        pos.x += size.x / 6 * 4;
        pos.z -= 1.5f;
        field3.transform.position = pos;
        field3.transform.Rotate(Vector3.up, 180);

        //activate nav mesh
        field1.GetComponent<NavMeshSurface>().BuildNavMesh();
        Global.Instance().playgroundSize = field1.GetComponent<NavMeshSurface>().GetComponent<Renderer>().bounds.size;

        //setFieldSize
        Bounds bound = new Bounds();
        

        //get start point
        var playerStartpoint = field1.transform.GetChild(0);
        var enemyStartpoint = field3.transform.GetChild(0);

        ////spawn in trill
        //var trill = Instantiate(Global.Instance().playerPrefab1, playerStartpoint.transform.position, Global.Instance().playerPrefab1.transform.rotation);
        //trill.GetComponent<PlayerBase>().assignTeamNumber(TeamNumber.One);
        //Global.Instance().setPlayer(trill);

        //spawn team
        GetComponent<SpawnController>().spawnTeam(Global.Instance().defaultPeopleOnTeam, playerStartpoint.transform.position);
        GetComponent<SpawnController>().spawnEnemyTeam(Global.Instance().amountOfPeopleOnTeam, enemyStartpoint.transform.position);

        //spawn buttons
        FindObjectOfType<SwitchController>().setUpCanvas();
        

        //spawnFlag
        var flag = Instantiate(Global.Instance().flagPrefab, playerStartpoint.position + new Vector3(0, Global.Instance().flagPrefab.GetComponent<Renderer>().bounds.size.y, -5), Quaternion.identity);


    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(fieldPrefab.GetComponent<Renderer>().bounds.center, fieldPrefab.GetComponent<Renderer>().bounds.size);
    }
}
