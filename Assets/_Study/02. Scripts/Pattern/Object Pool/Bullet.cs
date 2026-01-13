using UnityEngine;

public class Bullet : MonoBehaviour
{
    private UnityPoolManager poolManager;

    void Awake()
    {
        poolManager = FindFirstObjectByType<UnityPoolManager>();
    }
    
    void OnEnable()
    {
        Invoke("ReturnPool", 3f);
    }

    private void ReturnPool()
    {
        poolManager.pool.Release(gameObject);
    }
}