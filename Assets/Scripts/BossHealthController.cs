using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    [SerializeField] Slider bossHealthSlider;
    [SerializeField] int currentHealth = 30;
    [SerializeField] PhantomBoss boss;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            boss.EndBattle();
            return;
        }
        bossHealthSlider.value = currentHealth;
    }
}
