using UnityEngine;
using UnityEngine.Pool;

public class UnityPoolManager : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    public GameObject prefab;

    void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateObject, GetObject, ReleaseObject);
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(prefab);
        Debug.Log("오브젝트 생성");
        
        return obj;
    }

    private void GetObject(GameObject obj)
    {
        obj.SetActive(true);
        Debug.Log("오브젝트 꺼내기");
    }

    private void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
        Debug.Log("오브젝트 넣기");
    }
}