using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();

    private int score;
    
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyListener()
    {
        foreach (var observer in observers)
        {
            observer.Notify();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score++;
            Debug.Log("점수 획득");
            
            if (score >= 10)
            {
                score = 0;
                NotifyListener();
            }
        }
    }
}