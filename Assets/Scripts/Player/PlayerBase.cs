using Assets.Scripts.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBase : MonoBehaviour
{

    CameraShake cameraShake;


    float damage = 0;
    float changeTime = 3;

    protected BaseStats baseStats = new BaseStats();
    public float Level { get => baseStats.level; set => baseStats.level = value; }
    public float Hp { get => baseStats.hp; set => baseStats.hp = value; }
    public float Speed { get => baseStats.speed; set => baseStats.speed = value; }
    public float AttackRate { get => baseStats.attackRate; set => baseStats.attackRate = value; }
    public float Attack { get => baseStats.attack; set => baseStats.attack = value; }
    public float Defense { get => baseStats.defense; set => baseStats.defense = value; }
    public float RecoveryRate { get => baseStats.recoveryRate; set => baseStats.recoveryRate = value; }

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
        var interScript = gameObject.GetComponent<InteractableBase>();

        if (interScript.type == InteractableType.Flag)
        {
            gameObject.transform.position = headSpace.transform.position;
            gameObject.transform.parent = headSpace.transform;
        }
        if (interScript.type == InteractableType.Attack)
        {
            gameObject.transform.position = rightArmSpace.transform.position;
            gameObject.transform.parent = rightArmSpace.transform;

        }
        if (interScript.type == InteractableType.Defense)
        {
            gameObject.transform.position = leftArmSpace.transform.position;
            gameObject.transform.parent = leftArmSpace.transform;
        }

        interScript.setIsPickedUp(true);
        

    }

    // Update is called once per frame
    protected void Update()
    {
        Hp = Mathf.Lerp(Hp, Hp - damage, Time.deltaTime * changeTime);

        if (Hp <= 0)
        {
            Hp = 0;
            die();
        }
    }

    public void takeDamage(float damage)
    {
        Hp -= damage;
        cameraShake.Shake();
    }

    public void die()
    {
        Debug.Log("im dead damit!");
        Destroy(gameObject);
    }


}
