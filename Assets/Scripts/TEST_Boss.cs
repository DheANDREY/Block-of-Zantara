using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Boss : MonoBehaviour
{
    //public GameObject[] enemy;
    //public BosQueue BQ;
    [SerializeField]private int enemyIndex = 0;

    public static TEST_Boss instance;
    private void Awake()
    {
        instance = this;
    }
    public bool isChangeEnemy;
    private BosQueue BQ;
    private void Start()
    {
        Debug.Log(enemyIndex);
        BQ = SelectArcMode.instance.BQ;
        GameObject prefab1 = Instantiate(BQ.enemy[enemyIndex], new Vector3(6.61f, 16.71f, -1f), Quaternion.identity);
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
                if(enemyIndex == BQ.enemy.Length - 1)
                {
                    AudioFile_Handler.instance.PlayBGM_Main(false);
                    AudioFile_Handler.instance.PlayBGM_Boss(true);
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
