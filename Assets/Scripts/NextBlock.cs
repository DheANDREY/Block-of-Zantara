using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlock : MonoBehaviour
{
    public GameObject[] nextPrefabBlock;
    [SerializeField] private GameObject instantiatedPrefab;
    public SpawnerBlock sB;
    public int numbArrayBlock;
    public static int randomIndex;
    // Start is called before the first frame update
    void Start()
    {
        randomIndex = Random.Range(0, nextPrefabBlock.Length-1);
        Debug.Log("Spawner: " + randomIndex);
    }
    
    // Update is called once per frame
    void Update()
    {
        changeBlock();
    }

    private int currentIndex = 0;
    public Transform nextBlockSlot;

    public void SpawnBlockNext()
    {
        //Debug.Log(SkillBoss1.isMonsterSpawned);
        if (!SkillBoss1.isMonsterSpawned)
        {
            randomIndex = Random.Range(0, nextPrefabBlock.Length - 1);
        }
        else
        {
            randomIndex = 8;
        }        
        //Debug.Log("Spawner Blocknya: " + randomIndex);
        instantiatedPrefab = Instantiate(nextPrefabBlock[randomIndex], new Vector3(nextBlockSlot.position.x, (float)(nextBlockSlot.position.y - 0.35), 0), Quaternion.identity) as GameObject;
        instantiatedPrefab.transform.SetParent(transform);
    }
    private void changeBlock()
    {
        //Ganti game object setiap kali pemain menekan tombol "Space"
        if (SpawnerBlock.instance.isBlockSpawned)
        {
            Destroy(instantiatedPrefab);
            currentIndex++;
            if (currentIndex >= nextPrefabBlock.Length-1)
            {
                currentIndex = -1;
            }
            SpawnBlockNext();
            SkillBoss1.isMonsterSpawned = false;
            SpawnerBlock.instance.isBlockSpawned = false;
            //Debug.Log(randomIndex);
        }
    }
}
