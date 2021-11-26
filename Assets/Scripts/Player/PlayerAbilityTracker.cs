using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    [SerializeField] bool canDoubleJump;
    [SerializeField] bool canDash;
    [SerializeField] bool canBecomeBall;
    [SerializeField] bool canDropBomb;
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] Transform spawnPoint;

    void Awake()
    {
        if (!PlayerStats.isInitialized)
        {
            InitializePlayerStats();
        }
    }

    private void InitializePlayerStats()
    {
        PlayerStats.canDoubleJump = canDoubleJump;
        PlayerStats.canDash = canDash;
        PlayerStats.canBecomeBall = canBecomeBall;
        PlayerStats.canDropBomb = canDropBomb;
        PlayerStats.currentHealth = currentHealth;
        PlayerStats.maxHealth = maxHealth;
        Vector3 spawn = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        PlayerStats.spawnPoint = spawn;
        PlayerStats.isInitialized = true;
    }

    public bool GetCanDoubleJump()
    {
        return PlayerStats.canBecomeBall;
    }

    public void SetCanDoubleJump(bool value)
    {
        PlayerStats.canDoubleJump = value;
    }
    
    public bool GetCanDash()
    {
        return PlayerStats.canDash;
    }

    public void SetCanDash(bool value)
    {
        PlayerStats.canDash = value;
    }

    public bool GetCanBecomeBall()
    {
        return PlayerStats.canBecomeBall;
    }
    public void SetCanBecomeBall(bool value)
    {
        PlayerStats.canBecomeBall = value;
    }

    public bool GetCanDropBomb()
    {
        return PlayerStats.canDropBomb;
    }

    public void SetCanDropBomb(bool value)
    {
        PlayerStats.canDropBomb = value;
    }

    public int GetCurrentHealth()
    {
        return PlayerStats.currentHealth;
    }

    public void SetCurrentHealth(int value)
    {
        PlayerStats.currentHealth = value;
    }
    public int GetMaxHealth()
    {
        return PlayerStats.maxHealth;
    }

    public void SetMaxHealth(int value)
    {
        PlayerStats.maxHealth = value;
    }

    public Vector3 GetSpawnPoint()
    {
        return PlayerStats.spawnPoint;
    }

    public void SetSpawnPoint(Vector3 value)
    {
        Vector3 newLocation = new Vector3(value.x, value.y, value.z);
        PlayerStats.spawnPoint = newLocation;
    }
}
