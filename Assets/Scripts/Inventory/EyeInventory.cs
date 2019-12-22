using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EyeInventory : MonoBehaviour
{

    public Transform content;
    Transform item;


    UnityEngine.Object[] eyes = null;

    void Start()
    {
        item = content.GetChild(0);

        // look into asset bundle later


        getInventoryFromFolder();
    }

    private void getInventoryFromFolder()
    {

        float cellSize = 130;
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

              // sprite.GetComponent<Image>().sprite = spriteImage;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
