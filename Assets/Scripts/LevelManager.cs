using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int highestLvl = 1;
    public GameObject[] LockLvl;

    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerPrefs.SetInt("HighestLVL", 1);
    }

    private void Update()
    {
        for (int i = 0; i < LockLvl.Length; i++)
        {
            if (i < highestLvl)
            {
                LockLvl[i].SetActive(false);
            }
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(highestLvl);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            highestLvl = 1;
            Debug.Log(highestLvl);
        }



    }
}
