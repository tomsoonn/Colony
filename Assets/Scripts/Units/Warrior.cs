using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit
{
    public float DeltaDistance = 3f;
    public int AttackDamage = 1;
    public float AttackTime=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        name = "Warrior";
        StartCoroutine("Attack");
        StartCoroutine("EatFood");
    }

    private IEnumerator Attack()
    {
        for (;;)
        {
            yield return new WaitForSeconds(AttackTime);
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < DeltaDistance)
                {
                    enemy.GetComponent<HealthPoints>().Health -= 1;
                    Debug.Log("Attacking");
                    break;
                }
            }
        }
    }
}