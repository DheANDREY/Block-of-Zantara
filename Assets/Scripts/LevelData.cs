using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelData : ScriptableObject
{
    public new int levelNumber;
    public string ArcName;
    public int score;
    public Sprite imageArc;


    public void Print()
    {
        Debug.Log("Level :" + levelNumber + " Name: " + ArcName + " Score:" + score);
    }
}
