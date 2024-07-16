using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBlock : MonoBehaviour
{
    public GameObject[] blockPrefab;   
    public Transform spawnPos;
    public static SpawnerBlock instance;
    private void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBlock();
    }
    public void SpawnNewBlock()
    {
        Invoke("SpawnPrefab", 1/2f);        
    }

    public bool isBlockSpawned;
   
    public void SpawnPrefab()
    {
        GameObject prefabSpawned = Instantiate(blockPrefab[NextBlock.randomIndex], transform.position, Quaternion.identity);
        //GameObject bP = Instantiate(blockPrefab[9], new Vector3(0, 0), Quaternion.identity);
        prefabSpawned.transform.SetParent(spawnPos);
        //bP.transform.SetParent(spawnPos);
        isBlockSpawned = true;
    }

    public void SpawnBlockerSide(bool isOn)
    {
        if (isOn)
        {
            blockPrefab[9].SetActive(true);
        }
        else
        {
            blockPrefab[9].SetActive(false);
        }
    }

    public GameObject[] bHole;
    public int numbBHole;
    public void EnableBlackHole(bool isLeft)
    {
        if (isLeft)
        {
            int a = Random.Range(0, 2);
            numbBHole = a;
            if (a == 0)
            {
                bHole[0].SetActive(true);
            }
            else
            {
                bHole[1].SetActive(true);
            }
        }
        else
        {
            int b = Random.Range(0, 2);
            numbBHole = b+3;
            if (b == 0)
            {
                bHole[2].SetActive(true);
            }
            else
            {
                bHole[3].SetActive(true);
            }
        }
    }
    public void DisableBlackHole()
    {
        bHole[0].SetActive(false); bHole[2].SetActive(false);
        bHole[1].SetActive(false); bHole[3].SetActive(false);
    }
}
