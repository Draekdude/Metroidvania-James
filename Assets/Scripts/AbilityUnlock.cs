using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField] bool unlockDoubleJump;
    [SerializeField] bool unlockDash;
    [SerializeField] bool unlockBecomeBall;
    [SerializeField] bool unlockDropBomb;
    [SerializeField] GameObject pickupEffect;
    [SerializeField] string unlockMessage;
    [SerializeField] TMP_Text unlockText;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
            {
                player.SetCanDoubleJump(true);
            }

            if (unlockDash)
            {
                player.SetCanDash(true);
            }

            if (unlockBecomeBall)
            {
                player.SetCanBecomeBall(true);
            }

            if (unlockDropBomb)
            {
                player.SetCanDropBomb(true);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);
            SetUnlockText();
            Destroy(gameObject);
        }
    }

    private void SetUnlockText()
    {
        Transform parent = unlockText.transform.parent;
        parent.SetParent(null);
        parent.position = transform.position;
        unlockText.text = unlockMessage;
        unlockText.gameObject.SetActive(true);
        Destroy(parent.gameObject, 3f);
        AudioManager.instance.PlaySoundEffect(SoundName.PICKUP);
    }

}
