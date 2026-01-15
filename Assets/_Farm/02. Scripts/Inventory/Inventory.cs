using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Slot[] slots;
    
    public void GetItem(IItem item)
    {
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