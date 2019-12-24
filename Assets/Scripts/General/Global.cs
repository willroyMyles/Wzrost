using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Global : MonoBehaviour
{
    #region singleton

    private static Global instance;

    public static Global Instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Global>();
            instance.findObjects();
        }
        return instance;
    }


    #endregion

    #region variables 
    //general 
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject flagPrefab;
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;
    public GameObject playerPrefab4;


    internal GameObject flag;
    internal GameObject plane;
    internal GameObject player1;
    internal GameObject player2;
    internal GameObject player3;
    internal GameObject player4;
    internal GameObject currentPlayer;

    internal Vector3 playgroundSize;

    internal void switchCurrentUser(GameObject obj)
    {
        currentPlayer = obj;
        var camscript = FindObjectOfType<CameraFollow>();
        if (camscript != null)
            camscript.assignPlayerToFollow();
    }

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
    internal Vector3 defaultCameraPosition = new Vector3(0.32f, 14.33f, -10.4f);
    internal float xAngle = 57f;

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







    private void Awake()
    {
        instance = this;
        if (instance == null) instance = FindObjectOfType<Global>();
        instance.findObjects();
    }

    #endregion

    private void Start()
    {
    }

    public void findObjects()
    {
        //create player
        setPlayer(Instantiate(playerPrefab1, playerPrefab1.transform.position, playerPrefab1.transform.rotation));
        //retieve mods
        if(SaveExists(player1.name.Replace("(Clone)", "")))
        {
            var mods = Load<Modifications>(player1.name.Replace("(Clone)", ""));
            //load mods
            Modder.ApplyMods(mods);
        }

        mainCamera = Camera.allCameras[0];

    }

    public void setPlayer(GameObject p)
    {
        player1 = p;
        currentPlayer = p;
        if (!playersOnTeam.Contains(p)) playersOnTeam.Add(p);
        var camscript = FindObjectOfType<CameraFollow>();
        if (camscript != null)
            camscript.assignPlayerToFollow();

    }

    public static void Save<T>(T obj, string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(path + key + ".txt", FileMode.Create))
        {
            formatter.Serialize(fs, obj);
            Debug.Log("Saved");
        }

    }

    public static T Load<T>(string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fs = new FileStream(path + key + ".txt", FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fs);
            Debug.Log("Loaded");

        }

        return returnValue;
    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + "/saves/" + key + ".txt";
        return File.Exists(path);

    }

    #region statistics

    float distance;
    

    #endregion
}
