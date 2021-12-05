using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] float timeToExplode = 0.5f;
    [SerializeField] GameObject explosion;
    [SerializeField] float blastRadius;
    [SerializeField] LayerMask whatIsDestructible;
    [SerializeField] int damageAmount;
    [SerializeField] LayerMask whatToDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if(timeToExplode <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
            DestroyDestructibleObjects();
            ApplyDamageToDamageableObjects();
            AudioManager.instance.PlaySoundEffectAdjusted(SoundName.ENEMY_EXPLODE);
        }
    }

    private void DestroyDestructibleObjects()
    {
        Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRadius, whatIsDestructible);
        foreach (var objectToRemove in objectsToRemove)
        {
            Destroy(objectToRemove.gameObject);
        }
    }

    private void ApplyDamageToDamageableObjects()
    {
        Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, blastRadius, whatToDamage);
        foreach (var objectToDamage in objectsToDamage)
        {
            EnemyHealthController enemyHealthController = objectToDamage.GetComponent<EnemyHealthController>();
            if(enemyHealthController != null) enemyHealthController.DamageEnemy(damageAmount);
        }
    }
}
