using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name;
    public int foodCost, woodCost, stoneCost, goldCost;
    public Sprite buildingSprite;
    SpriteRenderer sr;

    public int HP = 10;
    public int EatAmount = 1;
    public float EatTime = 5.0f;
    public int NoFoodHPPenalty = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EatFood");
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EatFood()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(EatTime);
            if (ResourceManager.me.CheckResourceAmount("food", EatAmount))
            {
                ResourceManager.me.ReduceResources("food", EatAmount);

            }
            else
            {
                HP = HP - NoFoodHPPenalty;
            }
        }
    }
}
