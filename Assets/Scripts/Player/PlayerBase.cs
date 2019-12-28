using Assets.Scripts.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBase : MonoBehaviour
{

    internal CameraShake cameraShake;
    [SerializeField] public Modifications mod;

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
    internal bool attackEquipped = false;

    internal TeamNumber GetTeamNumber()
    {
        return teamNumber;
    }

    internal bool defenseEquipped = false;
    internal bool flagEquipped = false;
    internal bool isOnTeam = false; // have spawn controller deal with this for now
    internal bool shouldUpdateHealthSlider = false;

    internal TeamNumber teamNumber;

    // Start is called before the first frame update
    protected void Start()
    {
        
        Hp = 10f;
        Speed = Attack = AttackRate = Defense = RecoveryRate = 1f;
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void assignTeamNumber(TeamNumber num)
    {
        teamNumber = num;
        Global.Instance().myTeamNumber = num;
        isOnTeam = true;
        //change color based on number

        changeBandanaColor(num);
    }

    public void assignEnemeyTeamNumber(TeamNumber num)
    {
        isOnTeam = false;
        changeBandanaColor(num);
    }

    private void changeBandanaColor(TeamNumber num)
    {
        var bandanas = GetComponentsInChildren<Transform>();
        GameObject bandana = null;
        foreach (var child in bandanas)
        {
            if (child.CompareTag("Bandana"))
            {
                bandana = child.gameObject;
                break;
            }
        }

        if(bandana != null)
        switch (num)
        {
                case TeamNumber.Default:
                    bandana.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);
                    break;
                case TeamNumber.One:
                    bandana.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
                    break;
                case TeamNumber.Two:
                    bandana.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                    break;
                case TeamNumber.Three:
                    bandana.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
                    break;
                case TeamNumber.Four:
                    bandana.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
                    break;
        }
    }

    internal void PickUp(GameObject gameObject)
    {
        //auto pickup for now
        var interScript = gameObject.GetComponent<InteractableBase>();

        if (interScript.type == InteractableType.Flag)
        {
            gameObject.transform.position = headSpace.transform.position;
            gameObject.transform.parent = headSpace.transform;
            Global.Instance().playerFlagEquipped = true;
        }
        if (interScript.type == InteractableType.Attack)
        {
            gameObject.transform.position = rightArmSpace.transform.position;
            gameObject.transform.parent = rightArmSpace.transform;
            Global.Instance().playerAttackEquipped = true;

        }
        if (interScript.type == InteractableType.Defense)
        {
            gameObject.transform.position = leftArmSpace.transform.position;
            gameObject.transform.parent = leftArmSpace.transform;
            Global.Instance().playerDefenseEquipped = true;
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
        shouldUpdateHealthSlider = true;
        //cameraShake.Shake();
    }

    /// <summary>
    /// If is true, it set the value to false and return true, otherwise returns false
    /// .This toggles the value only if it is true
    /// </summary>
    /// <returns>bool shouldUpdateHealthSlider</returns>
    public bool ShouldUpdateHealthCanvas()
    {
        if (shouldUpdateHealthSlider)
        {
            shouldUpdateHealthSlider = !shouldUpdateHealthSlider;
            return !shouldUpdateHealthSlider;
        }
        else return false;
    }

    public void die()
    {
        if (Global.Instance().opponentsWithinSphere.Contains(gameObject)) Global.Instance().opponentsWithinSphere.Remove(gameObject);
        Destroy(gameObject);
        this.enabled = false;
    }


}
