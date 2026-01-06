using UnityEngine;

public class TrailCircle : MonoBehaviour
{
    private TrailRenderer trail;

    private TrailRenderer breakTrail1;
    private TrailRenderer breakTrail2;

    public float timer = 1f;
    public float speed = 3f;
    public float radius = 5f;
    private float theta;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        trail.time = timer;
        theta += speed * Time.deltaTime;
        transform.position = radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
    }
}