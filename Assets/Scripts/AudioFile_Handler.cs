using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class AudioFile_Handler : MonoBehaviour
{    
    //public AudioSource mainBGM, bgmBoss;
    [SerializeField]private AudioSource bgmAudioSource;
    public AudioClip bgm_Main, bgm_Boss, bgm_win, bgm_lose;

    //[SerializeField] private DemoLoadScene transitionSettings;

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
        bgmAudioSource = GetComponent<AudioSource>();
        PlayBGM_Main(true); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayBGM_Main(false);
            PlayBGM_Boss(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayBGM_Boss(false);
            PlayBGM_Main(true);            
        }
    }

    public void PlayBGM_Main(bool isPlay)
    {
        if (isPlay)
        {
            //mainBGM.Play();
            Debug.Log("Play Main");
            PlayBGM_Boss(false); PlayBGM_Lose(false); PlayBGM_Win(false);
            bgmAudioSource.clip = bgm_Main; bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
        else
        {
            //mainBGM.Stop();
            bgmAudioSource.Stop();
        }
    }
    public void PlayBGM_Boss(bool isPlay)
    {
        if (isPlay)
        {
            //bgmBoss.Play();
            PlayBGM_Main(false);
            bgmAudioSource.clip = bgm_Boss;
            bgmAudioSource.Play();
        }
        else
        {
            //bgmBoss.Stop();
            bgmAudioSource.Stop();
        }
    }

    public void PlayBGM_Win(bool isPlay)
    {
        if (isPlay)
        {
            bgmAudioSource.clip = bgm_win; bgmAudioSource.loop = false;
            bgmAudioSource.Play();
            
        }
        else
        {
            bgmAudioSource.Stop();
            bgmAudioSource.loop = true;
            //PlayBGM_Main(true);
        }
    }
    public void PlayBGM_Lose(bool isPlay)
    {
        if (isPlay)
        {
            Debug.Log("Play Lose");
            bgmAudioSource.clip = bgm_lose; bgmAudioSource.loop = false;
            bgmAudioSource.Play();
        }
        else
        {
            bgmAudioSource.Stop();
            bgmAudioSource.loop = true;
            //PlayBGM_Main(true);
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
