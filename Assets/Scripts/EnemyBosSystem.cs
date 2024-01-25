using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosSystem : MonoBehaviour
{
    [SerializeField] private int enemyIndex = 0;

    public static EnemyBosSystem instance;
    private void Awake()
    {
        instance = this;
    }
    public bool isChangeEnemy;
    private BosQueue BQ;
    private BossController boss;
    private EnemyController minion;
    private void Start()
    {
        //Debug.Log(SelectArcMode.instance.selectedLevel);
        //minion = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyController>();
        //boss = GameObject.FindGameObjectWithTag("enemy").GetComponent<BossController>();
        BQ = SelectArcMode.instance.BQ;
        //GameObject prefab1 = Instantiate(BQ.enemy[enemyIndex], new Vector3(6.61f, 16.71f, -1f), Quaternion.identity);

        GameObject prefab1 = Instantiate(BQ.enemy[enemyIndex], transform.position, Quaternion.identity);
        //Debug.Log(prefab1.transform.position);

        prefab1.transform.SetParent(transform);
    }
    void Update()
    {
        NextEnemy();
    }

    private void SpawnEnemy()
    {

        GameObject prefabSpawned = Instantiate(BQ.enemy[enemyIndex], transform.position, Quaternion.identity);
        prefabSpawned.transform.SetParent(transform);
    }

    private void NextEnemy()
    {
        //Ganti game object setiap kali pemain menekan tombol "Space"
        if (isChangeEnemy)
        {
            enemyIndex++;
            if (enemyIndex < BQ.enemy.Length)
            {
                //enemyIndex = -1;
                SpawnEnemy();
                isChangeEnemy = false;
                if (enemyIndex == BQ.enemy.Length - 1)
                {
                    if (boss != null)
                    {
                        AudioFile_Handler.instance.PlayBGM_Utama(false);
                        AudioFile_Handler.instance.PlayBGM_Boss(true);
                    }
                    else if (minion != null)
                    {
                        Debug.Log("Tetap Music Utama");
                    }
                    
                }
            }
            else
            {
                GameFlow.instance.OpenWinPanel();
            }
            //Debug.Log(randomIndex);
        }
    }
    //public GameObject bossBGM;
    public void DestroyBGM()
    {
        //GameObject bgm = GameObject.FindGameObjectWithTag("BGM");

    }
}
