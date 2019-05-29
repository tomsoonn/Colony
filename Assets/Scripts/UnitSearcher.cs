using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class UnitSearcher : MonoBehaviour
{
    public GameObject TargetUnit;


    private void Start()
    {
        StartCoroutine("Search");
    }

    private IEnumerator Search()
    {
        for (;;)
        {
            yield return new WaitForSeconds(1);
            if (TargetUnit)
            {
                var ai = GetComponent<AIDestinationSetter>();
                ai.target = TargetUnit.transform;
            }
            else
            {
                TargetUnit = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}