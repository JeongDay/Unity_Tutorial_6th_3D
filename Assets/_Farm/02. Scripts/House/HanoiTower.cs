using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HanoiTower : MonoBehaviour, ITriggerEvent
{
    public enum HanoiLevel { Lv1 = 3, Lv2, Lv3 }
    public HanoiLevel hanoiLevel;

    [SerializeField] private GameObject[] ringPrefabs;

    [SerializeField] private BoardStick[] sticks;

    public static GameObject selectedRing;
    public static bool isSelected;

    IEnumerator Start()
    {
        for (int i = (int)hanoiLevel - 1; i >= 0; i--)
        {
            GameObject ring = Instantiate(ringPrefabs[i]);

            sticks[0].PushRing(ring);

            yield return new WaitForSeconds(1f);
        }
    }

    public void InteractionEnter()
    {
        CameraManager.OnChangedCamera("House", "Hanoi");
        Camera.main.cullingMask = ~(1 << 2); // 2번 레이어만 빼고 설정
    }

    public void InteractionExit()
    {
        CameraManager.OnChangedCamera("Hanoi", "House");
        Camera.main.cullingMask = -1; // 전체 설정
        // Camera.main.cullingMask = 0; // 전체 설정 해제
    }
}