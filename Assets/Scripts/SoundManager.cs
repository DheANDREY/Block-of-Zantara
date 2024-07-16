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

    private SoundEffect sfxE;

    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        bgm = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        sfxE = GameObject.FindWithTag("SFX").GetComponent<SoundEffect>();

        //ChangeVolumeBGM(); ChangeVolumeSFX();
        //if (!PlayerPrefs.HasKey("bgmVolume") || !PlayerPrefs.HasKey("sfxVolume"))
        //{
        //    PlayerPrefs.SetFloat("bgmVolume", 0.5f);
        //    Debug.Log("Load");
        //    PlayerPrefs.SetFloat("sfxVolume", 0.5f);
        //    Load();
        //}
        //else
        //{
        //    Load();
        //}
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Reset Volume");
            float a = 0;
            volumeBGMSlider.value = 0;
            volumeSFXSlider.value = 0;
            sfx.volume = 0f;
            bgm.volume = 0f;
            
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
        sfx.volume = volumeSFXSlider.value;
        Save();
    }
    public void Load()
    {
        // Load and log the values from PlayerPrefs
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        float bgmVolume = PlayerPrefs.GetFloat("bgmVolume");

        //Debug.Log("Loaded SFX Volume: " + sfxVolume);
        //Debug.Log("Loaded BGM Volume: " + bgmVolume);

        // Set the sliders and audio sources to the loaded values
        volumeSFXSlider.value = sfxVolume;
        sfx.volume = sfxVolume;

        volumeBGMSlider.value = bgmVolume;
        bgm.volume = bgmVolume;
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("bgmVolume", volumeBGMSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", volumeSFXSlider.value);
    }

    public void OpPause()
    {
        sfxE.PlaySFX_OpPause();
    }
    public void ToAdvSurv_SFX()
    {
        StartCoroutine(PlayDelay(0.65f));
    }
    private IEnumerator PlayDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxE.PlaySFX_PlayAdvSurv();
    }
}
