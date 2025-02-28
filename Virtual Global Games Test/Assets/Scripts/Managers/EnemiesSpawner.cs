using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour, iSpawnerUsers<EnemyController>
{
    public Spawner<EnemyController> spawner;

    void Start()
    {
        spawner.Init(this); 
    }

    void Update()
    {
        spawner.SpawnOverTime();
    }
    public void OnSpawnedCustomizable(EnemyController newItem, Pool<EnemyController> pool)
    {
        newItem.Init(pool);
    }
    public void RestartSpawner()
    {
        spawner.pool.RecycleAll();
    }
}
