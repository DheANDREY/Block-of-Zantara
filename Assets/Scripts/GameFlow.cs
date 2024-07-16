using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using EasyTransition;
using DG.Tweening;

public class GameFlow : MonoBehaviour
{
    public TextMeshProUGUI textArcLevel;
    public TextMeshProUGUI textScore;
    public static int pointScore;
    private AudioFile_Handler bgm;
    //private GameObject levelAdv;
    public static bool isSurvivalMode;
    //public int currentLvl = 0;
    public int currentLvl = 0;
    private SoundEffect soundManager;
    private int modeChoosen;
    private SoundManager sfxM;

    //private SoundEffect sfxM;
    private void Start()
    {
        pointScore = 0;
        SkillBoss1.instance.ResetAllSkill();

        sfxM = FindObjectOfType<SoundManager>();
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioFile_Handler>();
        //levelAdv = GameObject.FindGameObjectWithTag("Adventure");
        soundManager = GameObject.FindGameObjectWithTag("soundmanager").GetComponent<SoundEffect>();

        modeChoosen = SelectArcMode.selectedMode;
        if (modeChoosen == 0)
        {
            textArcLevel.text = LevelArcController.levelArc;
            Time.timeScale = 1f;
            //Debug.Log(SelectArcMode.instance.selectedLevel);
            currentLvl = SelectArcMode.instance.selectedLevel;
            Debug.Log("Start = " + currentLvl);
            textArcLevel.text = currentLvl.ToString();
            textScore.text = pointScore.ToString();
        }
        else {
            isSurvivalMode = true;
            Debug.Log("Survival Mode");
            Time.timeScale = 1f;
            textScore.text = pointScore.ToString();
        };
        //Debug.Log("Start = " + currentLvl);
        //Time.timeScale = 1f;
        ////Debug.Log(SelectArcMode.instance.selectedLevel);
        //currentLvl = SelectArcMode.instance.selectedLevel;

        //if (currentLvl > LevelManager.highestLvl)
        //{
        //    LevelManager.highestLvl = currentLvl;

        //    PlayerPrefs.SetInt("HighestLVL", LevelManager.highestLvl);
        //}
        //textArcLevel.text = currentLvl.ToString();

    }

    private void Update()
    {
        //if (isPlayerLose)
        //{
        //    OpenLosePanel();
        //}
        //if (isPlayerWin)            
        //{
        //    Debug.Log("WinPanel from GameFlow");
        //    OpenWinPanel();
        //}

        if (Input.GetKeyDown(KeyCode.L))
        {
            
        }

        textScore.text = pointScore.ToString();
    }

    public static GameFlow instance;
    private void Awake()
    {
        instance = this;
        //soundManager.Load();
        // Cek apakah sudah ada instance dari GameManager
        //if (FindObjectsOfType<GameFlow>().Length > 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);

        // Mengambil nilai yang disimpan        
        //currentLvl = PlayerPrefs.GetInt("HighestLVL", 0);
    }
    public GameObject losePanel;
    public bool isPlayerLose;
    public void OpenLosePanel()
    {
        if (losePanelOpened)
            return;
        //Debug.Log("Grid Status After Losing First Game:");
        //for (int x = 0; x < 10; x++)
        //{
        //    for (int y = 0; y < 12; y++)
        //    {
        //        Debug.Log($"Grid[{x}, {y}] = {block.grid[x, y]}");
        //    }
        AudioFile_Handler.instance.PlayBGM_Lose(true);
        Time.timeScale = 0f;
        
        losePanel.SetActive(true);
        losePanelOpened = true;
    }
    public GameObject WinPanel;
    private bool winPanelOpened, losePanelOpened;
    public void OpenWinPanel()
    {
        if (winPanelOpened)
            return;

        if (currentLvl > LevelManager.instance.highestLvl)
        {
            LevelManager.instance.highestLvl = currentLvl;
            Debug.Log("Highest= " + LevelManager.instance.highestLvl);
            Debug.Log("CurrentLVL:" + currentLvl);

            PlayerPrefs.SetInt("HighestLVL", currentLvl);
        }
        else
        {
            //Debug.Log("Current Lvl Lower than Saved Lvl= " + currentLvl );
        }

        Time.timeScale = 0f;
        AudioFile_Handler.instance.PlayBGM_Win(true); Debug.Log("This Win BGM");
        WinPanel.SetActive(true);

        winPanelOpened = true;
    }
    public void CloseLosePanel()
    {
            losePanel.SetActive(false);        
    }

    public GameObject PausePanel;
    public Transform settingPanel;
    
    public void OpenPauseMenu(bool isOpen)
    {
        if (isOpen)
        {            
            //soundManager.Load();
            PausePanel.SetActive(true);
            sfxM.OpPause();
            settingPanel.DOScale(new Vector3(1, 1, 1), 0.25f).SetEase(Ease.OutSine).OnComplete(() => Time.timeScale = 0f);
            

        }
        else
        {
            
            Time.timeScale = 1f;
            sfxM.OpPause();
            settingPanel.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutSine).OnComplete(() => PausePanel.SetActive(false));            
        }
    }
    public SpawnerBlock spawnerBlock;
    public block[] _block;

    public void RestartGame()
    {
        CloseLosePanel();
        //isPlayerLose = false;
        //isPlayerWin = false;
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
        //Debug.Log("=== Grid Status First Game: ===");
        //for (int x = 0; x < 10; x++)
        //{
        //    for (int y = 0; y < 12; y++)
        //    {
        //        //Debug.Log($"Grid[{x}, {y}] = {block.grid[x, y]}");
        //    }
        //}
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;

        AudioFile_Handler.instance.PlayBGM_Main(true);
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1.2f;
        }
        SpawnerBlock.instance.SpawnNewBlock();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        AudioFile_Handler.instance.PlayBGM_Main(true);
    }

    public TransitionSettings transitionMove;
    public void GoNextArc()
    {
        Time.timeScale = 1f;   
        

        //MenuManager.instance.MoveContentInLvl(currentLvl);
        AudioFile_Handler.instance.PlayBGM_Main(true);

        //TransitionManager.Instance().Transition("LevelMenu", transitionMove, 0);

        //MenuManager.instance.OpenLevelMenu(true);
    }

    public void RestartSurvival()
    {
        SelectArcMode.ModeNow(1);
        //RestartGame();
        SceneManager.LoadScene("Survival");
        AudioFile_Handler.instance.PlayBGM_Main(true);
    }

    private IEnumerator StartTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        
    }


    public void ControllerMove(int i)
    {
        foreach (var _blocks in _block)
        {
            if (_blocks != null)
            {
                if (i == 1)
                {
                    //FindObjectOfType<block>().MoveRight2();
                    Debug.Log("SwipeRight");
                    //OnSwipe?.Invoke(Vector2.right);
                    block.instance.MoveRight2();
                }
                else if (i == 2)
                {                    
                        FindObjectOfType<block>().MoveLeft2();
                        //Debug.Log("After Boost");                    
                }
                else if (i == 3)
                {
                    //FindObjectOfType<block>().MoveUp2();
                    block.instance.MoveUp2();
                }
                else
                {

                }
            }
        }
    }
    
    
}
