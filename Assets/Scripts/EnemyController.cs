using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHP = 10.0f;
    private float currentHP;
    private EnemyBosSystem TB;
    public GameObject vfxDie;

    private static EnemyController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TB = GetComponent<EnemyBosSystem>();

        currentHP = maxHP;
        HpEnemyManager.instance.SetMaxHP(maxHP); // Atur nilai maksimum HP di UI Health Bar
        isSkill1_Actived = false; isSkill2_Actived = false;
    }
    private void Update()
    {
        if (currentHP < (maxHP * 0.75) && !isSkill1_Actived)
        {
            ActivateSelectedSkill(skill1);
            isSkill1_Actived = true;
        }
        else if (currentHP < (maxHP * 0.35) && !isSkill2_Actived)
        {
            ActivateSelectedSkill(skill2);
            isSkill2_Actived = true;
        }
        else
        {
            //Debug.Log("Normal");
        }

        if (currentHP <= 0)
        {
            //Debug.Log("coba");
            //AnimatorBoss.instance.PlayAnimDie();
            PlayVFXDie();
            Invoke("EnemyDie", 1.75f);
        }
    }

    public Transform posEnemy;
    private void PlayVFXDie()
    {
        GameObject prefabSFX = Instantiate(vfxDie, new Vector3(6.61f, 16.71f, -10f), Quaternion.identity) as GameObject;
        //ParticleSystem particleSystem = prefabSFX.GetComponent<ParticleSystem>();
        //if (particleSystem != null)
        //{
        //    // Mengurangi ukuran partikel sebanyak 50% dari ukuran awal
        //    float scaleFactor = 0.55f;
        //    ParticleSystem.MainModule mainModule = particleSystem.main;
        //    mainModule.scalingMode = ParticleSystemScalingMode.Local; // Pastikan mode scaling lokal
        //    mainModule.startSizeMultiplier *= scaleFactor;
        //}
        prefabSFX.transform.SetParent(posEnemy);
    }
    public void EnemyDie()
    {
        //GameFlow.instance.OpenWinPanel();
        isSkill1_Actived = false; isSkill2_Actived = false;
        SkillBoss1.instance.ResetAllSkill();
        Destroy(gameObject);        
        EnemyBosSystem.instance.isChangeEnemy = true;
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        HpEnemyManager.instance.SetCurrentHP(currentHP); // Perbarui UI Health Bar setelah menerima kerusakan
    }

    public SkillBoss1 skillBos;
    [SerializeField]
    private BossSkillType skill1, skill2;
    public bool isSkill1_Actived, isSkill2_Actived;
    public void ActivateSelectedSkill(BossSkillType skillType)
    {
        switch (skillType)
        {
            case BossSkillType.BoostFallBlock:
                skillBos.BoostFallBlock();
                break;
            case BossSkillType.HealHP:
                skillBos.HealHP(3.5f);
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
