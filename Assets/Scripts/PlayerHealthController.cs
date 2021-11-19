using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    // [HideInInspector]
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    UIController uIController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        uIController = FindObjectOfType<UIController>();
        uIController.UpdateHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
        uIController.UpdateHealth(currentHealth, maxHealth);
    }
}
