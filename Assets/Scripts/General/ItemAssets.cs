using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
  public static ItemAssets IS { get; set; }

    private void Awake()
    {
        IS = this;
    }

    public Sprite eyes, rightHand;
}
