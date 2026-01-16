using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private IItem item;

    [SerializeField] private Button slotButton;
    [SerializeField] private Image slotImage;

    [SerializeField] private Image dragItem;
    private static Slot dragSlot;

    public bool IsEmpty { get; private set; } = true;

    void Awake()
    {
        slotButton.onClick.AddListener(UseItem);
    }

    void OnEnable()
    {
        slotImage.gameObject.SetActive(!IsEmpty); // 비워있지 않다면 켜기
        slotButton.interactable = !IsEmpty;
    }

    public void AddItem(IItem item)
    {
        IsEmpty = false;
        this.item = item;
        slotImage.sprite = item.Icon;

        slotImage.gameObject.SetActive(true);
        slotButton.interactable = true;
    }
    
    private void UseItem()
    {
        if (item == null)
            return;

        item.Use();
        item = null;
        IsEmpty = true;
        slotImage.gameObject.SetActive(false);
        slotImage.sprite = null;
        slotButton.interactable = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty)
            return;

        dragSlot = this;
        dragItem.sprite = item.Icon;
        dragItem.gameObject.SetActive(true);
        dragItem.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragItem.transform.position = eventData.position; // 마우스 위치로 드래그 아이템 이동
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 아이템 정보 Swap
        IItem tempItem = this.item;
        SetItem(dragSlot.item);
        dragSlot.SetItem(tempItem);

        Debug.Log("아이템 이동 완료");

        dragItem.sprite = null;
        dragItem.gameObject.SetActive(false);
        dragSlot = null;
    }
    
    public void SetItem(IItem newItem)
    {
        this.item = newItem;
        if (newItem == null)
        {
            IsEmpty = true;
            slotImage.sprite = null;
            slotImage.gameObject.SetActive(false);
            slotButton.interactable = false;
        }
        else
        {
            IsEmpty = false;
            slotImage.sprite = newItem.Icon;
            slotImage.gameObject.SetActive(true);
            slotButton.interactable = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            DropItemToWorld();
        
        dragItem.sprite = null;
        dragItem.gameObject.SetActive(false);
        dragSlot = null;

        dragItem.raycastTarget = true;
    }

    private void DropItemToWorld()
    {
        if (item == null)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);
        GameObject dropObj = PoolManager.Instance.GetObject(item.ItemName);
        dropObj.transform.position = spawnPos + Vector3.up * 0.5f;
        SetItem(null);
        
        Debug.Log($"{dropObj.name}을 바닥에 버렸습니다.");
    }
}