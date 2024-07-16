using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int highestLvl = 0;    
    public GameObject[] LockLvl;
    public GameObject[] lockChar;

    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        highestLvl = PlayerPrefs.GetInt("HighestLVL");
        Debug.Log("Highest: "+highestLvl);
        
        MoveContentInLvl(highestLvl);
    }

    private void Update()
    {
        for (int i = 0; i < LockLvl.Length; i++)
        {
            if (i < highestLvl)
            {
                LockLvl[i+1].SetActive(false);
                lockChar[i + 1].SetActive(false);
            }

        }


        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    highestLvl++;
        //    Debug.Log(highestLvl);
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    highestLvl = 1;
        //    Debug.Log(highestLvl);
        //}
    }

    public RectTransform rectTransform;
    public void MoveContentInLvl(int moveValue)
    {
        float valueLeft = -1000 * moveValue;
        if (rectTransform != null)
        {
            Vector2 offsetToLeft = rectTransform.offsetMin;
            offsetToLeft.x = valueLeft;
            rectTransform.offsetMin = offsetToLeft;
        }

    }
}
