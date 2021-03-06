﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TeamNumber
{
    Default,
    One,
    Two,
    Three,
    Four
}
public class GameController : MonoBehaviour
{
    public GameObject ground;
    public GameObject enemyPrefab;
    public int amountOfEnemies;

    Vector3 playgroundSize;

    public Vector3 PlaygroundSize { get => playgroundSize; set => playgroundSize = value; }

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<SpawnController>().spawnPlayer();


        playgroundSize = ground.GetComponent<MeshRenderer>().bounds.size;
        ground.GetComponent<NavMeshSurface>().BuildNavMesh();
        if (enemyPrefab != null)
        {
            //GetComponent<SpawnController>().spawnEnemies(enemyPrefab, amountOfEnemies);
            GetComponent<SpawnController>().SpawnTeam(3);
            GetComponent<SpawnController>().SpawnEnemyTeam(4);
        }
        Global.Instance().currentPlayer.GetComponent<PlayerBase>().assignTeamNumber(Global.Instance().myTeamNumber);
        Global.Instance().playersOnTeam.Add(Global.Instance().currentPlayer);
        FindObjectOfType<CameraBase>().AssignPlayerToFollow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
