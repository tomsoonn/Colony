using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    public int MaxHealth = 10;
    public int Health = 10;

    void Update()
    {
        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }
}