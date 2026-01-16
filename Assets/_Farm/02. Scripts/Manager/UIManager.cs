using UnityEngine;

public class UIManager : SingletonCore<UIManager>
{
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private GameObject inventoryUI;

    public void InventoryOnOff()
    {
        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);
    }
    
    public void AllPopUpClose()
    {
        foreach (var popUp in popUps)
            popUp.SetActive(false);
    }
}