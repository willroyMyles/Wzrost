using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchController : MonoBehaviour
{
    Canvas canvas;
    Transform btn, panel;
    void Start()
    {
        setUp();
    }

    public void setUpCanvas()
    {

        if (canvas == null) setUp();
        var cellSize = 250;
        int x = 0;

        foreach(var obj in Global.Instance().playersOnTeam) {
            var button = Instantiate(btn, panel).GetComponent<RectTransform>();
            button.gameObject.SetActive(true);
            button.anchoredPosition = new Vector2(x * cellSize, 0);
            var textInButton = button.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            textInButton.text = obj.name.Replace("1(Clone)", " " + x);
            var actualButton = button.gameObject.GetComponent<Button>();

            setUpButton(actualButton, obj);
            x++;
        }
    }

    private void setUp()
    {
        if (canvas) return;
        canvas = GetComponent<Canvas>();
        panel = canvas.transform.Find("Panel");
        btn = panel.Find("Button");
    }

    private void setUpButton(Button actualButton, GameObject obj)
    {
        actualButton.onClick.AddListener(delegate { Global.Instance().SwitchCurrentUser(obj);  });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
