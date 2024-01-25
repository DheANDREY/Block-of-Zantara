using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossSkillType
{
    BoostFallBlock,
    HealHP,
    FreezeMove,
    SpawnBooster,
    SpawnMonster,
    Smoke
}
public class BossController : MonoBehaviour
{
    public float maxHP;
    private float currentHP;
    private EnemyBosSystem TB;

    public SkillBoss1 skillBos;

    private static BossController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TB = GetComponent<EnemyBosSystem>();

        currentHP = maxHP;
        HpEnemyManager.instance.SetMaxHP(maxHP); // Atur nilai maksimum HP di UI Health Bar
        isSkill1_Actived = false; isSkill2_Actived = false; isSkill3_Actived = false;
    }
    private void Update()
    {
        if (currentHP < (maxHP * 0.8)&& !isSkill1_Actived)
        {
            Debug.Log("Skill: Smoke");
            ActivateSelectedSkill(skill1);
            isSkill1_Actived = true;
        }
        else if (currentHP < (maxHP * 0.5) && !isSkill2_Actived)
        {
            Debug.Log("Skill: Smoke");
            ActivateSelectedSkill(skill2);
            isSkill2_Actived = true;
        }
        else if(currentHP < (maxHP * 0.15)&& !isSkill3_Actived)
        {
            Debug.Log("Skill: Heal");
            ActivateSelectedSkill(skill3);
            isSkill3_Actived = true;
        }
        else
        {
            //Debug.Log("Normal");
        }
        if (currentHP <= 0)
        {
            //Debug.Log("coba");
            AnimatorBoss.instance.PlayAnimDie();
            
            Invoke("EnemyDie", 4f);
        }
    }

    public void EnemyDie()
    {
        //GameFlow.instance.OpenWinPanel();
        isSkill1_Actived = false; isSkill2_Actived = false; isSkill3_Actived = false;
        SkillBoss1.instance.ResetAllSkill();
        Destroy(gameObject);
        
        EnemyBosSystem.instance.isChangeEnemy = true;
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        HpEnemyManager.instance.SetCurrentHP(currentHP); // Perbarui UI Health Bar setelah menerima kerusakan
    }
    [SerializeField]
    private BossSkillType skill1, skill2;
    public bool isSkill1_Actived, isSkill2_Actived;

    [SerializeField]
    private BossSkillType skill3;
    public bool isSkill3_Actived;
    public void ActivateSelectedSkill(BossSkillType skillType)
    {
            switch (skillType)
            {
                case BossSkillType.BoostFallBlock:
                    skillBos.BoostFallBlock();
                    break;
                case BossSkillType.HealHP:
                    skillBos.HealHP(5.5f);
                    break;
                case BossSkillType.FreezeMove:
                    skillBos.FreezeMove();
                    break;
                case BossSkillType.SpawnBooster:
                    skillBos.SpawnBoosterArea();
                    break;
                case BossSkillType.SpawnMonster:
                    skillBos.SpawnBlockMonster();
                    break;
                case BossSkillType.Smoke:
                    skillBos.SpawnSmokeNextMove();
                    break;            
            }
    }
}

