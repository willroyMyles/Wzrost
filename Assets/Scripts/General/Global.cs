using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    #region singleton

    public Global instance()
    {
        return this;
    }


    #endregion

    #region variables 

    GameObject flag;
    GameObject plane;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;


    private void Awake()
    {
        flag = GameObject.Find("flag");
        plane = GameObject.Find("Plane");
    }

    #endregion

    #region statistics

    float distance;
    

    #endregion
}
