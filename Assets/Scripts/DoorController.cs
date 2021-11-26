using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float distanceToOpen;

    const string Door_Open_Animation = "isOpen";

    PlayerController player;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
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
    }

    void OpenDoor()
    {
        if(!isOpen)
        {
            animator.SetBool(Door_Open_Animation, true);
            isOpen = true;
        }
    }

    void CloseDoor()
    {
        if(isOpen)
        {
            animator.SetBool(Door_Open_Animation, false);
            isOpen = false;
        }
    }
}
