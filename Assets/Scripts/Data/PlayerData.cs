using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highestLvl;

    public PlayerData(LevelManager lvlManager)
    {
        highestLvl = lvlManager.highestLvl;
    }
}
