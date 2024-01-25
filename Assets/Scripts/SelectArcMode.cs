using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectArcMode : MonoBehaviour
{
    public BosQueue BQ;
    public int selectedLevel;

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
    }
    public void LevelNow(int lvl)
    {
            selectedLevel = lvl;
    }
}
