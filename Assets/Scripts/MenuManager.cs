using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    private SoundEffect sfx;

    private void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<SoundEffect>();
        Time.timeScale = 1f;
        //MoveContentInLvl(LevelManager.highestLvl);
    }

    public static MenuManager instance;
    private void Awake()
    {
        instance = this;

    }
    public void OpenMainMenu(bool isOpen)
    {
        if (isOpen)
        {            
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
        }
    }
    public void OpenLevelMenu(bool isOpen)
    {
        if (isOpen)
        {
            
            SceneManager.LoadScene("LevelMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ToAdvSurv_SFX()
    {
        StartCoroutine(PlayDelay(0.65f));
    }
    private IEnumerator PlayDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfx.PlaySFX_PlayAdvSurv();
    }

    
    public void PlaySurvivalMode()
    {
        sfx.PlaySFX_PlayAdvSurv();
        SceneManager.LoadScene("Survival");
        SelectArcMode.ModeNow(1);
    }

    public GameObject settingMenu;
    public Transform settingPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }

    public bool playSFX;
    public void ResetGameProgress()
    {
        int a = 0;
        PlayerPrefs.SetInt("HighestLVL", a);
        playSFX = false;
        sfx.PlaySFX_ResetFX();
        ConfirmReset(false);
        OpenSettingMenu(false);
        Debug.Log("Reset Prefs: " + a);
    }
    public void OpenSettingMenu(bool isOpen)
    {
        if (isOpen)
        {
            sfx.PlaySFX_OpSetting();
            settingMenu.SetActive(true);
            settingPanel.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
            playSFX = true;
        }
        else
        {
            if (playSFX)
            {
                sfx.PlaySFX_OpSetting();
            }
            settingPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => settingMenu.SetActive(false));
        }
    }

    public GameObject leadBMenu, advLMenu,survLMenu ;
    public Transform leadBPanelBG;
    public void OpenLeaderboard(bool isOpen)
    {
        if (isOpen)
        {            
            sfx.PlaySFX_OpSetting();
            leadBMenu.SetActive(true);
            leadBPanelBG.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
        }
        else
        {           
                sfx.PlaySFX_OpSetting();
            
            leadBPanelBG.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => leadBMenu.SetActive(false));
        }
    }
    public void OpenAdvLBoard(bool isOpen)
    {
        if (isOpen)
        {
            sfx.PlaySFX_LBCont();
            advLMenu.SetActive(true);
        }
        else
        {
            sfx.PlaySFX_LBCont();
            advLMenu.SetActive(false);
        }
    }
    public void OpenSurvLBoard(bool isOpen)
    {
        if (isOpen)
        {
            sfx.PlaySFX_LBCont();
            survLMenu.SetActive(true);
        }
        else
        {
            sfx.PlaySFX_LBCont();
            survLMenu.SetActive(false);
        }
    }

    public GameObject panelConfirmReset;
    public void ConfirmReset(bool isOpen)
    {
        if (isOpen)
        {
            sfx.PlaySFX_CReset();
            panelConfirmReset.SetActive(true);
            panelConfirmReset.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
            playSFX = true;
        }
        else
        {
            if (playSFX)
            {
                sfx.PlaySFX_OpPause();
            }
            panelConfirmReset.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => panelConfirmReset.SetActive(true));
        }
    }
    public GameObject panelConfirmExit;
    public void ConfirmExit(bool isOpen)
    {
        if (isOpen)
        {
            panelConfirmExit.SetActive(true);
            panelConfirmExit.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
        }
        else
        {
            panelConfirmExit.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => panelConfirmReset.SetActive(true));
        }
    }
    public void CloseGame()
    { 
            Application.Quit();        
    }
    
}
