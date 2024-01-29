using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectArcMode : MonoBehaviour
{
    public BosQueue BQ;
    public int selectedLevel;
    public block[] _block;

    public static SelectArcMode instance;
    private void Awake()
    {
        instance = this;
    }

    public void PlayAdvMode(BosQueue enemyForLevel)
    {
        BQ = enemyForLevel;
        //LevelArcController.levelArc = levelAdv;
        SceneManager.LoadScene("Gameplay");
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1.2f;
        }
    }
    public void LevelNow(int lvl)
    {
            selectedLevel = lvl;
    }
}
