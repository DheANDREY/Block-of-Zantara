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
        SpawnNewPrefab();
    }
    public void SpawnNewPrefab()
    {
        Invoke("SpawnPrefab", 1/2f);        
    }
    public bool isBlockSpawned;
   
    public void SpawnPrefab()
    {

        GameObject prefabSpawned = Instantiate(blockPrefab[NextBlock.randomIndex], transform.position, Quaternion.identity);
        prefabSpawned.transform.SetParent(spawnPos);
        isBlockSpawned = true;
    }
}
