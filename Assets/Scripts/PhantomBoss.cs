using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomBoss : MonoBehaviour
{
    const string VANISH_ANIMATION = "vanish";
    [SerializeField] Transform camPosition;
    [SerializeField] float camSpeed;
    [SerializeField] int threshold1;
    [SerializeField] int threshold2;
    [SerializeField] float activeTime;
    [SerializeField] float fadeOutTime;
    [SerializeField] float inactiveTime;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Transform boss;
    Transform targetPoint;
    
    float activeCounter;
    float fadeCounter;
    float inactiveCounter;

    CameraController cameraController;


    BossHealthController bossHealthController;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        cameraController.enabled = false;
        activeCounter = activeTime;
        bossHealthController = FindObjectOfType<BossHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, camPosition.transform.position, camSpeed * Time.deltaTime);
        if (bossHealthController.currentHealth > threshold1) 
        {
            if (activeCounter > 0)
            {
                activeCounter -= Time.deltaTime;
                if (activeCounter <= 0)
                {
                    fadeCounter = fadeOutTime;
                    animator.SetTrigger(VANISH_ANIMATION);
                }
            }
            else if(fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;
                if(fadeCounter <= 0)
                {
                    boss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter > 0)
            {
                inactiveCounter -= Time.deltaTime;
                if (inactiveCounter <= 0)
                {
                    int newSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
                    boss.position = spawnPoints[newSpawnPoint].position;
                    boss.gameObject.SetActive(true);
                    activeCounter = activeTime;
                }
            }
        }
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
