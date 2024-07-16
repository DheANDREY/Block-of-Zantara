using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMech : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private GameObject descSkillChar;
    private void OnMouseDown()
    {
        descSkillChar.SetActive(true);
    }
    private void OnMouseUp()
    {
        descSkillChar.SetActive(false);
    }
}
