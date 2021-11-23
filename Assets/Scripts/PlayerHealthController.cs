using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerHealthController : MonoBehaviour
{

    // [HideInInspector]
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] float invincibilityTime;
    [SerializeField] float flashTime;
    [SerializeField] SpriteRenderer[] playerSprites;
    [SerializeField] GameObject deathEffect;

    RespawnController respawnController;
    UIController uIController;
    float invincibilityCounter = 0f;
    float flashCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        uIController = FindObjectOfType<UIController>();
        respawnController = FindObjectOfType<RespawnController>();
        uIController.UpdateHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInvincible())
        {
            invincibilityCounter -= Time.deltaTime;
            FlashSprites();
            if (!IsInvincible()) EnableAllSprites();
        }
    }

    private bool IsInvincible()
    {
        return invincibilityCounter > 0;
    }

    private void EnableAllSprites()
    {
        playerSprites.ToList().ForEach(s => s.enabled = true);
        flashCounter = 0;
    }

    private void FlashSprites()
    {
        flashCounter -= Time.deltaTime;
        if (flashCounter <= 0)
        {
            playerSprites.ToList().ForEach(s => s.enabled = !s.enabled);
            flashCounter = flashTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincibilityCounter > 0) return;
        currentHealth -= damageAmount;
        uIController.UpdateHealth(currentHealth, maxHealth);
        if (IsDead())
        {
            OnDie();
            return;
        }
        invincibilityCounter = invincibilityTime;
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        uIController.UpdateHealth(currentHealth, maxHealth);
    }

    private void OnDie()
    {
        currentHealth = 0;
        if (deathEffect != null) Instantiate(deathEffect, transform.position, transform.rotation);
        respawnController.Respawn();
    }

    private bool IsDead()
    {
        return currentHealth <= 0;
    }
}
