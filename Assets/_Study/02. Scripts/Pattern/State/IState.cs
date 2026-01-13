using UnityEngine;

public interface IState
{
    void StateEnter(); // 상태 시작
    void StateUpdate(); // 상태 진행중
    void StateExit(); // 상태 종료
}