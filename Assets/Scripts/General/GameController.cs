using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        playgroundSize = ground.GetComponent<MeshRenderer>().bounds.size;
        ground.GetComponent<NavMeshSurface>().BuildNavMesh();
        if(enemyPrefab != null) GetComponent<SpawnController>().spawnEnemies(enemyPrefab, amountOfEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
