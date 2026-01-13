using UnityEngine;

public class IdleState : MonoBehaviour, IState
{
    public void StateEnter()
    {
        Debug.Log("대기 시작");
    }

    public void StateUpdate()
    {
        Debug.Log("대기중");
    }

    public void StateExit()
    {
        Debug.Log("대기 종료");
    }
}