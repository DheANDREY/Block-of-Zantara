using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalFlow : MonoBehaviour
{
    [SerializeField] private block[] _block;
    [SerializeField] private int nextScoreMilestone;
    [SerializeField] private float minboostTimeSpd;

    private void Start()
    {
        foreach (var _blocks in _block)
        {
            _blocks.fallTime = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameFlow.pointScore > nextScoreMilestone)
        {
            foreach (var _blocks in _block)
            {
                _blocks.fallTime = _blocks.fallTime - minboostTimeSpd;
            }
            Debug.Log("Milestone Score= " + nextScoreMilestone+1250);
            nextScoreMilestone += 1250;
        }
    }
}
