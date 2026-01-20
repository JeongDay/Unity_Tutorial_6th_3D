using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class StudyTask : MonoBehaviour
{
    private int x, y, r1, r2;
    private int count = 0;

    async void Start()
    {
        Debug.Log("테스트 시작");

        await Task.Run(() =>
        {
            while (true)
            {
                count++;

                x = y = r1 = r2 = 0;

                Task t1 = Task.Run(SubThread1);
                Task t2 = Task.Run(SubThread2);

                Task.WaitAll(t1, t2);

                if (r1 == 0 && r2 == 0)
                    break;
            }
        });

        Debug.Log($"r1 = 0, r2 = 0 현상 발생 Count : {count}");
    }

    private void SubThread1()
    {
        y = 1;
        r1 = x;
    }

    private void SubThread2()
    {
        x = 1;
        r2 = y;
    }
}