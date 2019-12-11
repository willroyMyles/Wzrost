using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float speed = 10f;
    float pushBackDistance = 2f;

    bool canMove = true;
    bool pushPlayerBack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            var hori = Input.GetAxis("Horizontal");
            var verti = Input.GetAxis("Vertical");

            var vec = new Vector3(hori, 0f, verti);
            vec = vec * speed * Time.deltaTime;
            if (vec != Vector3.zero) transform.forward = -vec;

            //vec.y = -10 * Time.deltaTime;
            GetComponent<CharacterController>().Move(vec);

            if (pushPlayerBack)
            {
                transform.position = transform.position - -transform.forward * pushBackDistance;
                pushPlayerBack = false;
            }

        }
    }

    public void PushPlayerBack(float blowBack)
    {
        pushBackDistance = blowBack;
        pushPlayerBack = true;
    }
}
