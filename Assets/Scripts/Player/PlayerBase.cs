using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    CameraShake cameraShake;
    float hp, speed, attackRate, attack, defense, recoveryRate;
    float pushBackDistance = 2f;
    int level = 1;

    float damage = 0;
    float changeTime = 3;

    public int Level { get => level; set => level = value; }
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AttackRate { get => attackRate; set => attackRate = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Defense { get => defense; set => defense = value; }
    public float RecoveryRate { get => recoveryRate; set => recoveryRate = value; }

    public GameObject headSpace, rightArmSpace, leftArmSpace;

    // Start is called before the first frame update
    protected void Start()
    {
        Hp = 10f;
        Speed = Attack = AttackRate = Defense = RecoveryRate = 1f;
        cameraShake = FindObjectOfType<CameraShake>();
    }

    internal void PickUp(GameObject gameObject)
    {
        //auto pickup for now
        gameObject.transform.position = headSpace.transform.position;
        gameObject.transform.parent = headSpace.transform;
    }

    // Update is called once per frame
    protected void Update()
    {
        Hp = Mathf.Lerp(Hp, Hp - damage, Time.deltaTime * changeTime);

        if (hp <= 0)
        {
            hp = 0;
            die();
        }
    }

    public void takeDamage(float damage)
    {
        hp -= damage;
        cameraShake.Shake();
    }

    public void die()
    {
        Debug.Log("im dead damit!");
        Destroy(gameObject);
    }


}
