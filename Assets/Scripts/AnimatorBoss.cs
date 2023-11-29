using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBoss : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public static AnimatorBoss instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void PlayAnimDie()
    {
        animator.SetBool("AnimDeath", true);
    }
    public void PlayAnimAtk(bool isAnimAttack)
    {
        if (isAnimAttack)
        {
            animator.SetBool("AnimAttack", true);
        }
        else
        {
            animator.SetBool("AnimAttack", false);
        }

    }
}
