using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Pool<T> where T : Component
{
    public Transform parent;
    public T itemPrefab;
    public int initSize;
    public List<T> itemsInPool = new List<T>();
    public List<bool> activated = new List<bool>();

    public void Init()
    {
        IncreaseingPool(initSize);
    }
    public void IncreaseingPool(int growBy)
    {
        for (int i = 0; i < growBy; i++)
        {
            var newItem = GameObject.Instantiate(itemPrefab, parent);
            itemsInPool.Add(newItem);
            activated.Add(false);
            newItem.gameObject.SetActive(false);
        }
    }
    public T GetItem()
    {
        for (int i = 0; i < itemsInPool.Count; i++)
        {
            if (activated[i] == false)
            {
                activated[i] = true;
                return itemsInPool[i];
            }
        }
        var lastItemAdded = itemsInPool.Count;
        IncreaseingPool(5);
        activated[lastItemAdded] = true;
        return itemsInPool[lastItemAdded];
    }
    public void RecycleItem(T itemToRecycle)
    {
        var index = itemsInPool.IndexOf(itemToRecycle);
        activated[index] = false;
    }
    public void RecycleAll()
    {
        for (int i = 0; i < itemsInPool.Count; i++)
        {
            RecycleItem(itemsInPool[i]);
            itemsInPool[i].gameObject.SetActive(false);
        }
    }
}
