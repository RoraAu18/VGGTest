using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]

public class Spawner<T> where T : Component
{
    public Pool<T> pool = new Pool<T>();
    public bool shouldSpawnOverTime = true;
    public float initialSpawnDelay = 1;
    public float spawnRate = 3;
    public float randomFactor = 1;
    public Collider spawnArea;

    public float timer;
    private float nextSpawnTime = 3;
    public bool initialDelayDone = false;
    private iSpawnerUsers<T> owner;

    public void Init(iSpawnerUsers<T> _owner)
    {
        owner = _owner;
        pool.Init();
    }
    public void SpawnOverTime()
    {
        timer += Time.deltaTime;
        if (!initialDelayDone)
        {
            if (timer < initialSpawnDelay) return;
            initialDelayDone = true;
            timer = 0;
        }
        if (timer >= nextSpawnTime)
        {
            SpawnNewItem();
            timer = 0;
            GetNextSpawnTime();
        }
    }
    public void SpawnNewItem()
    {
        var newItem = pool.GetItem();
        if (spawnArea != null)
        {
            var center = owner.transform.position;
            var upLimit = center + spawnArea.bounds.extents;
            var downLimit = center - spawnArea.bounds.extents;
            var randomPos = center;
            randomPos.x += Random.Range(downLimit.x, upLimit.x);
            randomPos.z += Random.Range(downLimit.z, upLimit.z);
            newItem.transform.position = randomPos;
        }
        newItem.gameObject.SetActive(true);
        owner.OnSpawnedCustomizable(newItem, pool);
    }

    private void GetNextSpawnTime()
    {
        nextSpawnTime = spawnRate + Random.Range(-randomFactor, randomFactor);
    }

}

public interface iSpawnerUsers<T> where T : Component
{
    public Transform transform
    {
        get;
    }
    public void OnSpawnedCustomizable(T newItem, Pool<T> pool);
}

