using System.Security.AccessControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource mainMenuMusic;
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource bossMusic;
    [SerializeField] AudioSource[] soundEffects;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayMainMenuMusic()
    {
        if(mainMenuMusic.isPlaying) return;
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if (levelMusic.isPlaying) return;
        mainMenuMusic.Stop();
        bossMusic.Stop();
        levelMusic.Play();
    }

    public void PlayBossMusic()
    {
        if (bossMusic.isPlaying) return;
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        AudioSource soundEffect = soundEffects.Where(s=> s.name == soundEffectName).FirstOrDefault();
        if (soundEffect != null)
        {
            soundEffect.Stop();
            soundEffect.Play();
        }
    }

    public void PlaySoundEffectAdjusted(string soundEffectName)
    {
        AudioSource soundEffect = soundEffects.Where(s=> s.name == soundEffectName).FirstOrDefault();
        if (soundEffect != null)
        {
            soundEffect.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            soundEffect.Play();
        }
    }

}
