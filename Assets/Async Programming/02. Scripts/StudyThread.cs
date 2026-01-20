using System.Threading;
using UnityEngine;

public class StudyThread : MonoBehaviour
{
    void Start()
    {
        Thread t = new Thread(SubThread);
        t.IsBackground = true;
        
        t.Start();
        
        t.Join();
        Debug.Log("Main Thread 종료");
    }
    
    private void SubThread()
    {
        Debug.Log("Sub Thread 실행");
        
        Thread.Sleep(5000); // 5초
        Debug.Log("Sub Thread 종료");
    }
}