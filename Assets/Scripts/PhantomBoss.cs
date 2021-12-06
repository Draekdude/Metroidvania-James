using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimationNames;

public class PhantomBoss : MonoBehaviour
{
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
    [SerializeField] float timeBetweenShots1;
    [SerializeField] float timeBetweenShots2;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject winObjects;
    [SerializeField] GameObject hiddenCave;
    [SerializeField] string bossRef;
    bool battleEnded;
    float shotCounter;
    Transform targetPoint;
    
    float activeCounter;
    float fadeCounter;
    float inactiveCounter;
    float attackDistance = 0.02f;

    CameraController cameraController;


    BossHealthController bossHealthController;

    // Start is called before the first frame update
    void Start()
    {

        AudioManager.instance.PlayBossMusic();
        cameraController = FindObjectOfType<CameraController>();
        cameraController.enabled = false;
        activeCounter = activeTime;
        bossHealthController = FindObjectOfType<BossHealthController>();
        shotCounter = timeBetweenShots1;
    }

    // Update is called once per frame
    void Update()
    {
        cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, camPosition.transform.position, camSpeed * Time.deltaTime);
        if(battleEnded) {
            fadeCounter -= Time.deltaTime;
            if(fadeCounter < 0) {
                if(winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);
                }
                cameraController.enabled = true;
                gameObject.SetActive(false);
            }
            return;
        } 
        if (IsFirstAttackPhaseActive())
        {
            FirstAttackPhase();
        }
        else 
        {
            if(targetPoint == null)
            {
                targetPoint = boss;
                fadeCounter = fadeOutTime;
                animator.SetTrigger(VANISH_ANIMATION);
            } 
            else 
            {
                SecondAttackPhase();
            }
        }
    }

    private bool IsFirstAttackPhaseActive()
    {
        return bossHealthController.currentHealth > threshold1;
    }

    private bool ShouldMoveTowardsTargetPoint()
    {
        return Vector3.Distance(boss.position, targetPoint.position) > attackDistance;
    }

    private void MoveTowardsTargetPoint()
    {
        boss.position = Vector3.MoveTowards(boss.position, targetPoint.position, moveSpeed * Time.deltaTime);
    }

    private void FirstAttackPhase()
    {
        if (activeCounter > 0)
        {
            activeCounter -= Time.deltaTime;
            if (ShouldBossVanishBegin()) BossVanishBegin();
            shotCounter -= Time.deltaTime;
            if (ShouldBossShoot()) BossShoot();
        }
        else if (fadeCounter > 0)
        {
            fadeCounter -= Time.deltaTime;
            if(ShouldBossVanish()) BossVanish();
        }
        else if (inactiveCounter > 0)
        {
            inactiveCounter -= Time.deltaTime;
            if(ShouldBossSetNewSpawnPoint()) {
                SetNewSpawnPoint();
                shotCounter = timeBetweenShots1;
            }
        }
    }

    private bool ShouldBossShoot()
    {
        return shotCounter <= 0;
    }

    private void BossShoot()
    {
        shotCounter = GetShotCounter();
        Instantiate(bullet, shotPoint.position, Quaternion.identity);
    }

    private float GetShotCounter()
    {
        if (IsFirstAttackPhaseActive())
        {
            return timeBetweenShots1;
        }
        else 
        {
            return timeBetweenShots2;
        }
    }

    private void SecondAttackPhase()
    {
        if (ShouldMoveTowardsTargetPoint())
        {
            MoveTowardsTargetPoint();
            if (!ShouldMoveTowardsTargetPoint()) BossVanishBegin();
            shotCounter -= Time.deltaTime;
            if (ShouldBossShoot()) BossShoot();
        } 
        else if (fadeCounter > 0)
        {
            fadeCounter -= Time.deltaTime;
            if(ShouldBossVanish()) BossVanish();
        }
        else //if (inactiveCounter > 0) //commented this out so boss wouldn't just sit there
        {
            inactiveCounter -= Time.deltaTime;
            if(ShouldBossSetNewSpawnPoint()) SetNewSpawnPoint();
            shotCounter = GetShotCounter();
        }
    }

    private bool ShouldBossSetNewSpawnPoint()
    {
        return inactiveCounter <= 0;
    }

    private bool ShouldBossVanish()
    {
        return fadeCounter <= 0;
    }

    private void BossVanish()
    {
        boss.gameObject.SetActive(false);
        inactiveCounter = inactiveTime;
    }

    private Transform GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }

    private void SetNewSpawnPoint()
    {
        boss.position = GetRandomSpawnPoint().position;
        boss.gameObject.SetActive(true);
        if(IsFirstAttackPhaseActive()) {
            activeCounter = activeTime;
        } else {
            targetPoint = GetDifferentRandomSpawnPoint(boss);
        }
    }

    private Transform GetDifferentRandomSpawnPoint(Transform currentPoint)
    {
        Transform newPoint = GetRandomSpawnPoint();
        while (newPoint == currentPoint)
        {
            newPoint = GetRandomSpawnPoint();
        }
        return newPoint;
    }

    private bool ShouldBossVanishBegin()
    {
        return activeCounter <= 0;
    }

    private void BossVanishBegin()
    {
        fadeCounter = fadeOutTime;
        animator.SetTrigger(VANISH_ANIMATION);
    }

    public void EndBattle()
    {
        battleEnded = true;
        hiddenCave.SetActive(false);
        winObjects.SetActive(true);
        animator.SetTrigger(VANISH_ANIMATION);
        boss.GetComponent<Collider2D>().enabled = false;
        FindObjectsOfType<BossBullet>().ToList().ForEach(x=> Destroy(x.gameObject));
        AudioManager.instance.PlayLevelMusic();
        PlayerPrefs.SetInt(bossRef, 1);
    }
}
