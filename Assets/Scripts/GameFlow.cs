using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameFlow : MonoBehaviour
{
    public TextMeshProUGUI textArcLevel;
    private AudioFile_Handler bgm;
    private int currentLevel;
    private void Start()
    {
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioFile_Handler>();
        textArcLevel.text = LevelArcController.levelArc;
        Time.timeScale = 1f;
        //Debug.Log(SelectArcMode.instance.selectedLevel);
        currentLevel = SelectArcMode.instance.selectedLevel;

        if (currentLevel > LevelManager.highestLvl)
        {
            LevelManager.highestLvl = currentLevel;

            PlayerPrefs.SetInt("HighestLVL", LevelManager.highestLvl);
        }
        textArcLevel.text = currentLevel.ToString();
    }

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

    public static GameFlow instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject losePanel;
    public bool isPlayerLose;
    public void OpenLosePanel()
    {
        //Debug.Log("Grid Status After Losing First Game:");
        //for (int x = 0; x < 10; x++)
        //{
        //    for (int y = 0; y < 12; y++)
        //    {
        //        Debug.Log($"Grid[{x}, {y}] = {block.grid[x, y]}");
        //    }
        //}
        Time.timeScale = 0f;
        AudioFile_Handler.instance.PlayBGM_Boss(false);
        losePanel.SetActive(true);        
    }
    public GameObject WinPanel;
    public bool isPlayerWin;
    public void OpenWinPanel()
    {
        Time.timeScale = 0f;
        AudioFile_Handler.instance.PlayBGM_Boss(false);
        WinPanel.SetActive(true);
    }
    public void CloseLosePanel()
    {
            losePanel.SetActive(false);        
    }

    public GameObject PausePanel;
    public void OpenPauseMenu(bool isOpen)
    {
        if (isOpen)
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
        }
    }
    public SpawnerBlock spawnerBlock;
    public block[] _block;

    public void RestartGame()
    {
        CloseLosePanel();
        isPlayerLose = false;
        isPlayerWin = false;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                if (block.grid[x, y] != null)
                {
                    Destroy(block.grid[x, y].gameObject);
                    block.grid[x, y] = null;
                }
            }
        }
        Debug.Log(isPlayerLose);
        Debug.Log("=== Grid Status First Game: ===");
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                Debug.Log($"Grid[{x}, {y}] = {block.grid[x, y]}");
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        AudioFile_Handler.instance.PlayBGM_Utama(true);
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1f;
        }
        SpawnerBlock.instance.SpawnNewBlock();
    }

    public void BackToMainMenu()
    {
        AudioFile_Handler.instance.PlayBGM_Utama(true);
        MenuManager.instance.OpenMainMenu(true);
    }

    public void GoNextArc()
    {
        currentLevel++;       
        LevelManager.highestLvl = currentLevel;
        PlayerPrefs.SetInt("HighestLVL", LevelManager.highestLvl);
        AudioFile_Handler.instance.PlayBGM_Utama(true);
        MenuManager.instance.OpenLevelMenu(true);
    }
}
