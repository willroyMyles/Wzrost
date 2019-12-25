using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    Vector3 center, size;
    // Start is called before the first frame update
    void Start()
    {
        center = Vector3.zero;

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemies(GameObject obj, int amount)
    {
        size = FindObjectOfType<GameController>().PlaygroundSize;

        int divisionFactor = 10; // 2 by default for eeach half

        for (int i = 0; i < amount; i++)
        {
            var pos = center + new Vector3(UnityEngine.Random.Range(-size.x / divisionFactor, size.x / divisionFactor), 4f, UnityEngine.Random.Range(-size.z / divisionFactor, size.z / divisionFactor));
            Instantiate(obj, pos, Quaternion.identity);
        }
    }

    internal void spawnPlayer()
    {
        Global.Instance().setPlayer(Instantiate(Global.Instance().playerPrefab1));
    }

    internal void spawnTeam(int v)
    {
        size = FindObjectOfType<GameController>().PlaygroundSize;

        int divisionFactor = 10; // 2 by default for eeach half

        for (int i = 0; i < v; i++)
        {
            var pos = center + new Vector3(UnityEngine.Random.Range(-size.x / divisionFactor, size.x / divisionFactor), 4f, UnityEngine.Random.Range(-size.z / divisionFactor, size.z / divisionFactor));
            var en = Instantiate(Global.Instance().enemyPrefab, pos, Quaternion.identity);
            en.GetComponent<EnemyBase>().assignTeamNumber(Global.Instance().myTeamNumber);
            Global.Instance().playersOnTeam.Add(en);
        }
    }

    internal void spawnTeam(int v, Vector3 pos)
    {
        for (int i = 0; i < v; i++)
        {
            var en = Instantiate(Global.Instance().playerPrefab1, pos, Quaternion.identity);
            en.GetComponent<PlayerBase>().assignTeamNumber(Global.Instance().myTeamNumber);
            en.GetComponent<GeneralMovement>().setPlayerControlled(false);
            Global.Instance().playersOnTeam.Add(en);
        }
    }

    internal void spawnEnemyTeam(int v)
    {
        int divisionFactor = 10; // 2 by default for eeach half

        for (int i = 0; i < v; i++)
        {
            var pos = center + new Vector3(UnityEngine.Random.Range(-size.x / divisionFactor, size.x / divisionFactor), 4f, UnityEngine.Random.Range(-size.z / divisionFactor, size.z / divisionFactor));
            var en = Instantiate(Global.Instance().enemyPrefab, pos, Quaternion.identity);
            en.GetComponent<EnemyBase>().assignEnemeyTeamNumber(TeamNumber.Two);
        }
    }

    internal void spawnEnemyTeam(int v, Vector3 pos)
    {
        for (int i = 0; i < v; i++)
        {
            var en = Instantiate(Global.Instance().enemyPrefab, pos, Quaternion.identity);
            en.GetComponent<EnemyBase>().assignEnemeyTeamNumber(TeamNumber.Two);
        }
    }

    public void spawnEnemies(GameObject obj)
    {
            size = FindObjectOfType<GameController>().PlaygroundSize;

            for (int i = 0; i < 100; i++) {
            var pos = center + new Vector3(UnityEngine.Random.Range(-size.x / 2, size.x / 2), 4f, UnityEngine.Random.Range(-size.z / 2, size.z / 2));
            Instantiate(obj, pos, Quaternion.identity);
        }
    }

    public void spawnRandomObjects()
    {
        var objectssss = Resources.LoadAll("Random/", typeof(GameObject));
        var point = Global.Instance().playgroundSize;
     

        foreach(GameObject obj in objectssss)
        {
            point.x = UnityEngine.Random.Range(-point.x / 2, point.x / 2);
            point.z = UnityEngine.Random.Range(-point.z / 2, point.z / 2);
            point.y = obj.transform.position.y;
            Instantiate(obj, point, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(center, size);
    }
}
