using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWeapon : InteractableBase
{
    private void Awake()
    {
        type = InteractableType.Attack;
        name = "Gatlin Gun";

        if(GetComponent<Rigidbody>() == null)
        {
            //attatch rigidbody
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        if(GetComponent<SphereCollider>() == null)
        {
            var sc = gameObject.AddComponent<SphereCollider>();
            sc.transform.position = Vector3.zero;
            sc.radius = .25f;
        }
    }
}
