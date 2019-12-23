using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EyeInventory : MonoBehaviour
{

    public Transform content;
    Transform item;
    GameObject eyesObject;
    CustomizeController cc;
    UnityEngine.Object[] eyes = null;

    void Start()
    {
        item = content.GetChild(0);

        // look into asset bundle later
        getInventoryFromFolder();
    }

    private void getInventoryFromFolder()
    {
        float cellSize = 230;
        int x = 0;
        int y = 0;
        eyes = Resources.LoadAll("Eyes", typeof(GameObject));
        if (eyes == null) return;
        // get images from objects names
        if (eyes.Length <=1)
        {

        }
        else
        {
            foreach (var obj in eyes)
            {
                var name = obj.name;
                Sprite spriteImage =(Sprite) Resources.Load("Eyes/images/" + name, typeof(Sprite));
                var sprite = Instantiate(item, content).GetComponent<RectTransform>();
                sprite.gameObject.SetActive(true);
                sprite.anchoredPosition = new Vector2(x * cellSize, 0);
                Image image = sprite.Find("Image").GetComponent<Image>();
                image.sprite = spriteImage;
                x++;

                setUpButton(sprite, obj as GameObject);
            }
        }
    }

    private void setUpButton(RectTransform item,  GameObject eyePrefab)
    {
        var btn = item.GetComponentInChildren<Button>();
        btn.onClick.AddListener(delegate { replaceEyes(eyePrefab);  } );
    }

    void replaceEyes(GameObject eyePrefab)
    {
        cc = FindObjectOfType<CustomizeController>();
        if (eyesObject == null) eyesObject = cc.getEyesObject();
        var trans = eyesObject.transform;
      

        var myEye = Instantiate(eyePrefab, trans);
        // let x be zero
        myEye.transform.position = new Vector3(0, myEye.transform.position.y, myEye.transform.position.z);
        myEye.transform.parent = trans.parent;

        DestroyImmediate(eyesObject, true);
        eyesObject = null;
        //save prefab
        savePrefab();
    }

    private void savePrefab()
    {
        var player = cc.getPlayer();
        var name = player.name.Replace("(Clone)", "");
        bool success;
        PrefabUtility.SaveAsPrefabAsset(player, "Assets/Resources/Player/"+ name+".prefab", out success);
        if (success) Debug.Log("saved");
        else Debug.Log("Error");
    }
}
