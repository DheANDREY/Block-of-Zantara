using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss1 : MonoBehaviour
{
    public block[] _block;
    public GameObject[] BoosterArea;
    private BossController boss;
    private EnemyController enemy;
   
    public static SkillBoss1 instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyController>();
        boss = GameObject.FindGameObjectWithTag("enemy").GetComponent<BossController>();
        //posBooster = GameObject.FindGameObjectWithTag("PosBooster").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isMonsterSpawned);
        if (Input.GetKeyDown(KeyCode.B))
        {
            BoostFallBlock();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HealHP(4f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FreezeMove();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Spawn");
            SpawnBoosterArea();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnBlockMonster();
            Debug.Log(isMonsterSpawned);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnSmokeNextMove();            
        }
    }

    public float dropTime;
    public void BoostFallBlock()
    {
        AnimAttackSkill(true);
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = dropTime;
            IconSkill(true);
        }
        StartCoroutine(ResetFallTimeAfterDelay(15f));
    }
    private IEnumerator ResetFallTimeAfterDelay(float delay)
    {
        Debug.Log("Done Boost");
        yield return new WaitForSeconds(delay); 
        foreach (var _blocks in _block)
        {
            AnimAttackSkill(false);
            IconSkill(false);
            _blocks.fallTime = 1.2f;
        }
    }   

    private bool isHeal;
    public void HealHP(float nilai)
    {        
        if (!isHeal)
        {
            IconSkill(true);
            AnimAttackSkill(true);
            if (boss != null)
            {
                boss.TakeDamage(-nilai);
            }else if(enemy != null)
            {
                enemy.TakeDamage(-nilai);
            }
            else
            {
                Debug.Log("No Boss or enemy");
            }           
            isHeal = true;            
        }
        StartCoroutine(ResetIconHeal(2));

    }
    private IEnumerator ResetIconHeal(float delay)
    {
        Debug.Log("Done Heal/SpawnSmoke");
        yield return new WaitForSeconds(delay);
        AnimAttackSkill(false);
        IconSkill(false);
        isHeal = false;
    }

    public GameObject prefabSmoke;
    private GameObject instantiatedSmoke;
    public void SpawnSmokeNextMove()
    {
        instantiatedSmoke = Instantiate(prefabSmoke, new Vector3(1.2f, 14.2f, 0), Quaternion.identity);
        IconSkill(true);
        AnimAttackSkill(true);
        StartCoroutine(DestroySmoke(25));
    }
    private IEnumerator DestroySmoke(float delay)
    {
        Debug.Log("Destroy SpawnSmoke");
        yield return new WaitForSeconds(delay);
        AnimAttackSkill(false);
        IconSkill(false);
        Destroy(instantiatedSmoke);
    }
    public int maxFreezeBlock;
    public void FreezeMove()
    {
        block.isFreeze = true;
        IconSkill(true);
        AnimAttackSkill(true);
        StartCoroutine(FreezeTime());
    }

    public static int counterFreeze;
    private IEnumerator FreezeTime()
    {
        Debug.Log("Done Freeze");
        while (counterFreeze < maxFreezeBlock)
        {
            yield return null;
        }
        AnimAttackSkill(false);
        IconSkill(false);
        counterFreeze = 0;
        block.isFreeze = false;
    }

    private Transform posBooster;
    private GameObject prefabBoost;
    public void SpawnBoosterArea()
    {
        //AnimAttackSkill(true);
        AnimAttackSkill(true);
        //IconSkill(true);
        IconSkill(true);
        int indexSpawn = Random.Range(0,BoosterArea.Length);
        prefabBoost = Instantiate(BoosterArea[indexSpawn], new Vector3(0,0,100), Quaternion.identity);
        prefabBoost.transform.SetParent(transform); Debug.Log("Spawn Boost");
        StartCoroutine(TimeBoostArea(30f));
    }
    private IEnumerator TimeBoostArea(float delay)
    {        
        yield return new WaitForSeconds(delay);
        foreach (var _blocks in _block)
        {
            //AnimAttackSkill(false);
            AnimAttackSkill(false);
            //IconSkill(false);
            IconSkill(false);
            Destroy(prefabBoost);
            Debug.Log("Destroy Spawn Boost");
        }
    }

    public static bool isMonsterSpawned = false;
    public void SpawnBlockMonster()
    {
        AnimAttackSkill(true);
        IconSkill(true);
        isMonsterSpawned = true;
        StartCoroutine(TimeAnimBMons(2f));
    }
    private IEnumerator TimeAnimBMons(float delay)
    {
        yield return new WaitForSeconds(delay);
        AnimAttackSkill(false);
        IconSkill(false);
    }



    public void IconSkill(bool isOn)
    {
        if (isOn)
        {
            HpEnemyManager.instance.SkillLights(true);
        }
        else
        {
            HpEnemyManager.instance.SkillLights(false);
        }
    }
    public void AnimAttackSkill(bool isOn)
    {
        if (isOn)
        {
            if (boss != null)
            {
                AnimatorBoss.instance.PlayAnimAtk(true);
            }else if(enemy != null)
            {
                AnimatorEnemy.instance.PlayAnimAtk(true);
            }
            else
            {
                Debug.Log("No Boss or Enemy");
            }
        }
        else
        {
            if (boss != null)
            {
                AnimatorBoss.instance.PlayAnimAtk(false);
            }
            else if (enemy != null)
            {
                AnimatorEnemy.instance.PlayAnimAtk(false);
            }
            else
            {
                Debug.Log("No Boss or Enemy");
            }
        }
    }
    public void ResetAllSkill()
    {
        ResetBoostFallBlock();
        ResetHealHP();
        ResetFreezeMove();
        ResetSpawnBoosterArea();
        ResetSpawnBlockMonster();
        ResetSpawnSmokeNextMove();
    }

    private void ResetBoostFallBlock()
    {
        StopAllCoroutines(); // Menghentikan semua coroutine yang sedang berjalan
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1f;
        }
        AnimAttackSkill(false);
        IconSkill(false);
    }

    private void ResetHealHP()
    {
        StopAllCoroutines();
        AnimAttackSkill(false);
        IconSkill(false);
        isHeal = false;
    }

    private void ResetFreezeMove()
    {
        StopAllCoroutines();
        block.isFreeze = false;
        AnimAttackSkill(false);
        IconSkill(false);
        counterFreeze = 0;
    }

    private void ResetSpawnBoosterArea()
    {
        StopAllCoroutines();
        AnimAttackSkill(false);
        IconSkill(false);
        if (prefabBoost != null)
        {
            Destroy(prefabBoost);
        }
    }

    private void ResetSpawnBlockMonster()
    {
        StopAllCoroutines();
        AnimAttackSkill(false);
        IconSkill(false);
        isMonsterSpawned = false;
    }

    private void ResetSpawnSmokeNextMove()
    {
        StopAllCoroutines();
        AnimAttackSkill(false);
        IconSkill(false);
        if (instantiatedSmoke != null)
        {
            Destroy(instantiatedSmoke);
        }
    }

}
