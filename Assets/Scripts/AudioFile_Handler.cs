using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFile_Handler : MonoBehaviour
{    
    public AudioSource mainBGM, bgmBoss;
    //public AudioSource sfx;
    //public AudioClip hit, dmg;

    public static AudioFile_Handler instance;
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
    private void Start()
    {
        PlayBGM_Utama(true);
    }
    public void PlayBGM_Utama(bool isPlay)
    {
        if (isPlay)
        {
            mainBGM.Play();
        }
        else
        {
            mainBGM.Stop();
        }
    }
    public void PlayBGM_Boss(bool isPlay)
    {
        if (isPlay)
        {
            bgmBoss.Play();
        }
        else
        {
            bgmBoss.Stop();
        }
    }
    //public void PlaySFX_Hit()
    //{
    //    sfx.PlayOneShot(hit);
    //}
    //public void PlaySFX_DMG()
    //{
    //    sfx.PlayOneShot(dmg);
    //}
    //public void SetSFXVolume(float volume)
    //{
    //    sfx.volume = volume;
    //}

}
