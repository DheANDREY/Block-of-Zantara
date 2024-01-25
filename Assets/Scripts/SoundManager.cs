using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeBGMSlider;
    public Slider volumeSFXSlider;

    private AudioSource sfx;
    private AudioSource bgm;
    private void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        bgm = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();

        //ChangeVolumeBGM();  ChangeVolumeSFX();
        if (!PlayerPrefs.HasKey("bgmVolume") || !PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("bgmVolume", 0.5f);
            Debug.Log("Load");
            //PlayerPrefs.SetFloat("sfxVolume", 0.5f);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolumeBGM()
    {
        float newVolume = volumeBGMSlider.value;
        bgm.volume = volumeBGMSlider.value;
        Save();
    }

    public void ChangeVolumeSFX()
    {
        float newVolume = volumeSFXSlider.value;
        //sfx.SetSFXVolume(newVolume);
        sfx.volume = volumeSFXSlider.value;
        Save();
    }
    public void Load()
    {
        volumeBGMSlider.value = PlayerPrefs.GetFloat("bgmVolume");   
        volumeSFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        //sfx.SetSFXVolume(volumeSFXSlider.value);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("bgmVolume", volumeBGMSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", volumeSFXSlider.value);
    }
}
