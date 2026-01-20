using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> popUps = new List<GameObject>();
    [SerializeField] private GameObject inventoryUI;

    void Start()
    {
        popUps.Add(transform.Find("Popup/Inventory").gameObject);
        popUps.Add(transform.Find("Popup/Storage").gameObject);
        popUps.Add(transform.Find("Popup/Quest").gameObject);

        inventoryUI = popUps[0];
    }
    
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