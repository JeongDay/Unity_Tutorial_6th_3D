using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour, ITriggerEvent
{
    private Animator anim;
    private NavMeshAgent agent;

    [SerializeField] private float wanderRadius = 15f;
    
    private float minWaitTime = 1f, maxWaitTime = 5f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            SetRandomDestination();
            anim.SetBool("IsWalk", true);
                                            // 길 찾기 종료                    남아있는 거리와 정지 거리 비교
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
            
            anim.SetBool("IsWalk", false);
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // 동물의 반경 안에서 랜덤한 위치로 목적지를 설명 및 이동하는 기능
    private void SetRandomDestination()
    {
        var randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
    
    public void InteractionEnter()
    {
        AnimalArea.failAction?.Invoke();
    }

    public void InteractionExit() { }
}