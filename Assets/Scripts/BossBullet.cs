using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int damageAmount;
    [SerializeField] GameObject impactEffect;
    PlayerHealthController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealthController>();
        SetAttackRotation(player.transform);
        AudioManager.instance.PlaySoundEffectAdjusted(SoundName.BOSS_SHOT);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    private void SetAttackRotation(Transform target)
    {
        Vector3 direction = transform.position - target.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.DamagePlayer(damageAmount);
        }
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        AudioManager.instance.PlaySoundEffectAdjusted(SoundName.BULLET_IMPACT);
    }
}
