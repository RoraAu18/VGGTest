using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class DataManager
{
    public void SaveData<T>(string id, T data)
    {
        var toSave = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(id, toSave);
        Debug.Log(id + " saving " + toSave);
    }

    public T GetData<T>(string id) where T : new()
    {
        if (!PlayerPrefs.HasKey(id))
        {
            return new T();
        }
        var jsonData = PlayerPrefs.GetString(id);
        T fileData = JsonUtility.FromJson<T>(jsonData);
        Debug.Log(id + " getting " + fileData);
        return fileData;
    }
}
