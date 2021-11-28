using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Image fadeScreen;
    [SerializeField] float fadeSpeed = 2f;

    const float BLACK = 1f;
    const float WHITE = 0f;
    bool fadeToBlack;
    bool fadeFromBlack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack)
        {
            FadeImage(BLACK);
            FadeCompleted(BLACK, fadeToBlack);
        }
        if (fadeFromBlack) 
        {
            FadeImage(WHITE);
            FadeCompleted(WHITE, fadeFromBlack);
        }
    }

    private void FadeCompleted(float value, bool fadeEvent)
    {
        if (fadeScreen.color.a == value)
        {
            fadeEvent = false;
        }
    }

    private void FadeImage(float newValue)
    {
        float a = Mathf.MoveTowards(fadeScreen.color.a, newValue, fadeSpeed * Time.deltaTime);
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, a);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth > 0 ? currentHealth : 0;
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadeToBlack = false;
        fadeFromBlack = true;
    }
}
