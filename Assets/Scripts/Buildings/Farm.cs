using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public int prodAmount = 1;
    public float prodTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("produceFood");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator produceFood()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(prodTime);
            ResourceManager.me.IncreaseResources("food", prodAmount);
        }
    }
}
