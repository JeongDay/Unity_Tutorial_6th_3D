using UnityEngine;

public class AvoidObstaclesMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mass = 5f;
    public float force = 50f;
    public float minDistToAvoid = 5f;

    private Vector3 targetPoint;
    public float steeringForce = 10f;

    public LayerMask obstacleMask;

    void Start()
    {
        targetPoint = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                targetPoint = hit.point; // 목적지 설정
        }

        Vector3 dir = (targetPoint - transform.position).normalized; // 목적지로 이동하려는 방향

        dir = GetAvoidanceDirection(dir); // 장애물이 있는지 확인

        if (Vector3.Distance(targetPoint, transform.position) < 1f) // Stop Distance
            return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, steeringForce * Time.deltaTime);
    }

    // 장애물을 만났을 때 이동 방향을 변경하는 기능
    private Vector3 GetAvoidanceDirection(Vector3 dir)
    {
        RaycastHit hit;
        
        // 정면을 향해 레이캐스트 발사 + 장애물 레이어 확인
        if (Physics.Raycast(transform.position, transform.forward, out hit, minDistToAvoid, obstacleMask))
        {
            Vector3 hitNormal = hit.normal; // 부딪힌 곳의 수직 벡터
            hitNormal.y = 0f;

            dir = transform.forward + hitNormal * force;
            dir.Normalize();
        }

        return dir;
    }
}