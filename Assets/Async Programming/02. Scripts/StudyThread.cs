using System.Threading;
using UnityEngine;

public class StudyThread : MonoBehaviour
{
    void Start()
    {
        Thread subThread = new Thread(MethodA);
        subThread.IsBackground = true; // 유니티를
        
        subThread.Start();

        subThread.Join(); // Thread가 완료될 때까지 대기 -> 동기

        Debug.Log("Main Thread 종료");
    }

    private void MethodA()
    {
        Debug.Log("Sub Thread 실행");
        Thread.Sleep(5000); // 5초 멈춤
        
        Debug.Log("Sub Thread 완료");
    }
}