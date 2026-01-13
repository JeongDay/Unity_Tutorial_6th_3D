using UnityEngine;

public class PatrolState : MonoBehaviour, IState
{
    public void StateEnter()
    {
        Debug.Log("정찰 시작");
    }

    public void StateUpdate()
    {
        Debug.Log("정찰중");
    }

    public void StateExit()
    {
        Debug.Log("정찰 종료");
    }
}