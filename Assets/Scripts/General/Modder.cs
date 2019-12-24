using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modder : MonoBehaviour
{

    public static void ApplyMods(Modifications mod)
    {
        if (mod.eyes.isEquipped) applyEyeMod(mod.eyes);
        if (mod.rightArm.isEquipped) applyRAMod(mod.rightArm);
        if (mod.leftArm.isEquipped) applyLAMod(mod.leftArm);
        if (mod.wheels.isEquipped) applyWheelsMod(mod.wheels);
        if (mod.shaft.isEquipped) applyShaftMod(mod.shaft);
    }

    private static void applyShaftMod(Interchangable shaft)
    {
        throw new NotImplementedException();
    }

    private static void applyWheelsMod(Interchangable wheels)
    {
        throw new NotImplementedException();
    }

    private static void applyLAMod(Interchangable leftArm)
    {
        throw new NotImplementedException();
    }

    private static void applyRAMod(Interchangable rightArm)
    {
        throw new NotImplementedException();
    }


    private static GameObject getEye()
    {
        return Global.Instance().currentPlayer.transform.Find("GameObject").Find("tram").Find("head").GetChild(0).gameObject;
    }

    private static void applyEyeMod(Interchangable eyes)
    {


        var cc = FindObjectOfType<CustomizeController>();
        var eyesObject = getEye();
        var trans = eyesObject.transform;
        var eyePrefab = (GameObject)Resources.Load("Eyes/" + eyes.prefabReference, typeof(GameObject));

        var myEye = Instantiate(eyePrefab, trans);
        // let x be zero
        myEye.transform.position = new Vector3(0, myEye.transform.position.y, myEye.transform.position.z);
        myEye.transform.parent = trans.parent;
        DestroyImmediate(eyesObject, true);
        eyesObject = null;

        //applyGenericMod("Eyes/" + eyes.prefabReference, "eyes"); ;

    }

    private static void applyGenericMod(string source, string objectToGet)
    {
        var cc = FindObjectOfType<CustomizeController>();
        GameObject objecttoreplace = cc.getObject(objectToGet);
        var transformForObject = objecttoreplace.transform;
        var prefab = (GameObject)Resources.Load(source, typeof(GameObject));
        var myObj = Instantiate(prefab, transformForObject);

        myObj.transform.position = new Vector3(0, myObj.transform.position.y, myObj.transform.position.z);
        myObj.transform.parent = transformForObject.parent;
        DestroyImmediate(objecttoreplace, true);
        objecttoreplace = null;
    }
}
