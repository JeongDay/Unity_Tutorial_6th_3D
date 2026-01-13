using UnityEngine;

public class PoolController : MonoBehaviour
{
    public UnityPoolManager poolManager;
    public Transform shootPoint;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = poolManager.pool.Get();
            
            bullet.transform.position = shootPoint.position;
            
            // 총알 발사 기능
        }
    }
}