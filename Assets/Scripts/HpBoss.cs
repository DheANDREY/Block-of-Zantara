using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBoss : MonoBehaviour
{
    public Image hpFill;
    public float hpRemaining;
    public float maxHP = 10.0f;
    // Start is called before the first frame update
    public static HpBoss instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        hpRemaining = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpRemaining > 0)
        {
            //hpRemaining -= Time.deltaTime;

            hpFill.fillAmount = hpRemaining / maxHP;
        }
        //Debug.Log(hpRemaining);
    }
    public void DecreaseHP(float dmg)
    {
        hpRemaining -= dmg;
        hpFill.fillAmount = hpRemaining / maxHP;
    }

}
