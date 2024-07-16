using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss1 : MonoBehaviour
{
    public block[] _block;
    public GameObject[] BoosterArea;
    private BossController boss;
    private EnemyController enemy;
    private SurvivalFlow survivalF;
    [SerializeField]private Transform posSmokeSpawn;

    public static bool isReduceActive;

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
        GameObject survivalObject = GameObject.FindGameObjectWithTag("survival");
        if (survivalF != null)
        {
            survivalF = survivalObject.GetComponent<SurvivalFlow>();
        }

        posSmokeSpawn = GameObject.FindGameObjectWithTag("smoke").GetComponent<Transform>();
        //GameObject posSmokeObj = GameObject.FindGameObjectWithTag("smokeP");
        //if(posSmokeSpawn != null)
        //{
        //    posSmokeSpawn = posSmokeObj.GetComponent<Transform>();
        //}
        //posBooster = GameObject.FindGameObjectWithTag("PosBooster").GetComponent<Transform>();
 
        isReduceActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isMonsterSpawned);
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    BoostFallBlock();
        //}

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    HealHP(4f);
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FreezeMove();
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("Spawn");
        //    SpawnBoosterArea();
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    SpawnBlockMonster();
        //    Debug.Log(isMonsterSpawned);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SpawnSmokeNextMove();
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    LimitRotateBlock();
        //}
    }

    public float dropTime, boostSurvival;
    public void BoostFallBlock()
    {
        AnimAttackSkill(true);
        foreach (var _blocks in _block)
        {
            if (!isReduceActive)
            {
                if (survivalF == null)
                {
                    _blocks.fallTime = dropTime;
                }
                else
                {
                    Debug.Log("Boost Survival");
                    _blocks.fallTime = _blocks.fallTime / boostSurvival;
                }
            }
            else
            {
                _blocks.fallTime = dropTime*1.5f;
            }
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
            if (survivalF == null)
            {
                _blocks.fallTime = 1f;
            }
            else
            {
                Debug.Log("Deboost Survival");
                _blocks.fallTime = _blocks.fallTime*boostSurvival;
            }
        }
    }

    private bool isHeal;    
    public void HealHP(float nilai,float _duration)
    {
        if (!isHeal)
        {
            IconSkill(true);
            AnimAttackSkill(true);
            if (boss != null)
            {
                if (!isReduceActive)
                {
                    boss.TakeDamage(-nilai, 1);
                }
                else
                {
                    boss.TakeDamage(-nilai / 2, 1);
                }
            } else if (enemy != null)
            {
                if (!isReduceActive)
                {
                    enemy.TakeDamage(-nilai, 1);
                }
                else
                {
                    enemy.TakeDamage(-nilai / 2, 1);
                }
            }
            else
            {
                Debug.Log("No Boss or enemy");
            }
            isHeal = true;
        }

        StartCoroutine(HealOverTime(nilai, _duration));

    }
    private IEnumerator HealOverTime(float totalHeal, float duration)
    {
        float healRate = totalHeal / duration; // Nilai heal yang diterapkan setiap detik
        float totalHealed = 0;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float healAmount = healRate * Time.deltaTime; // Hitung heal per frame berdasarkan waktu
            totalHealed += healAmount; // Tambahkan healAmount ke totalHealed

            // Terapkan healAmount ke target (boss atau enemy)
            if (boss != null)
            {
                boss.TakeDamage(-healAmount,1);
            }
            else if (enemy != null)
            {
                enemy.TakeDamage(-healAmount,1);
            }
            else
            {
                //Debug.Log("No Boss or enemy");
            }

            timeElapsed += Time.deltaTime; // Tambahkan waktu yang telah berlalu
            yield return null; // Tunggu frame berikutnya
        }

        // Proses heal selesai
        IconSkill(false);
        AnimAttackSkill(false);
        isHeal = false;
    }

    public GameObject prefabSmoke;
    private GameObject instantiatedSmoke;
    public void SpawnSmokeNextMove()
    {
        instantiatedSmoke = Instantiate(prefabSmoke, posSmokeSpawn.transform.position, Quaternion.identity);
        IconSkill(true);
        AnimAttackSkill(true);
        if (!isReduceActive)
        {
            StartCoroutine(DestroySmoke(25));
        }
        else
        {
            StartCoroutine(DestroySmoke(25 / 2));
        }
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
        foreach (var _blocks in _block)
        {
            _blocks.isFreeze = true;
        }
        Debug.Log("DO Freeze");
        IconSkill(true);
        AnimAttackSkill(true);
        StartCoroutine(FreezeTime());
    }

    public static int counterFreeze;
    private IEnumerator FreezeTime()
    {
        Debug.Log("Done Freeze");
        if (!isReduceActive)
        {
            while (counterFreeze < maxFreezeBlock)
            {
                yield return null;
            }
        }
        else
        {
            while(counterFreeze-1 < maxFreezeBlock)
            {
                yield return null;
            }
        }
        AnimAttackSkill(false);
        IconSkill(false);
        counterFreeze = 0;
        foreach (var _blocks in _block)
        {
            _blocks.isFreeze = false;
        }
    }
    public void SpawnSideBlocker()
    {
        Debug.Log("Aktif Skill Side Blocker");
        foreach (var _blocks in _block)
        {
            _blocks.isBlockSide = true;
        }
        SpawnerBlock.instance.SpawnBlockerSide(true);
        IconSkill(true);
        AnimAttackSkill(true);
        StartCoroutine(TimeSideFreeze());
    }

    public int maxSideFreeze;
    public static int freezeSideCounter;
    private IEnumerator TimeSideFreeze()
    {
        if (!isReduceActive)
        {
            while (freezeSideCounter < maxSideFreeze)
            {
                yield return null;
            }
        }
        else
        {
            while (freezeSideCounter - 1 < maxSideFreeze)
            {
                yield return null;
            }
        }
        Debug.Log("Deactive Skill Side Blocker");
        SpawnerBlock.instance.SpawnBlockerSide(false);
        foreach (var _blocks in _block)
        {
            _blocks.isBlockSide = false;
        }

        AnimAttackSkill(false);
        IconSkill(false);
        freezeSideCounter = 0;
    }

    private Transform posBooster;
    private GameObject prefabBoost;
    public void SpawnBoosterArea()
    {
        //AnimAttackSkill(true);
        AnimAttackSkill(true);
        //IconSkill(true);
        IconSkill(true);
        int indexSpawn = Random.Range(0, BoosterArea.Length);
        prefabBoost = Instantiate(BoosterArea[indexSpawn], new Vector3(0, 0, 100), Quaternion.identity);
        prefabBoost.transform.SetParent(transform); Debug.Log("Spawn Boost");
        if (!isReduceActive)
        {
            StartCoroutine(TimeBoostArea(30f));
        }
        else
        {
            StartCoroutine(TimeBoostArea(30 / 2f));
        }
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
    
    public void DoubleMoveBlocks()
    {
        foreach (var _blocks in _block)
        {
            _blocks.isDoubleMove = true;
        }
        //block.instance.isDoubleMove = true;
        AnimAttackSkill(true);
        IconSkill(true);
        if (!isReduceActive)
        {
            StartCoroutine(TimeDoubleMove(15));
        }
        else
        {
            StartCoroutine(TimeDoubleMove(15/2));
        }
    }
    private IEnumerator TimeDoubleMove(int delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var _blocks in _block)
        {
            _blocks.isDoubleMove = false;
        }
        AnimAttackSkill(false);
        IconSkill(false);
    }
    
    public void BlackHoleMove()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            SpawnerBlock.instance.EnableBlackHole(true);
        }
        else
        {
            SpawnerBlock.instance.EnableBlackHole(false);
        }

        //block.instance.ForceMoveToSide();
        //block.isFreeze = true;
        AnimAttackSkill(true);
        IconSkill(true);
        if (!isReduceActive)
        {
            StartCoroutine(TimeSuckedBlock(12f));
        }
        else
        {
            StartCoroutine(TimeSuckedBlock(9f));
        }
    }
    private IEnumerator TimeSuckedBlock(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnerBlock.instance.DisableBlackHole();
        block.instance.isFreeze = false;
        AnimAttackSkill(false);
        IconSkill(false);
    }

    private int maxMoveLimiter=3;
    public int counterMove;
    public void LimitMoveBlock()
    {
        foreach (var _blocks in _block)
        {
            _blocks.isLimitMove = true;
        }
        AnimAttackSkill(true);
        IconSkill(true);
        StartCoroutine(TimeLimitMove());
    }
    private IEnumerator TimeLimitMove()
    {
        if (!isReduceActive)
        {
            while (counterMove < maxMoveLimiter)
            {
                yield return null;
            }
        }
        else
        {
            while (counterMove - 1 < maxMoveLimiter)
            {
                yield return null;
            }
        }
        foreach (var _blocks in _block)
        {
            _blocks.isLimitMove = false;            
        }
        counterMove = 0;
        AnimAttackSkill(false);
        IconSkill(false);
    }


    private int maxRotateLimiter = 4;
    public int counterRotate;
    public void LimitRotateBlock()
    {
        foreach (var _blocks in _block)
        {
            _blocks.isLimitRotate = true;
        }
        AnimAttackSkill(true);
        IconSkill(true);
        StartCoroutine(TimeLimitRotate());
    }
    private IEnumerator TimeLimitRotate()
    {
        if (!isReduceActive)
        {
            while (counterRotate < maxRotateLimiter)
            {
                yield return null;
            }
        }
        else
        {
            while (counterRotate - 2 < maxRotateLimiter)
            {
                yield return null;
            }
        }
        foreach (var _blocks in _block)
        {
            _blocks.isLimitRotate = false;
        }
        counterRotate = 0;
        AnimAttackSkill(false);
        IconSkill(false);
    }

    //====================================================================================================================================================================================================================================================
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
            } else if (enemy != null)
            {
                AnimatorEnemy.instance.PlayAnimAtk(true);
            }
            else
            {
                
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
                
            }
        }
    }


 //====================================================================================================================================================================================================================================================
    public void ResetAllSkill()
    {
        ResetBoostFallBlock();
        ResetHealHP();
        ResetFreezeMove();
        ResetSideFreeze();
        ResetSpawnBoosterArea();
        ResetSpawnBlockMonster();
        ResetSpawnSmokeNextMove();
        ResetDoubleMove();
        ResetBlackHole();
        ResetLimitMove();   ResetLimitRotate();
    }

    private void ResetBoostFallBlock()
    {
        StopAllCoroutines(); // Menghentikan semua coroutine yang sedang berjalan
        foreach (var _blocks in _block)
        {
            if (survivalF == null)
            {
                _blocks.fallTime = 1f;
            }
            else
            {
                _blocks.fallTime = _blocks.fallTime;
            }
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
        foreach (var _blocks in _block)
        {
            _blocks.isFreeze = false;
        }
        AnimAttackSkill(false);
        IconSkill(false);
        counterFreeze = 0;
    }

    private void ResetSideFreeze()
    {
        StopAllCoroutines();
        foreach (var _blocks in _block)
        {
            _blocks.isBlockSide = false;
        }
        SpawnerBlock.instance.SpawnBlockerSide(false);
        AnimAttackSkill(false);
        IconSkill(false);
        freezeSideCounter = 0;
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

    private void ResetDoubleMove()
    {
        foreach (var _blocks in _block)
        {
            _blocks.isDoubleMove = false;
        }
        AnimAttackSkill(false);
        IconSkill(false);
    }
    private void ResetBlackHole()
    {
        SpawnerBlock.instance.DisableBlackHole();
        foreach (var _blocks in _block)
        {
            _blocks.isFreeze = false;
        }
        AnimAttackSkill(false);
        IconSkill(false);
    }
    private void ResetLimitMove()
    {
        foreach (var _blocks in _block)
        {
            _blocks.isLimitMove = false;            
        }
        counterMove = 0;
        AnimAttackSkill(false);
        IconSkill(false);
    }
    private void ResetLimitRotate()
    {
        foreach (var _blocks in _block)
        {
            _blocks.isLimitRotate = false;
        }
        counterRotate = 0;
        AnimAttackSkill(false);
        IconSkill(false);
    }
}
