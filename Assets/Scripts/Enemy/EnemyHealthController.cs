using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] int totalHealth = 3;
    [SerializeField] GameObject deathEffect;

    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;
        if (totalHealth <= 0)
        {
            if (deathEffect != null) Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            AudioManager.instance.PlaySoundEffect(SoundNames.ENEMY_EXPLODE);
        }
    }
}
