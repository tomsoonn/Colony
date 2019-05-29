using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Worker";
        StartCoroutine("EatFood");
    }

}
