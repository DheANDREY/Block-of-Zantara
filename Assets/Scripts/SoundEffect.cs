using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip hit, dmg, advSurv, setting, pause, selChar, openDesc, selectLvl, LBoardC, woosh, confReset, resetFXs;

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
    public void PlaySFX_PlayAdvSurv()
    {
        sfx.PlayOneShot(advSurv);
    }
    public void PlaySFX_OpSetting()
    {
        sfx.PlayOneShot(setting);
    }
    public void PlaySFX_SelChar()
    {
        sfx.PlayOneShot(selChar);
    }
    public void PlaySFX_OpPause()
    {
        sfx.PlayOneShot(pause);
    }
    public void PlaySFX_OpDesc()
    {
        sfx.PlayOneShot(openDesc);
    }
    public void PlaySFX_SelLvl()
    {
        sfx.PlayOneShot(selectLvl);
    }
    public void PlaySFX_LBCont()
    {
        sfx.PlayOneShot(LBoardC);
    }
    public void PlaySFX_SwapW()
    {
        sfx.PlayOneShot(woosh);
    }
    public void PlaySFX_CReset()
    {
        sfx.PlayOneShot(confReset);
    }
    public void PlaySFX_ResetFX()
    {
        sfx.PlayOneShot(resetFXs);
    }

    public void SetSFXVolume(float volume)
    {
        sfx.volume = volume;
    }
}
