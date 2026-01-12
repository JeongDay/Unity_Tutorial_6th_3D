using UnityEngine;

public class FieldArea : MonoBehaviour, ITriggerEvent
{
    public void InteractionEnter()
    {
        CameraManager.OnChangedCamera("Player", "Field");
    }

    public void InteractionExit()
    {
        CameraManager.OnChangedCamera("Field", "Player");
    }
}