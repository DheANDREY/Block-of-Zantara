using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectArcMode : MonoBehaviour
{
    public BosQueue BQ;
    public int selectedLevel;
    public int selectedChar;
    public static int selectedMode;
    public block[] _block;

    private SoundEffect sfx;

    public static SelectArcMode instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<SoundEffect>();
        //Debug.Log("nChar:" + selectedChar);
    }
    public void PlayAdvMode(BosQueue enemyForLevel)
    {
        BQ = enemyForLevel;
        //LevelArcController.levelArc = levelAdv;
        //SceneManager.LoadScene("Gameplay");
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1.2f;
        }
    }
    public void LevelNow(int lvl)
    {
            selectedLevel = lvl;
    }
    public static void ModeNow(int mode)
    {
        selectedMode = mode;
        // 0 = Adventure
        // 1 = Survival
    }

    [SerializeField] private GameObject[] descSkillChar;
    [SerializeField] private GameObject[] selected;
    public void CharNumber(int charN)
    {
        sfx.PlaySFX_SelChar();
        selectedChar = charN;
        for (int i = 0; i < selected.Length; i++)
        {
            if (i != selectedChar)
            {
                selected[i].SetActive(false);
            }
        }
        selected[selectedChar].SetActive(true);
        Debug.Log("Char Choosen:" + charN);
    }
    
}
