using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    // Start is called before the first frame update

    float lifeTime = 3f;
    float currentTime = 0f;
    float bulletSpeed = 10f;
    float speedincrease = 1.3f;
    float speedDecrease = 1.3f;
    Vector3 velocity;

    float damage = 1f;
    float damageFallOff;
    internal float damageFallOffAmount = 2;
    float stunOnHit = .15f;
    float blowBack = 2f;
    string whoDoIBelongTo;

    bool isDefelectable = true;

    public float BlowBack { get => blowBack; set => blowBack = value; }
    public float StunOnHit { get => stunOnHit; set => stunOnHit = value; }
    public float Damage { get => damage; set => damage = value; }
    private void Awake()
    {
        damageFallOff = damage / damageFallOffAmount;
    }
    void Start()
    {
        

    }

    public void setUpBall(Vector3 dir, string whoIBelongTo)
    {
        whoDoIBelongTo = whoIBelongTo;
        velocity = dir * bulletSpeed;
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


        if (collision.gameObject.tag == "Interactor" )
        {
            var eb = collision.gameObject.GetComponent<PlayerBase>();
            eb.takeDamage(damage);
          //  if (whoDoIBelongTo == "Player") eb.cameraShake.Shake();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "artillary")
        {
            damage -= damageFallOff;
            velocity = gameObject.transform.forward * bulletSpeed * speedincrease;
            GetComponent<Rigidbody>().velocity = velocity;
            Destroy(gameObject);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(isDefelectable)
        {
           
        }
        
    }
}
