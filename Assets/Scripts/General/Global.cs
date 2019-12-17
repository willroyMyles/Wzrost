using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    #region singleton

    private static Global instance;

    public static Global Instance()
    {
        if (instance == null) instance = FindObjectOfType<Global>();
        instance.findObjects();
        return instance;
    }


    #endregion

    #region variables 

    internal GameObject flag;
    internal GameObject plane;
    internal GameObject player;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;

    internal Camera mainCamera;
    internal List<GameObject> objectsInPlayerSpace = new List<GameObject>();

    internal float cameraMovementSpeed = 4;
    internal float cameraMovementDamp = .2f;
    internal float playerDectecionSphereLookRadius = 15f;
    internal float playerPreferredZoomLevel = .02f; // 0 - max, 1-min 

    internal List<GameObject> opponentsWithinSphere = new List<GameObject>();

    internal bool playerWithinDistanceToAim = false;


    private void Awake()
    {
        instance = this;
        if (instance == null) instance = FindObjectOfType<Global>();
        instance.findObjects();
    }

    #endregion

    private void Start()
    {
        findObjects();
    }

    public void findObjects()
    {
        flag = GameObject.Find("flag");
        plane = GameObject.Find("Plane");
        player = GameObject.Find("rollyNavMesh");
        mainCamera = Camera.allCameras[0];
    }

    #region statistics

    float distance;
    

    #endregion
}
