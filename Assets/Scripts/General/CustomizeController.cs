﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeController : MonoBehaviour
{

    public Canvas canvas;
    [SerializeField] internal GameObject player;
    internal GameObject eyesObject;
    Camera cam;

    Transform item;

    public Transform left, right, front, eyes;
    CanvasGroup g1, g2, g3;
    // Start is called before the first frame update
    void Start()
    {

        setUp();

    }

    private void setUp()
    {
        if (Global.Instance().player1 == null)
        {
            player = Instantiate(Global.Instance().playerPrefab1, Global.Instance().playerPrefab1.transform.position, Global.Instance().playerPrefab1.transform.rotation);
            Global.Instance().SetPlayer(player);
        }
        else player = Global.Instance().currentPlayer;
        cam = Global.Instance().mainCamera;
        var grps = canvas.GetComponentsInChildren<CanvasGroup>();
        g1 = grps[0];
        g2 = grps[1];
        g3 = grps[2];

       
    }

    public GameObject getEyesObject()
    {
        if (player == null) player = Global.Instance().currentPlayer;
        var objs = player.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0);
        return objs.gameObject;
    }

    public void DestroyEyes()
    {
        Destroy(player.transform.Find("GameObject").Find("tram").Find("head").GetChild(0).gameObject);
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public void moveCameraToFront() { StartCoroutine(moveCameraToFront(10)); }
    public void moveCameraToLeft() { StartCoroutine(moveCamera(left.position, -25)); }
    public void moveCameraToRight() { StartCoroutine(moveCamera(right.position, 25)); }
    public void moveCameraToEyes() { StartCoroutine(moveCameraToEyes(10)); }

    IEnumerator moveCameraToFront(float t)
    {
        while (true)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, front.position, Time.deltaTime * t);
            cam.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(cam.transform.eulerAngles.y, 0, Time.deltaTime * t), 0);
            g1.alpha = Mathf.Lerp(g1.alpha, 1, Time.deltaTime * t);
            g3.alpha = Mathf.Lerp(g3.alpha, 0, Time.deltaTime * t);

            yield return null;
            if (Vector3.Distance(cam.transform.position, front.position) < .02f)
            {
                cam.transform.position = front.position;
                cam.transform.eulerAngles = new Vector3(0, 0, 0);
                g1.alpha = 1;
                g3.alpha = 0;
                break;
            }
        }
    }

    internal GameObject getObject(string objectToGet)
    {
        throw new NotImplementedException();
    }

    IEnumerator moveCameraToEyes(float t)
    {
        while (true)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, eyes.position, Time.deltaTime * t);
            g1.alpha = Mathf.Lerp(g1.alpha, 0, Time.deltaTime * t);
            g3.alpha = Mathf.Lerp(g3.alpha, 1, Time.deltaTime * t);

            yield return null;
            if (Vector3.Distance(cam.transform.position, eyes.position) < .02f)
            {
                cam.transform.position = eyes.position;
                g1.alpha = 0;
                g3.alpha = 1;
                break;
            }
        }
    }

    IEnumerator moveCamera(Vector3 pos, float angle)
    {
        var t = 10;
        while (true)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, pos, Time.deltaTime * t);
            cam.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(cam.transform.eulerAngles.y, angle, Time.deltaTime * t), 0);
            if(angle == 0) {
                g1.alpha = Mathf.Lerp(g1.alpha, 1, Time.deltaTime * t);
                if (g1.alpha - 1 < .09f) g1.alpha = 1;
            }
            else
            {
                g1.alpha = Mathf.Lerp(g1.alpha, 0, Time.deltaTime * t);
                if (g1.alpha + 0 < .09f) g1.alpha = 0;

            }
            yield return null;
            var y = cam.transform.eulerAngles.y;

            if(angle < 0)
            {
                if (y - (360 + angle) > -1 && y - (360 + angle) < 1)
                {
                    cam.transform.position = pos;
                    cam.transform.eulerAngles = new Vector3(0, angle, 0);
                    
                    break;
                }
            }
            else
            {
                if (y - (0 + angle) > -2 && y - (0 + angle) < 2)
                {
                    cam.transform.position = pos;
                    cam.transform.eulerAngles = new Vector3(0, angle, 0);
                    break;
                }
            }


           
        }
    }
}
