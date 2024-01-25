using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip hit, dmg;

    public static SoundEffect instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void PlaySFX_Hit()
    {
        sfx.PlayOneShot(hit);
    }
    public void PlaySFX_DMG()
    {
        sfx.PlayOneShot(dmg);
    }
    public void SetSFXVolume(float volume)
    {
        sfx.volume = volume;
    }
}
