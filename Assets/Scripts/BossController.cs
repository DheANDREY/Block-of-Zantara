using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float maxHP = 10.0f;
    private float currentHP;


    private static BossController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {        
        currentHP = maxHP;
        HpEnemyManager.instance.SetMaxHP(maxHP); // Atur nilai maksimum HP di UI Health Bar
    }
    private void Update()
    {
        if(currentHP <= 0)
        {
            AnimatorBoss.instance.PlayAnimDie();
            Invoke("BossDie", 4f);
        }
    }

    public void BossDie()
    {
        MenuManager.instance.OpenWinPanel();
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        HpEnemyManager.instance.SetCurrentHP(currentHP); // Perbarui UI Health Bar setelah menerima kerusakan
    }
}
