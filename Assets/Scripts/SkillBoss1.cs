using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss1 : MonoBehaviour
{
    public block[] _block;
    
    // Start is called before the first frame update
    void Start()
    {
        // _block = GameObject.FindGameObjectWithTag("Block").GetComponent<block>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            BoostFallBlock();
        }
    }

    public void BoostFallBlock()
    {
        AnimatorBoss.instance.PlayAnimAtk(true);
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 0.5f;
            HpEnemyManager.instance.SkillLights(true);
        }
        StartCoroutine(ResetFallTimeAfterDelay(10f));
    }
    private IEnumerator ResetFallTimeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu selama 30 detik
        foreach (var _blocks in _block)
        {
            AnimatorBoss.instance.PlayAnimAtk(false);
            HpEnemyManager.instance.SkillLights(false);
            _blocks.fallTime = 1f;
        }
    }
}
