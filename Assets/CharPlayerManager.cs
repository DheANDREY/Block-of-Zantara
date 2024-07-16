using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPlayerManager : MonoBehaviour
{
    public GameObject[] charPlayer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnChar();
    }

    private void SpawnChar()
    {        
        int numbCharP = SelectArcMode.instance.selectedChar;
        Debug.Log("Char Choosen:" + numbCharP);
        GameObject char1 = Instantiate(charPlayer[numbCharP], transform.position, Quaternion.identity);
        char1.transform.SetParent(transform);
    }
}
