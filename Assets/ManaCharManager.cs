using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaCharManager : MonoBehaviour
{
    public Image manaFill;

    private float maxMana=100;
    public static float currentMana;
    private CharController charC;
    public ButtonSkillOn playerButton;

    public static ManaCharManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        charC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>();

        currentMana = 0;
    }

    public bool isButtonSkillOn;
    private void Update()
    {
        if (currentMana < 0)
        {
            currentMana = 0;
        }

        if(currentMana>=maxMana)
        {
            currentMana = maxMana;
            isButtonSkillOn = true;
        }



        UpdateUI();

        if (Input.GetKeyDown(KeyCode.M))
        {
            //if (currentMana >= 10)
            //{
            //    UseMana(10); Debug.Log("Mana: " + currentMana);
            //}
            //else
            //{
            //    Debug.Log("Not Enough Mana: " + currentMana);
            //}
            IncreaseMana(maxMana);
        }

        
    }
    public void SetMaxMana(float value)
    {
        maxMana = value;
        currentMana = maxMana; // Atur HP awal ke nilai maksimum
        UpdateUI();
    }

    public void SetCurrentMana(float value)
    {
        currentMana = value;
        UpdateUI();
    }

    public void UseMana(float value)
    {
        currentMana -= value;
    }

    public void IncreaseMana(float value)
    {
        currentMana += value;
    }

    private void UpdateUI()
    {        
        if (manaFill != null)
        {
            manaFill.fillAmount = currentMana / maxMana;
            //Debug.Log(currentHP);
        }
    }

    public GameObject skillLightON;
    public void SkillLights(bool isActiveSkill)
    {
        if (isActiveSkill)
        {
            skillLightON.SetActive(true);
        }
        if (!isActiveSkill)
        {
            skillLightON.SetActive(false);
        }
    }

    public void UseSkillPlayerr()
    {
        charC.UseSkill();
        //ButtonSkillStatus(false);
        isButtonSkillOn = false;
    }    
}
