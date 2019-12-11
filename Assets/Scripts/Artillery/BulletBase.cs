using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    // Start is called before the first frame update

    float lifeTime = 3f;
    float currentTime = 0f;
    float bulletSpeed = 30f;
    float speedincrease = 1.3f;
    float speedDecrease = 1.3f;
    Vector3 velocity;

    float damage = 8f;
    float damageFallOff = 4f;
    float stunOnHit = .15f;
    float blowBack = 2f;

    bool isDefelectable = true;

    public float BlowBack { get => blowBack; set => blowBack = value; }
    public float StunOnHit { get => stunOnHit; set => stunOnHit = value; }
    public float Damage { get => damage; set => damage = value; }

    void Start()
    {
        velocity = -gameObject.transform.forward * bulletSpeed;
        velocity.y = 0;
        GetComponent<Rigidbody>().velocity = velocity;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lifeTime) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().takeDamage(damage, stunOnHit);
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBase>().takeDamage(damage);

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(isDefelectable)
        {
            damage -= damageFallOff;
            velocity = -gameObject.transform.forward * bulletSpeed * speedincrease;
        }
    }
}
