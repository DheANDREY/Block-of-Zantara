using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DisplayArcLevel : MonoBehaviour
{
    public LevelData levelData;

    public Image arcImage;
    public TextMeshProUGUI arcName;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI score;
    public GameObject nextBlocker;

    public static DisplayArcLevel instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        arcImage.sprite = levelData.imageArc;
        arcName.text = levelData.ArcName;
        lvl.text = levelData.levelNumber.ToString();
        score.text = levelData.score.ToString();


    }

    public void OpenNextButton()
    {
        nextBlocker.SetActive(false);
    }

}
