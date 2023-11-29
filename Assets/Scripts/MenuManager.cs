using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
        if (isPlayerLose)
        {
            OpenLosePanel();
        }
        if (isPlayerWin)
        {
            OpenWinPanel();
        }
    }

    public static MenuManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject losePanel;
    public bool isPlayerLose;
    public void OpenLosePanel()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);        
    }
    public GameObject WinPanel;
    public bool isPlayerWin;
    public void OpenWinPanel()
    {
        Time.timeScale = 0f;
        WinPanel.SetActive(true);
    }
    public void CloseLosePanel()
    {
            losePanel.SetActive(false);        
    }
    public SpawnerBlock spawnerBlock;
    public block[] _block;
    public void RestartGame()
    {
        CloseLosePanel();
        isPlayerLose = false;
        isPlayerWin = false;
        Debug.Log(isPlayerLose);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1f;
        }
        SpawnerBlock.instance.SpawnNewPrefab();
        //spawnerBlock.SpawnNewPrefab();
    }

}
