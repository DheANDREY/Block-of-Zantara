using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{   
    private static CharController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        manaCharManager = GameObject.FindGameObjectWithTag("mana").GetComponent<ManaCharManager>();

        isSkill1_Actived = false;
    }
    private void Update()
    {
               
    }

    public void UseSkill()
    {
        if (ManaCharManager.currentMana == 100)
        {
            manaCharManager.UseMana(100);
            ActivateSelectedSkill(skill1);
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }

    public SkillChar skillChar;
    [SerializeField]private CharSkillType skill1;
    private ManaCharManager manaCharManager;
    public bool isSkill1_Actived;
    public void ActivateSelectedSkill(CharSkillType skillType)
    {
        switch (skillType)
        {
            case CharSkillType.DirectDMG:                
                skillChar.DealDamageDirect();
                break;
            case CharSkillType.DestroyLowestBlocks:
                skillChar.DeleteLowestBlock();
                break;
            case CharSkillType.BuffDMG:
                skillChar.BuffDMG();
                break;
            case CharSkillType.ReduceBoss:
                skillChar.ReduceEffectSkill();
                break;
            case CharSkillType.UpScore:
                skillChar.ScoreExtra();
                break;
            case CharSkillType.SlowFallBlock:
                skillChar.SlowdownFall();
                break;
        }
    }

    public enum CharSkillType
    {
        DirectDMG,
        BuffDMG,
        ReduceBoss,
        UpScore,
        SlowFallBlock,
        DestroyLowestBlocks
    }
}
