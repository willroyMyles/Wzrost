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

    Color low = new Color(.85f, 0, 0, 1f);
    Color cooldownColor = new Color(0, .85f, 0, 1f);


    void Start()
    {
        setSliderColors();
        relativeRotation = healthSlider.transform.localRotation;
        coolDownSlider.maxValue = GetComponent<FireController>().FireRate;
        healthSlider.maxValue = GetComponent<PlayerBase>().Hp;
        healthSlider.value = healthSlider.maxValue;
    }

    private void setSliderColors()
    {
        coolDownImage.color = Color.Lerp(low, cooldownColor, coolDownSlider.value / coolDownSlider.maxValue);
        healthImage.color = Color.Lerp(low, cooldownColor, healthSlider.value / healthSlider.maxValue);

    }

    // Update is called once per frame
    void Update()
    {
        if (useRelativerotation)
        {
            //relative rotation for sliders
            healthSlider.transform.rotation = relativeRotation;
            coolDownSlider.transform.rotation = relativeRotation;
        }
    }

    private void LateUpdate()
    {
        updateCoolDownSlider(GetComponent<FireController>().CoolDownTime);
        updateHealthSlider(GetComponent<PlayerBase>().Hp);
        
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
