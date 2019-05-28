using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int eatAmount = 1;
    public float eatTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("eatFood");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator eatFood()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(eatTime);
            ResourceManager.me.ReduceResources("food", eatAmount);
        }
    }
}
