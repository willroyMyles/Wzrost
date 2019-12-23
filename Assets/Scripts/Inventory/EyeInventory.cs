using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
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
        cc = FindObjectOfType<CustomizeController>();

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

    void replaceEyes(GameObject eyePrefab, bool shouldSave = true)
    {
        if (eyesObject == null) eyesObject = cc.getEyesObject();
        var trans = eyesObject.transform;
      

        var myEye = Instantiate(eyePrefab, trans);
        // let x be zero
        myEye.transform.position = new Vector3(0, myEye.transform.position.y, myEye.transform.position.z);
        myEye.transform.parent = trans.parent;
        DestroyImmediate(eyesObject, true);
        eyesObject = null;

        if (shouldSave)
        {
        var mods = cc.player.GetComponentInChildren<PlayerBase>().mod;
            if (mods != null)
            {
                mods.eyes.changePart(myEye);
                mods.eyes.Type = ModificationType.Eyes;
                saveModifications(mods);
            }
        }
    }

    public static void loadEyesModification(Modifications mod)
    {
        var eyesInterchangable = mod.eyes;

    }

    private void saveModifications(Modifications mods)
    {
        Global.Save(mods, cc.player.name.Replace("(Clone)", ""));
        //Global.Save<float>(1.2f, "float");
    }

    private void loadModifications()
    {
        var mods = Global.Load<Modifications>(cc.player.name.Replace("(Clone)", ""));
    }


}
