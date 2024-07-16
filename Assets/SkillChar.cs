using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChar : MonoBehaviour
{
    public block[] _block;
    private BossController boss;
    private EnemyController enemy;
    private CharController charC;

    public static SkillChar instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyController>();
        boss = GameObject.FindGameObjectWithTag("enemy").GetComponent<BossController>();        
        charC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>();
        //posBooster = GameObject.FindGameObjectWithTag("PosBooster").GetComponent<Transform>();
    }

    private void Update()
    {
        // Periksa jika referensi `enemy` atau `boss` null dan perbarui
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("enemy")?.GetComponent<EnemyController>();
        }

        if (boss == null)
        {
            boss = GameObject.FindGameObjectWithTag("enemy")?.GetComponent<BossController>();
        }
    }

    public GameObject dmg;
    public void DealDamageDirect()
    {
        if (boss != null)
        {
            AnimAttackSkill(true);
            IconSkill(true);
            boss.TakeDamage(boss.maxHP/6,1);
            Instantiate(dmg, new Vector3(transform.position.x,transform.position.y+1,0), Quaternion.identity);
            Invoke("OffDDamage", 1.7f);
        }
        else if (enemy != null)
        {
            AnimAttackSkill(true);
            IconSkill(true);
            enemy.TakeDamage(enemy.maxHP/4.5f,1);
            Instantiate(dmg, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            Invoke("OffDDamage", 1.7f);
        }
        else
        {
            Debug.Log("No Boss or Enemy");
        }
    }
    private void OffDDamage()
    {
        AnimAttackSkill(false);
        IconSkill(false);
    }

    public GameObject prefabDestroyLow;
    public void DeleteLowestBlock()
    {
        AnimAttackSkill(true);
        IconSkill(true);
        
        StartCoroutine(DelayDestroy(5));
    }
    private IEnumerator DelayDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(prefabDestroyLow, new Vector3(9f, 0, 0), Quaternion.identity);        
        AnimAttackSkill(false);
        IconSkill(false);
        Invoke("DelayDelete", 1);
    }
    private void DelayDelete()
    {
        block.instance.DeleteLowest();
    }

    public static bool isDMGBuffed=false;
    public void BuffDMG()
    {
        AnimAttackSkill(true);
        IconSkill(true);
        isDMGBuffed = true;
        StartCoroutine(ResetAfterBuff(15f));
    }
    private IEnumerator ResetAfterBuff(float delay)
    {
        yield return new WaitForSeconds(delay);
        IconSkill(false);
        AnimAttackSkill(false);
        isDMGBuffed = false;
    }

    public void ReduceEffectSkill()
    {
        AnimAttackSkill(true);
        IconSkill(true);
        SkillBoss1.isReduceActive = true;
        StartCoroutine(ResetReduceEffect(12f));
    }
    private IEnumerator ResetReduceEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        IconSkill(false);
        AnimAttackSkill(false);
        SkillBoss1.isReduceActive = false;
    }

    public bool isUpScore;
    public void ScoreExtra()
    {
        IconSkill(true);
        AnimAttackSkill(true);
        isUpScore = true;
        if (boss != null)
        {
            boss.TakeDamage(boss.maxHP / (6f*1.25f), 1);
        }
        else if (enemy != null)
        {
            enemy.TakeDamage(enemy.maxHP / (4.5f*1.35f), 1);
        }
        else
        {
            Debug.Log("No Boss or Enemy");
        }
        StartCoroutine(ResetUpScore(15f));
    }
    private IEnumerator ResetUpScore(float delay)
    {
        yield return new WaitForSeconds(delay);
        IconSkill(false);
        AnimAttackSkill(false);
        isUpScore = false;
    }

    public bool isSlowdown;
    public int blockCounter;
    public void SlowdownFall()
    {
        isSlowdown = true;
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = _blocks.fallTime * 1.5f;
        }
        AnimAttackSkill(true);
        IconSkill(true);
        StartCoroutine(ResetSlowdown());
    }
    private IEnumerator ResetSlowdown()
    {
        while (blockCounter < 5)
        {
            yield return null;
        }
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1;
        }
        isSlowdown = false;
        IconSkill(false);
        AnimAttackSkill(false);
        blockCounter = 0;
    }



    public void IconSkill(bool isOn)
    {
        if (isOn)
        {
            ManaCharManager.instance.SkillLights(true);
        }
        else
        {
            ManaCharManager.instance.SkillLights(false);
        }
    }

    [SerializeField] private Animator animator;
    public void AnimAttackSkill(bool isOn)
    {
        if (animator != null)
        {
            if (isOn)
            {
                animator.SetBool("AnimAttack", true);
            }
            else
            {
                animator.SetBool("AnimAttack", false);

            }
        }
        else
        {
            Debug.Log("Survival");
        }
    }
}
