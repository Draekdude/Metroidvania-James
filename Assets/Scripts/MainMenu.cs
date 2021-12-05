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
    }

    public void Continue()
    {
        print(PlayerPrefs.GetString(PlayerPrefNames.CONTINUE_LEVEL));
        SceneManager.LoadScene(PlayerPrefs.GetString(PlayerPrefNames.CONTINUE_LEVEL));
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Game quit");
    }
}
