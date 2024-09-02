using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    Rigidbody Rigidbody;
    public float dmg;
    float remainTime = 10f;
    float curTime;
    Shot shot;

    private void Start()
    {
        Rigidbody.velocity = transform.forward * bulletSpeed;
    }
    private void OnEnable()
    {
        curTime = 10f;
        Rigidbody = gameObject.GetComponent<Rigidbody>();
        
    }
    

    private void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime < 0)
        {
            ObjectPool.ReturnObject(this);
            curTime = remainTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.collider.gameObject.GetComponent<Monster>();
        monster.HitDmg(dmg);
        ObjectPool.ReturnObject(this);
    }
}
