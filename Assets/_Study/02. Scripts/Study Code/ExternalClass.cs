using System;
using UnityEngine;

public class ExternalClass : MonoBehaviour
{
    public delegate void AtkDelegate();
    public static event AtkDelegate atkDelegate;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 공격
            atkDelegate?.Invoke();
        }
    }
}