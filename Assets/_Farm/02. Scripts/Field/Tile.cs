using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int arrayPos;

    private GameObject cropObj;
    private GameObject fruitPrefab;

    private int maxFruitCount;
    private bool isCreate = false;

    #region 작물 심기
    public void CreateCrop(GameObject cropPrefab) // 타일의 자식으로 작물 생성 기능
    {
        if (isCreate)
            return;

        cropObj = PoolManager.Instance.GetObject(cropPrefab.name);

        cropObj.transform.SetParent(transform);
        cropObj.transform.localPosition = Vector3.zero;

        float randomY = Random.Range(0, 360);
        Vector3 randomRot = new Vector3(0, randomY, 0);
        cropObj.transform.localRotation = Quaternion.Euler(randomRot);

        isCreate = true;

        cropObj.GetComponent<Crop>().SetCropData(out fruitPrefab, out maxFruitCount);
    }

    #endregion

    #region 작물 수확하기
    public void HarvestCrop()
    {
        if (isCreate)
        {
            Crop crop = cropObj.GetComponent<Crop>();
            if (crop.cropState == Crop.CropState.Level3)
            {
                isCreate = false;
                
                string cropName = cropObj.name.Replace("(Clone)", "");
                PoolManager.Instance.ReleaseObject(cropName, cropObj);

                StartCoroutine(HarvestRoutine());
            }
        }
    }

    IEnumerator HarvestRoutine()
    {
        int randomAmount = Random.Range(1, maxFruitCount);

        for (int i = 0; i < randomAmount; i++)
        {
            GameObject fruitObj = PoolManager.Instance.GetObject(fruitPrefab.name);
            
            fruitObj.transform.position = transform.position + Vector3.up * 0.5f;
            Rigidbody fruitRb = fruitObj.GetComponent<Rigidbody>();

            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            Vector3 forceDir = new Vector3(randomX, 3f, randomZ);
            fruitRb.AddForce(forceDir, ForceMode.Impulse);

            yield return new WaitForSeconds(0.25f);
        }
    }

    #endregion
}