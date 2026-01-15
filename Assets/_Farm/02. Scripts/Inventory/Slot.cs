using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private IItem item;

    [SerializeField] private Button slotButton;
    [SerializeField] private Image slotImage;

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
}