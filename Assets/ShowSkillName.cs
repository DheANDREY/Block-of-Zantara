using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowSkillName : MonoBehaviour
{
    public string[] nameSkill;

    private MovementText movementText;

    public static ShowSkillName instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        movementText = GameObject.FindGameObjectWithTag("skillName").GetComponent<MovementText>();
        //movementText.textObj.text = nameSkill[0];
    }

    private void Update()
    {
        
    }
}
