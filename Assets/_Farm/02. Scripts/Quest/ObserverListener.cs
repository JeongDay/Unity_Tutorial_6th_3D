using UnityEngine;

public class ObserverListener : MonoBehaviour, IObserver
{
    private ISubject subject;

    void Awake()
    {
        subject = GetComponent<ISubject>();
    }

    void OnEnable()
    {
        subject.AddObserver(this);
    }

    void Disable()
    {
        subject.RemoveObserver(this);
    }
    
    public void Notify()
    {
        Debug.Log("점수 10점 획득");
    }
}