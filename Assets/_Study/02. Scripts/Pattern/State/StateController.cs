using UnityEngine;

public class StateController : MonoBehaviour
{
    private IState currentState;

    private IState idle, patrol, trace, attack;

    private CharacterController cc;
    private Animator anim;
    [SerializeField] private GameObject prefab;

    void Awake()
    {
        cc =  GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
        idle = gameObject.AddComponent<IdleState>();
        patrol = gameObject.AddComponent<PatrolState>();
        trace = gameObject.AddComponent<TraceState>();
        attack = gameObject.AddComponent<AttackState>();
    }
    
    void Start()
    {
        currentState = idle;
    }

    void Update()
    {
        currentState?.StateUpdate();

        if (Input.GetKeyDown(KeyCode.Q))
            SetState(idle);
        else if (Input.GetKeyDown(KeyCode.W))
            SetState(patrol);
        else if (Input.GetKeyDown(KeyCode.E))
            SetState(trace);
        else if (Input.GetKeyDown(KeyCode.R))
            SetState(attack);
    }

    public void SetState(IState newState)
    {
        if (currentState != newState)
        {
            currentState?.StateExit(); // 기존 상태의 Exit
            
            currentState = newState; // 상태 변경
            
            currentState?.StateEnter(); // 새로운 상태의 Enter
            
        }
    }
}