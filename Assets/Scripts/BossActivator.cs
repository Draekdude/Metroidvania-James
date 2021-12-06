using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject bossToActivate;
    [SerializeField] string bossRef;

    void Start()
    {
        bossToActivate.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(PlayerPrefs.HasKey(bossRef) && PlayerPrefs.GetInt(bossRef) == 1) return;
            bossToActivate.SetActive(true);
            gameObject.SetActive(false); 
        }
    }
}
