using UnityEngine;

public interface IObserver
{
    string QuestName { get; }
    int CurrentCount { get; }
    bool IsCompleted { get; }
    
    void Notify(string questName);
}