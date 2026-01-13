using System.Collections.Generic;
using UnityEngine;

public class StudyObjectPool : MonoBehaviour
{
    public Queue<GameObject> objQueue = new Queue<GameObject>();

    public GameObject objPrefab;
    public Transform parent;

    void Start()
    {
        CreateObject(50);
    }

    private void CreateObject(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(objPrefab, parent);
            EnqueueObject(obj);
        }
    }

    public void EnqueueObject(GameObject newObj)
    {
        Rigidbody rb = newObj.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        newObj.SetActive(false);
        
        objQueue.Enqueue(newObj);
    }

    public GameObject DequeueObject()
    {
        if (objQueue.Count < 3)
            CreateObject(50);
        
        GameObject obj = objQueue.Dequeue();
        obj.SetActive(true);

        return obj;
    }
}