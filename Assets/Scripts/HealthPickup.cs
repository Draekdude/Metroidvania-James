using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount;
    [SerializeField] GameObject healEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController playerHealthController = other.GetComponentInParent<PlayerHealthController>();
            playerHealthController.HealPlayer(healAmount);
            if(healEffect != null)
            {
                Instantiate(healEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            AudioManager.instance.PlaySoundEffect(SoundName.PICKUP);
        }
    }
}
