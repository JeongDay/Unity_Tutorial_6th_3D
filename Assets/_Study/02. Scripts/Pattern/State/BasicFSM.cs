using System;
using UnityEngine;

public class BasicFSM : MonoBehaviour
{
    public enum MonsterState { Idle, Patrol, Trace, Attack }
    public MonsterState monsterState = MonsterState.Idle;

    void Update()
    {
        switch (monsterState) // 상태에 따른 기능 실행
        {
            case MonsterState.Idle:
                Debug.Log("Idle : 몬스터 대기중");
                break;
            case MonsterState.Patrol:
                Debug.Log("Patrol : 몬스터 정찰중");
                break;
            case MonsterState.Trace:
                Debug.Log("Trace : 몬스터 추격중");
                break;
            case MonsterState.Attack:
                Debug.Log("Attack : 몬스터 공격중");
                break;
        }
    }

    public void SetState(MonsterState newState) // 상태 변경
    {
        if (monsterState != newState)
            monsterState = newState;
    }
}