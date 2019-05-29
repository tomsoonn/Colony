using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawTime = 10.0f;
    public int population;
    public GameObject enemy;
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(spawTime);
            population = UnitsManager.me.currentPoplualtion / 2;
            for (int i = 0; i < population; i++)
            {
                GameObject built = (GameObject)Instantiate(enemy, enemy.transform.position, Quaternion.Euler(0, 0, 0));
                built.SetActive(true);
                enemies.Add(built);
            }
            Debug.Log("Enemies Created: " + population);
        }
    }
}
