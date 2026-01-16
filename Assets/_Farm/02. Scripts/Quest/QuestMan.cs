using UnityEngine;

public class QuestMan : MonoBehaviour, ITriggerEvent
{
    [SerializeField] private GameObject questUI;
    
    public void InteractionEnter()
    {
        questUI.SetActive(true);
    }

    public void InteractionExit()
    {
        questUI.SetActive(false);
    }
}