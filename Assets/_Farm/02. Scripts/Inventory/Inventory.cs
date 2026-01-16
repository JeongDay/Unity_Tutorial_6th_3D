using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Slot[] slots;
    
    public void GetItem(IItem item)
    {
        string questName = item.ItemName.Replace("_Fruit", ""); // Carrot_Fruit
        QuestManager.Instance.NotifyListener(questName);
        
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                return;
            }
        }
    }
}