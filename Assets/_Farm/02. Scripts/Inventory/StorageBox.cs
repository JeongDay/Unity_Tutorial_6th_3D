using System.Collections;
using UnityEngine;

public class StorageBox : MonoBehaviour, ITriggerEvent
{
    private Animator anim;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject storageUI;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InteractionEnter()
    {
        anim.SetTrigger("Open");

        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        yield return new WaitForSeconds(1f);
        
        inventoryUI.SetActive(true);
        storageUI.SetActive(true);
    }

    public void InteractionExit()
    {
        anim.SetTrigger("Close");
        
        inventoryUI.SetActive(false);
        storageUI.SetActive(false);
    }
}