using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField] bool unlockDoubleJump;
    [SerializeField] bool unlockDash;
    [SerializeField] bool unlockBecomeBall;
    [SerializeField] bool unlockDropBomb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if(unlockDoubleJump)
            {
                player.SetCanDoubleJump(true);
            }

            if(unlockDash)
            {
                player.SetCanDash(true);
            }

            if(unlockBecomeBall)
            {
                player.SetCanBecomeBall(true);
            }

            if(unlockDropBomb)
            {
                player.SetCanDropBomb(true);
            }

            Destroy(gameObject);
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
