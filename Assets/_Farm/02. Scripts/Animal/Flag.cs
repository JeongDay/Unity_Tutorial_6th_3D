using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Vector3 offsetPos;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.SetParent(other.transform);
            transform.localPosition = offsetPos;
            transform.localRotation = Quaternion.identity;
        }
    }
}