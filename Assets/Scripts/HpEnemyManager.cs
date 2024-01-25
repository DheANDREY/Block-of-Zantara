using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEnemyManager : MonoBehaviour
{
    public Image hpFill;

    private float maxHP;
    private float currentHP;
    public static HpEnemyManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       // Debug.Log(currentHP);
    }
    private void Update()
    {
        UpdateUI();
        
        //Debug.Log(currentHP);
    }
    public void SetMaxHP(float value)
    {
        maxHP = value;
        currentHP = maxHP; // Atur HP awal ke nilai maksimum
        UpdateUI();
    }

    public void SetCurrentHP(float value)
    {
        currentHP = value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (maxHP > 0)
        {            
            hpFill.fillAmount = currentHP / maxHP;
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
        if(!isActiveSkill)
        {
            skillLightON.SetActive(false);
        }
    }
}
