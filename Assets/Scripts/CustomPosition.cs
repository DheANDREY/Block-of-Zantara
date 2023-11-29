using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPosition : MonoBehaviour
{
    public static CustomPosition instance;
    private void Awake()
    {
        instance = this;
    }
    public Vector3 SetPosBlock = new Vector3(0f, 0f, 0f);

    void Start()
    {
        transform.position = SetPosBlock;
    }
}
