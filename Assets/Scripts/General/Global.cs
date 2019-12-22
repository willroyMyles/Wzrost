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
    //general 
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject flagPrefab;
    public GameObject playerPrefab;


    internal GameObject flag;
    internal GameObject plane;
    internal GameObject player;
    internal Vector3 playgroundSize;
    internal Vector3 mySectorSize;
    internal Vector3 middleSectorSize;
    internal Vector3 enemySectorSize;

    internal Vector3 enemyFlagPosition;
    internal Vector3 teamFlagPosition;

    //camera
    internal Camera mainCamera;
    internal float cameraMovementSpeed = 4;
    internal float cameraMovementDamp = .2f;
    internal float playerPreferredZoomLevel = .02f; // 0 - max, 1-min 
    internal float orthoMaxZoom = 23f;
    internal float orthoMinZoom = 10f;

    //player
    internal List<GameObject> objectsInPlayerSpace = new List<GameObject>();
    internal float playerDectecionSphereLookRadius = 15f;
    internal List<GameObject> opponentsWithinSphere = new List<GameObject>();
    internal bool playerWithinDistanceToAim = false;
    internal bool playerAttackEquipped = false;
    internal bool playerDefenseEquipped = false;
    internal bool playerFlagEquipped = false;
    internal TeamNumber myTeamNumber = TeamNumber.One;

    //team?
    internal int amountOfPeopleOnTeam = 4;
    internal int defaultPeopleOnTeam = 3;
    internal Color teamColor = Color.blue;
    internal List<GameObject> playersOnTeam = new List<GameObject>();



    internal Joystick joystick;




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
        player = GameObject.Find("trill");
        mainCamera = Camera.allCameras[0];
    }

    #region statistics

    float distance;
    

    #endregion
}
