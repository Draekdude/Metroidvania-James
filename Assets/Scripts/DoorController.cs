using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float distanceToOpen;
    [SerializeField] Transform exitPoint;
    [SerializeField] float movePlayerSpeed;
    [SerializeField] string levelToLoad;

    PlayerController player;
    PlayerAbilityTracker abilityTracker;
    UIController uiController;
    bool isOpen = false;
    bool playerExiting;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        abilityTracker = FindObjectOfType<PlayerAbilityTracker>();
        uiController = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distanceToOpen)
        {
            OpenDoor();
        } else {
            CloseDoor();
        }

        if(playerExiting)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, exitPoint.position, Time.deltaTime * movePlayerSpeed);
        }
    }

    void OpenDoor()
    {
        if(!isOpen)
        {
            animator.SetBool(AnimationNames.Door_Open_Animation, true);
            isOpen = true;
        }
    }

    void CloseDoor()
    {
        if(isOpen)
        {
            animator.SetBool(AnimationNames.Door_Open_Animation, false);
            isOpen = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == TagNames.Player_Tag)
        {
            other.GetComponentInParent<PlayerController>().canMove = false;
            if(playerExiting) return;
            StartCoroutine(UserDoorCo());
        }
    }

    IEnumerator UserDoorCo()
    {
        playerExiting = true;
        player.standAnimator.enabled = false;
        FindObjectOfType<UIController>().StartFadeToBlack();
        yield return new WaitForSeconds(1.5f);
        abilityTracker.SetSpawnPoint(exitPoint.position);
        player.canMove = true;
        player.standAnimator.enabled = true;
        FindObjectOfType<UIController>().StartFadeFromBlack();

        SetPlayerPrefs();

        SceneManager.LoadScene(levelToLoad);
    }

    private void SetPlayerPrefs()
    {
        PlayerPrefs.SetString(PlayerPrefNames.CONTINUE_LEVEL, levelToLoad);
        PlayerPrefs.SetFloat(PlayerPrefNames.POSITION_X, exitPoint.position.x);
        PlayerPrefs.SetFloat(PlayerPrefNames.POSITION_Y, exitPoint.position.y);
        PlayerPrefs.SetFloat(PlayerPrefNames.POSITION_Z, exitPoint.position.z);
    }
}
