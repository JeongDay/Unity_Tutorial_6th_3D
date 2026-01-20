using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class StudyLock : MonoBehaviour
{
    private readonly object obj = new object();

    async void Start()
    {
        Debug.Log("테스트 시작");

        Task t1 = Task.Run(() => SubThread("T1"));
        Task t2 = Task.Run(() => SubThread("T2"));
        
        await Task.WhenAll(t1, t2);

        Debug.Log("메인 쓰레드 종료");
    }

    private void SubThread(string msg)
    {
        lock (obj)
        {
            Debug.Log($"{msg} 쓰레드 시작");
            Thread.Sleep(500);
            
            Debug.Log($"{msg} 쓰레드 진행중");
            Thread.Sleep(500);
            
            Debug.Log($"{msg} 쓰레드 종료");
        }
    }
}