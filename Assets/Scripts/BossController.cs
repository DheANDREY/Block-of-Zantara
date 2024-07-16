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
    Smoke,
    SideBlocker,
    DoubleSideMove,
    BlackHole,
    LimitMove,
    LimitRotate
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
            MovementText.instance.StartMove(0);
            ActivateSelectedSkill(skill1);
            Debug.Log("Skill1 Active: " + skill1);
            isSkill1_Actived = true;
        }
        else if (currentHP < (maxHP * 0.5) && !isSkill2_Actived)
        {
            MovementText.instance.StartMove(1);
            Debug.Log("Skill2 Active: "+skill2);
            ActivateSelectedSkill(skill2);
            isSkill2_Actived = true;
        }
        else if(currentHP < (maxHP * 0.15)&& !isSkill3_Actived)
        {
            MovementText.instance.StartMove(2);
            Debug.Log("Skill3 Active: "+skill3);
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
        PlayVFXDie();
        //GameFlow.instance.OpenWinPanel();
        isSkill1_Actived = false; isSkill2_Actived = false; isSkill3_Actived = false;
        SkillBoss1.instance.ResetAllSkill();
        Destroy(gameObject);
        
        EnemyBosSystem.instance.isChangeEnemy = true;
        SurvivalEnemyBoss.instance.isChangeEnemy = true;
    }

    [SerializeField] private GameObject vfxDie;
    public Transform posBoss;
    private void PlayVFXDie()
    {
        GameObject prefabSFX = Instantiate(vfxDie, posBoss.position, Quaternion.identity) as GameObject;
        //ParticleSystem particleSystem = prefabSFX.GetComponent<ParticleSystem>();
        //if (particleSystem != null)
        //{
        //    // Mengurangi ukuran partikel sebanyak 50% dari ukuran awal
        //    float scaleFactor = 0.55f;
        //    ParticleSystem.MainModule mainModule = particleSystem.main;
        //    mainModule.scalingMode = ParticleSystemScalingMode.Local; // Pastikan mode scaling lokal
        //    mainModule.startSizeMultiplier *= scaleFactor;
        //}
        prefabSFX.transform.SetParent(posBoss);
    }

    public void TakeDamage(float damage, float buff)
    {        
            currentHP -= damage * buff;
        //Debug.Log("DMG: " + damage * buff);
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
                    skillBos.HealHP(5.5f*0.65f,6);
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
                case BossSkillType.SideBlocker:
                    skillBos.SpawnSideBlocker();
                    break;
                case BossSkillType.DoubleSideMove:
                    skillBos.DoubleMoveBlocks();
                    break;
                case BossSkillType.BlackHole:
                    skillBos.BlackHoleMove();
                    break;
                case BossSkillType.LimitMove:
                    skillBos.LimitMoveBlock();
                    break;
                case BossSkillType.LimitRotate:
                    skillBos.LimitRotateBlock();
                    break;
        }
    }
}

