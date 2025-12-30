using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshSurface surface;

    public float bakeDistance = 10f;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        surface.transform.position = transform.position;
        surface.BuildNavMesh();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        agent.SetDestination(transform.position + dir);

        float dist = Vector3.Distance(transform.position, surface.transform.position);
        if (dist > bakeDistance)
        {
            surface.transform.position = transform.position;
            surface.BuildNavMesh();
        }
    }
}