using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string newGameScene;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Game quit");
    }
}
