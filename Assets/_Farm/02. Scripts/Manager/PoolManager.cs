using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [Serializable]
    public class PoolData
    {
        public string name;
        public GameObject prefab;
    }

    public List<PoolData> poolList = new List<PoolData>();

    // 이름으로 Pool 검색
    private Dictionary<string, IObjectPool<GameObject>> poolDics = new Dictionary<string, IObjectPool<GameObject>>();

    void Awake()
    {
        // Pool을 생성하고 Dictionary에 등록하는 기능
        foreach (var poolData in poolList)
        {
            poolDics[poolData.name] = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(poolData.prefab), // 생성하는 기능
                actionOnGet: (obj) => obj.SetActive(true), // 꺼내는 기능
                actionOnRelease: (obj) => obj.SetActive(false)); // 넣는 기능
        }
    }

    public GameObject GetObject(string key)
    {
        if (poolDics.ContainsKey(key))
        {
            GameObject obj = poolDics[key].Get();
            
            return obj;
        }

        Debug.LogError($"Pool {key} is not found");
        return null;
    }

    public void ReleaseObject(string key, GameObject obj)
    {
        if (poolDics.ContainsKey(key))
            poolDics[key].Release(obj);
        else
            Debug.LogError($"Pool {key} is not found");
    }
}