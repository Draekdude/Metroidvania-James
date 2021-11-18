using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    const string Player_Tag = "Player";
    PlayerHealthController playerHealthController;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Player_Tag)
        {
            playerHealthController = other.gameObject.GetComponentInParent<PlayerHealthController>();
            DealDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Player_Tag)
        {
            playerHealthController = other.GetComponentInParent<PlayerHealthController>();
            DealDamage();
        }
    }

    void DealDamage()
    {
        playerHealthController.DamagePlayer(damageAmount);
    }
}
