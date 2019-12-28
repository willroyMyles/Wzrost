using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class EnemyBase : PlayerBase
{
    // Start is called before the first frame update

    Canvas canvas;
    CanvasGroup group;
    Image image;
    TMPro.TextMeshProUGUI text;
    float timeCanvasShown = 2f;
    float maxShownTime = 2f;

    bool shouldShowCanves = false;
    

    new void  Start()  
    {
        //Start();
        base.Start();

        var go = transform.parent;
        //canvas = GetComponentInChildren<Canvas>();
        //if (canvas.worldCamera == null)
        //{
        //    canvas.worldCamera = Camera.allCameras[0];
        //}
        //image = GetComponentInChildren<Image>();
        //group = canvas.GetComponent<CanvasGroup>();
        //text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        //text.text = baseStats.hp.ToString();
    }

    public void StunEnemy(float stunTime)
    {
        //StartCoroutine(GetComponent<EnemyController>().stunPlayer(stunTime));
    }

    public void takeDamage(float damage, float stunOnHit)
    {
        base.takeDamage(damage);
        StunEnemy(stunOnHit);
        //text.text = ((int)(baseStats.hp * 10)).ToString() + "%";
        //shouldShowCanves = true;
    }

    private void updateImagePositionAndAlpha()
    {
        if (shouldShowCanves)
        {
            //updates alpha
            group.alpha = Mathf.Lerp(0, 1, timeCanvasShown / maxShownTime);

            //update position
            var screenpos = Camera.allCameras[0].WorldToScreenPoint(gameObject.transform.position); 
            var point = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenpos, Camera.allCameras[0], out point);
            image.transform.position = canvas.transform.TransformPoint(point) ;
            timeCanvasShown -= Time.deltaTime;

            if (timeCanvasShown <= 0)
            {
                shouldShowCanves = false;
                timeCanvasShown = maxShownTime;
            }
        }
    }

    private new void Update()
    {
        base.Update();
        //updateImagePositionAndAlpha();
    }


}
