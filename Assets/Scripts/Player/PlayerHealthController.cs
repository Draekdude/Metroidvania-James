using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerHealthController : MonoBehaviour
{

    // [HideInInspector]
    [SerializeField] float invincibilityTime;
    [SerializeField] float flashTime;
    [SerializeField] SpriteRenderer[] playerSprites;
    [SerializeField] GameObject deathEffect;
    // PlayerHealthController playerHealthController;
    RespawnController respawnController;
    UIController uIController;
    float invincibilityCounter = 0f;
    float flashCounter = 0f;

    bool isSpawned = false;
    PlayerAbilityTracker abilities;

    void Awake()
    {
        if(!isSpawned){
            //DontDestroyOnLoad(gameObject);
            isSpawned = true;
        } 
        else 
        {
            Destroy(gameObject);
        }
        abilities = GetComponent<PlayerAbilityTracker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        uIController = FindObjectOfType<UIController>();
        respawnController = FindObjectOfType<RespawnController>();
        uIController.UpdateHealth(abilities.GetCurrentHealth(), abilities.GetMaxHealth());
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
        abilities.SetCurrentHealth(abilities.GetCurrentHealth() - damageAmount);
        uIController.UpdateHealth(abilities.GetCurrentHealth(), abilities.GetMaxHealth());
        if (IsDead())
        {
            OnDie();
            return;
        }
        invincibilityCounter = invincibilityTime;
    }

    public void FillHealth()
    {
        abilities.SetCurrentHealth(abilities.GetMaxHealth());
        uIController.UpdateHealth(PlayerStats.currentHealth, PlayerStats.maxHealth);
    }

    private void OnDie()
    {
        PlayerStats.currentHealth = 0;
        if (deathEffect != null) Instantiate(deathEffect, transform.position, transform.rotation);
        respawnController.Respawn();
    }

    private bool IsDead()
    {
        return abilities.GetCurrentHealth() <= 0;
    }
}
