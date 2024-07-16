using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class MovementText : MonoBehaviour
{
    public GameObject textMove;
    private Vector3 initialLocalPosition;
    public TextMeshProUGUI textObj;

    public static MovementText instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        initialLocalPosition = textMove.transform.localPosition;
        
        //StartMove();
    }

    public void StartMove(int numbSkill)
    {
        textObj.text = ShowSkillName.instance.nameSkill[numbSkill];
        textMove.transform.DOLocalMoveY(-3.5f, 3).OnComplete(() => Invoke("NextMove", 4));
    }

    private void NextMove()
    {
        textMove.transform.DOLocalMoveY(-50f, 1);
        Invoke("ResetPos", 2);
    }
    private void ResetPos()
    {
        //textMove.transform.position = new Vector3(0, 50, 0);
        textMove.transform.localPosition = initialLocalPosition;
    }
}
