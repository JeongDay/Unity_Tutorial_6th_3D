using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public enum CropState { Level1, Level2, Level3 }
    public CropState cropState;

    [SerializeField] private CropData data;

    private float growthTime;
    public bool isHarvest;

    void Awake()
    {
        growthTime = data.growthTime;
    }

    void OnEnable()
    {
        isHarvest = false;
        SetState(CropState.Level1);
        
        StartCoroutine(GrowthRoutine());
    }

    IEnumerator GrowthRoutine()
    {
        yield return new WaitForSeconds(growthTime);
        SetState(CropState.Level2);
        
        yield return new WaitForSeconds(growthTime);
        SetState(CropState.Level3);
        
        isHarvest = true;
    }

    private void SetState(CropState newState)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == (int)newState);
    }
}