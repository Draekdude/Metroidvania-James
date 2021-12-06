using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(PlayerPrefNames.CONTINUE_LEVEL)) continueButton.SetActive(true);
        AudioManager.instance.PlayMainMenuMusic();
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(newGameScene);
        PlayerStats.isInitialized = false;
    }

    public void Continue()
    {
        if(PlayerPrefs.HasKey(PlayerPrefNames.DOUBLEJUMPUNLOCKED) && PlayerPrefs.GetInt(PlayerPrefNames.DOUBLEJUMPUNLOCKED) == 1)
        {
            PlayerStats.canDoubleJump = true;
        }
        if(PlayerPrefs.HasKey(PlayerPrefNames.DASHUNLOCKED) && PlayerPrefs.GetInt(PlayerPrefNames.DASHUNLOCKED) == 1)
        {
            PlayerStats.canDash = true;
        }
        if(PlayerPrefs.HasKey(PlayerPrefNames.BALLUNLOCKED) && PlayerPrefs.GetInt(PlayerPrefNames.BALLUNLOCKED) == 1)
        {
            PlayerStats.canBecomeBall = true;
        }
        if(PlayerPrefs.HasKey(PlayerPrefNames.BOMBUNLOCKED) && PlayerPrefs.GetInt(PlayerPrefNames.BOMBUNLOCKED) == 1)
        {
            PlayerStats.canDropBomb = true;
        }
        PlayerStats.isInitialized  = true;
        SceneManager.LoadScene(PlayerPrefs.GetString(PlayerPrefNames.CONTINUE_LEVEL));
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Game quit");
    }
}
