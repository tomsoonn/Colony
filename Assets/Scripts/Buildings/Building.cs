using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string name;
    public int foodCost, woodCost, stoneCost, goldCost;
    public Sprite buildingSprite;
    SpriteRenderer sr;

    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string CostString()
    {
        return String.Format("Cost\nFood: {0}\nWood: {1}\nStone: {2}\nGold: {3}", foodCost, woodCost, stoneCost, goldCost);
    }
}
