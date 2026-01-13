using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int arrayPos;

    private bool isCreate = false;
    
    public void CreateCrop(GameObject cropPrefab) // 타일의 자식으로 작물 생성 기능
    {
        if (isCreate)
            return;

        GameObject cropObj = Instantiate(cropPrefab);
        // GameObject cropObj = PoolManager.Instance.pool.Get();
        
        cropObj.transform.SetParent(transform);
        cropObj.transform.localPosition = Vector3.zero;

        float randomY = Random.Range(0, 360);
        Vector3 randomRot = new Vector3(0, randomY, 0);
        cropObj.transform.localRotation = Quaternion.Euler(randomRot);

        isCreate = true;
    }
}