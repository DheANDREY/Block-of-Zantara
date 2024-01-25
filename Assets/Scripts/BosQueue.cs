using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BosQueue", menuName = "Enemies")]
public class BosQueue : ScriptableObject
{
    public static BosQueue instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] enemy;
}
