using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalEnemyBoss : MonoBehaviour
{
    public GameObject[] enemy;
    public bool isChangeEnemy;

    public static SurvivalEnemyBoss instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        RandomSpawnEnemy();
    }

    void Update()
    {
        if (isChangeEnemy)
        {
            ChangeEnemy();
        }
        //ChangeEnemy();
    }

    public void RandomSpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemy[Random.Range(0, enemy.Length - 1)], transform.position, Quaternion.identity);
        spawnedEnemy.transform.SetParent(transform);
        //spawnedEnemy.transform.localScale = new Vector3(0.78f, 0.8f);
    }

    public void ChangeEnemy()
    {
            RandomSpawnEnemy();
            //EnemyBosSystem.instance.isChangeEnemy = false;
            isChangeEnemy = false;        
    }
}
