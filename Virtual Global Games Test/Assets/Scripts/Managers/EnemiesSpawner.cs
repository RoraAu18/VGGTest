using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour, iSpawnerUsers<EnemyController>
{
    public Spawner<EnemyController> spawner;
    public GameManager gameManager => GameManager.Instance;
    public UIManager uiManager=> UIManager.Instance;
    void Start()
    {
        spawner.Init(this); 
    }

    void Update()
    {
        if (gameManager.enemiesSpawned >= gameManager.totalEnemiesPerLevel) return;
        spawner.SpawnOverTime();
    }
    public void OnSpawnedCustomizable(EnemyController newItem, Pool<EnemyController> pool)
    {
        gameManager.enemiesCount++;
        gameManager.enemiesSpawned++;
        uiManager.SetEnemiesAmount(gameManager.enemiesCount);
        newItem.Init(pool);
    }
    public void RestartSpawner()
    {
        spawner.pool.RecycleAll();
    }
}
