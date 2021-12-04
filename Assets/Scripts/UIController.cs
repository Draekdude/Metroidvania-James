using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{

    // static private UIController uIController;
    [SerializeField] GameObject prefab;
    [SerializeField] Slider healthSlider;
    [SerializeField] Image fadeScreen;
    [SerializeField] float fadeSpeed = 2f;
    [SerializeField] string mainMenuScene;
    [SerializeField] GameObject pauseScreen;

    const float BLACK = 1f;
    const float WHITE = 0f;
    bool fadeToBlack;
    bool fadeFromBlack;
    // static bool hasSpawned = false;

    // private void Awake()
    // {
    //     if(uIController != null) return;

    // }

    // private void SpawnPersistentObjects()
    // {
    //     GameObject persistentObject = Instantiate(prefab);
    //     DontDestroyOnLoad(persistentObject);
    // }
    // Start is called before the first frame update
    void Start()
    {
        fadeScreen.color = SetTransperency(BLACK);
        fadeFromBlack = true;
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

    private Color SetTransperency(float value)
    {
        return new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, value);
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

    public void PauseUnpause()
    {
        if(!pauseScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
        } 
        else 
        {
            Time.timeScale = 1;
        }
        pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnPauseUnpause()
    {
        PauseUnpause();
        print("Pause");
    }

    public void OnGoToMainMenu()
    {

    }


}
