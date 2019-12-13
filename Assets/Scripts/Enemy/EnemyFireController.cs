using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireController : MonoBehaviour
{
    public GameObject bulletPrefab;

    float fireRate = 2.5f;
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
        if (fire)
        {

            coolDownTime = 0;
            fire = false;
        }
        if (coolDownTime < fireRate)
        {
            coolDownTime += Time.deltaTime;
        }
        else
        {
            coolDownTime = fireRate;
        }
    }

    public void Fire()
    {
        if (CoolDownTime == fireRate)
        {
            fire = true;
            var spawnPos = gameObject.transform.position + transform.forward * spawnDistance;
            var bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            bullet.GetComponent<BulletBase>().setUpBall(transform.forward);

            //push player back
            //var pm = GetComponent<MovementWithNavMesh>();
            //float bb = bullet.GetComponent<BulletBase>().BlowBack;
            //pm.PushPlayerBack(bb);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(gameObject.transform.position + gameObject.transform.forward * spawnDistance, 1);
        Gizmos.color = Color.red;
    }
}
