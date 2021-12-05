using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    void Awake()
    {
        if(AudioManager.instance == null) {
            AudioManager newAudioManager = Instantiate(audioManager);
            AudioManager.instance = newAudioManager;
            DontDestroyOnLoad(newAudioManager.gameObject);
        }
    }
}
