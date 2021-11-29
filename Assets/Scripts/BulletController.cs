using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    const string ENEMY = "Enemy";
    const string BOSS = "Boss";
    [SerializeField] float bulletSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject impactEffect;
    [SerializeField] int damageAmount = 1;
    public UnityEngine.Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = moveDirection * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ENEMY) 
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }
        if (other.tag == BOSS) 
        {
            other.GetComponent<BossHealthController>().TakeDamage(damageAmount);
        }
        if(impactEffect != null) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
