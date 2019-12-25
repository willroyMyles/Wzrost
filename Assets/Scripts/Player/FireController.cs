using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject bulletPrefab;

    float fireRate = .5f;
    float nextFire = 0;
    float coolDownTime = 0;
    float spawnDistance = 1f;
    bool fire = false;

    Vector3 finalPosition;

    public float NextFire { get => nextFire; set => nextFire = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float CoolDownTime { get => coolDownTime; set => coolDownTime = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTime < fireRate)
        {
            coolDownTime += Time.deltaTime;
        }
        else
        {
            coolDownTime = fireRate;
        }

        if (fire)
        {

            coolDownTime = 0;
            fire = false;
        }
       // if (Input.GetMouseButtonDown(0)) Fire();
    }

    public void Fire()
    {
        if(CoolDownTime == fireRate)
        {
            fire = true;
            var spawnPos = gameObject.transform.position + gameObject.transform.forward * spawnDistance;
            var bullet = Instantiate(bulletPrefab, spawnPos, gameObject.transform.rotation);
            bullet.GetComponent<BulletBase>().setUpBall(transform.forward, gameObject.tag);

            //push player back
            var pm = GetComponent<GeneralMovement>();
            float bb = bullet.GetComponent<BulletBase>().BlowBack;
            pm.PushPlayerBack(bb);
        }
    }
}
