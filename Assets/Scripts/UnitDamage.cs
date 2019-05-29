using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDamage : MonoBehaviour
{
    public float DeltaDistance = 3f;
    public float AttackStrength = 0.3f;
    public float AttackSpeed = 0.1f;
    private float _partAttack;

    private void Start()
    {
        StartCoroutine("Attack");
    }

    private IEnumerator Attack()
    {
        for (;;)
        {
            yield return new WaitForSeconds(AttackSpeed);
            var opponent = GetComponent<UnitSearcher>().TargetUnit;
            if (opponent 
                && Vector3.Distance(opponent.transform.position, gameObject.transform.position) <DeltaDistance)
            {
                if (_partAttack > 1)
                {
                    _partAttack -= 1;
                    opponent.GetComponent<Unit>().HP -= 1;
                }
                else
                {
                    _partAttack += AttackStrength;
                }
            }
            else
            {
                _partAttack = 0;
            }
        }
    }
}