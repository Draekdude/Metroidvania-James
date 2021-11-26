using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    [SerializeField] float waitToRespawn;

    GameObject player;
    PlayerHealthController playerHealthController;
    PlayerAbilityTracker abilities;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerHealthController = FindObjectOfType<PlayerHealthController>();
        abilities = FindObjectOfType<PlayerAbilityTracker>();
        player.transform.position = abilities.GetSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        DontDestroyOnLoad(gameObject);
        player.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        print("Died at: " + abilities.GetSpawnPoint().x);
        playerHealthController.FillHealth();
        Destroy(gameObject);
    }
}
