using UnityEngine;

public class Flag : MonoBehaviour
{
    public static bool IsHasFlag = false;
    
    [SerializeField] private Vector3 offsetPos;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsHasFlag = true;
            transform.SetParent(other.transform);
            transform.localPosition = offsetPos;
            transform.localRotation = Quaternion.identity;
        }
    }
}