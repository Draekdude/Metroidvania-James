using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    RespawnController respawnController;
    PlayerAbilityTracker abilities;

    void Start()
    {
        abilities = FindObjectOfType<PlayerAbilityTracker>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            print(transform.position.x);
            abilities.SetSpawnPoint(transform.position);
        }
    }
}
