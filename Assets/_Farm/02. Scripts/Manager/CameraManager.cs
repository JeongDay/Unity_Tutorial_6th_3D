using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform clearShot;

    private static event Action<string, string> onChangedCamera;
    
    private Dictionary<string, CinemachineCamera> cameraDics = new Dictionary<string, CinemachineCamera>();

    void Awake()
    {
        if (clearShot == null)
            return;

        for (int i = 0; i < clearShot.childCount; i++)
        {
            Transform child = clearShot.GetChild(i);
            CinemachineCamera cam = child.GetComponent<CinemachineCamera>();

            if (!cameraDics.ContainsKey(child.name)) // 저장된 키값이 있는지 확인
            {
                cameraDics.Add(child.name, cam); // 딕셔너리에 저장
                Debug.Log($"{child.name} 카메라 등록");
            }
        }
    }

    void OnEnable()
    {
        onChangedCamera += SetCamera;
    }

    void OnDisable()
    {
        onChangedCamera -= SetCamera;
    }

    private void SetCamera(string from, string to)
    {
        cameraDics[from].Priority = 0;
        cameraDics[to].Priority = 10;
    }

    public static void OnChangedCamera(string from, string to)
    {
        onChangedCamera?.Invoke(from, to);
    }
    
}