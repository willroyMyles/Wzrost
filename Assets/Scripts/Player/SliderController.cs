using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider healthSlider, coolDownSlider;
    public Image healthImage, coolDownImage;

    bool useRelativerotation = false ;

    private Quaternion relativeRotation;
    private FireController fireController;
    private PlayerBase playerBase;

    Color low = new Color(.85f, 0, 0, 1f);
    Color cooldownColor = new Color(0, .85f, 0, 1f);


    void Start()
    {
        setSliderColors();
        relativeRotation = healthSlider.transform.localRotation;

        fireController = GetComponent<FireController>();
        playerBase = GetComponent<PlayerBase>();

        coolDownSlider.maxValue = fireController.FireRate;
        healthSlider.maxValue = playerBase.Hp;
        healthSlider.value = healthSlider.maxValue;

        healthSlider.transform.rotation = relativeRotation;
        coolDownSlider.transform.rotation = relativeRotation;
    }

    private void setSliderColors()
    {
        coolDownImage.color = Color.Lerp(low, cooldownColor, coolDownSlider.value / coolDownSlider.maxValue);
        healthImage.color = Color.Lerp(low, cooldownColor, healthSlider.value / healthSlider.maxValue);

    }

    private void LateUpdate()
    {
        return;
        if (fireController.getCanFire())    updateCoolDownSlider(fireController.CoolDownTime);
        if(playerBase.ShouldUpdateHealthCanvas())   updateHealthSlider(playerBase.Hp);
       
    }



    #region external
    private void updateHealthSlider(float hp)
    {
        if (healthSlider.value == hp) return;
        healthSlider.value = hp;
        setSliderColors();
    }
    public void updateCoolDownSlider(float val)
    {
        if (coolDownSlider.value == val) return;
        coolDownSlider.value = val;
        setSliderColors();
    }

    #endregion
}
