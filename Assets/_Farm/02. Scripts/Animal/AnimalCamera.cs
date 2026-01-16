using UnityEngine;

public class AnimalCamera : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}