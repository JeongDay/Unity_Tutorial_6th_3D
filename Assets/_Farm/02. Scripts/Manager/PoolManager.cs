using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : SingletonCore<PoolManager>
{
    public ObjectPool<GameObject> pool;
    public GameObject prefab;

    protected override void Awake()
    {
        base.Awake();
        
        pool = new ObjectPool<GameObject>(CreateObject, GetObject, ReleaseObject);
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(prefab);

        return obj;
    }

    private void GetObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}