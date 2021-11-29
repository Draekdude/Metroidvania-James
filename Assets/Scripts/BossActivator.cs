using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject bossToActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            bossToActivate.SetActive(true);
            gameObject.SetActive(false); 
        }
    }
}
