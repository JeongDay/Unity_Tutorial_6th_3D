using System;
using UnityEngine;

public class Fruit : MonoBehaviour, ITriggerEvent, IItem
{
    public Inventory Inven { get; private set; }
    public GameObject Obj { get; private set; }
    public string ItemName { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }

    void Awake()
    {
        Inven = FindFirstObjectByType<Inventory>();
        Obj = gameObject;

        ItemName = gameObject.name.Replace("(Clone)", "");
    }
    
    public void InteractionEnter()
    {
        Get();
    }

    public void InteractionExit() { }

    public void Get()
    {
        GameManager.Instance.Pool.ReleaseObject(ItemName, gameObject);
        Debug.Log($"{ItemName} 획득");
        
        Inven.GetItem(this);
    }

    public void Use()
    {
        Debug.Log($"{ItemName} 사용");
    }
}