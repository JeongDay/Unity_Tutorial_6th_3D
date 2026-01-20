using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StudyUniTaskLock : MonoBehaviour
{
    private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1); // 1명 들어갈 수 있도록 설정 -> 열쇠

    async void Start()
    {
        Debug.Log("테스트 시작");

        UniTask t1 = UniTask.Run(() => SubMethod("T1"));
        UniTask t2 = UniTask.Run(() => SubMethod("T2"));
    }

    private async UniTaskVoid SubMethod(string msg)
    {
        await _lock.WaitAsync(); // 열쇠가 없으면 비동기 대기

        try
        {
            Debug.Log($"{msg} 쓰레드 실행");
            await UniTask.Delay(500);
            
            Debug.Log($"{msg} 쓰레드 실행중");
            await UniTask.Delay(500);
            
            Debug.Log($"{msg} 쓰레드 종료");
        }
        finally
        {
            _lock.Release();
        }
    }
}