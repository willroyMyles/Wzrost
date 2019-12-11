﻿using System.Collections;
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

    public void spawnEnemies(GameObject obj)
    {
            size = FindObjectOfType<GameController>().PlaygroundSize;

            for (int i = 0; i < 100; i++) {
            var pos = center + new Vector3(UnityEngine.Random.Range(-size.x / 2, size.x / 2), 4f, UnityEngine.Random.Range(-size.z / 2, size.z / 2));
            Instantiate(obj, pos, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(center, size);
    }
}