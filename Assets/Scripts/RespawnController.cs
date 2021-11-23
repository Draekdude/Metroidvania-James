using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    [SerializeField] float waitToRespawn;

    GameObject player;
    PlayerHealthController playerHealthController;
    Vector3 respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerHealthController = FindObjectOfType<PlayerHealthController>();
        respawnPoint = player.transform.position;
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
        player.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        player.transform.position = respawnPoint;
        player.SetActive(true);
        playerHealthController.FillHealth();
    }
}
