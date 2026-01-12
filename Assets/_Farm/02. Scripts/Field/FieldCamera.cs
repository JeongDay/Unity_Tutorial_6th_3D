using UnityEngine;

public class FieldCamera : MonoBehaviour
{
    private Transform target;

    [SerializeField] private Vector3 offset, minBounds, maxBounds;
    [SerializeField] private float smoothSpeed = 5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target == null)
            return;
        
        Vector3 destination = target.position + offset; // 따라갈 포지션
        Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, destination, smoothSpeed * Time.deltaTime); // 부드럽게 이동

        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x); // x축 영역 제한
        smoothedPosition.z = Mathf.Clamp(smoothedPosition.z, minBounds.z, maxBounds.z); // z축 영역 제한
        
        transform.localPosition = smoothedPosition;
    }
}