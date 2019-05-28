using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public int ProdAmount = 1;
    public float ProdTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ProduceFood");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ProduceFood()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(ProdTime);
            ResourceManager.me.IncreaseResources("food", ProdAmount);
        }
    }
}
