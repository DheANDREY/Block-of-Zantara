using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Boss : MonoBehaviour
{
    public GameObject boss;
    public Vector3 posisi;
    // Start is called before the first frame update
    void Start()
    {
        GameObject prefabSpawned = Instantiate(boss, posisi, Quaternion.identity);
        prefabSpawned.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
