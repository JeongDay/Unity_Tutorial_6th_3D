using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : SingletonCore<QuestManager>, ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    [SerializeField] private Button[] questButtons;
    [SerializeField] private QuestData[] datas;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < questButtons.Length; i++)
        {
            int j = i;
            questButtons[i].onClick.AddListener(() => SetButton(j));
        }
    }

    private void SetButton(int index)
    {
        Quest quest = new Quest(datas[index]);
        questButtons[index].gameObject.SetActive(false);
    }
    
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log($"퀘스트 {observer.QuestName}을 등록하였습니다.");
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
        Debug.Log($"퀘스트 {observer.QuestName}을 삭제하였습니다.");
    }

    public void NotifyListener(string questName)
    {
        for (int i = observers.Count -1; i >= 0; i--) // 역순 실행
            observers[i].Notify(questName);
    }
}