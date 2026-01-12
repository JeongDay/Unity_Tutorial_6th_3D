using UnityEngine;

public class AnimalArea : MonoBehaviour, ITriggerEvent
{
    public void InteractionEnter()
    {
        CameraManager.OnChangedCamera("Player", "Animal");
    }

    public void InteractionExit()
    {
        CameraManager.OnChangedCamera("Animal", "Player");
    }
}