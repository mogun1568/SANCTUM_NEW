using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, Data.Item> ItemDict { get; private set; } = new Dictionary<string, Data.Item>();

    public void Init()
    {
        ItemDict = LoadJson<Data.ItemData, string, Data.Item>("ItemData").MakeDict();
    }

    // �� �κ� �� �𸣰���
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        //Debug.Log(textAsset.text);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
