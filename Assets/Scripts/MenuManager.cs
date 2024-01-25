using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
    public GameObject settingMenu;
    public void OpenSettingMenu(bool isOpen)
    {
        if (isOpen)
        {
            settingMenu.SetActive(true);

        }
        else
        {
            settingMenu.SetActive(false);

        }
    }

    
}
