using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnemy : MonoBehaviour
{
    private Animator animator;
    public static AnimatorEnemy instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimDie()
    {
        animator.SetBool("AnimDeath", true);
    }
    public GameObject EffectSkill; 
    private GameObject prefabSkill;
    public void PlayAnimAtk(bool isAnimAttack)
    {
        
        if (isAnimAttack)
        {
            animator.SetBool("AnimAttack", true);
            prefabSkill = Instantiate(EffectSkill, new Vector3(6.612f, 15.69f, 0), Quaternion.identity);
            prefabSkill.transform.SetParent(transform);
        }
        else
        {
            Destroy(prefabSkill, 0.7f);
            animator.SetBool("AnimAttack", false);
        }
    }
}
