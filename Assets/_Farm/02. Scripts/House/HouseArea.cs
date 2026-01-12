using System;
using UnityEngine;

public class HouseArea : MonoBehaviour, ITriggerEvent
{
    public void InteractionEnter()
    {
        CameraManager.OnChangedCamera("Player", "House");
    }

    public void InteractionExit()
    {
        CameraManager.OnChangedCamera("House", "Player");
    }
}