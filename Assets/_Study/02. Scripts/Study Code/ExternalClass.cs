using System;
using UnityEngine;

public class ExternalClass : MonoBehaviour
{
    void Start()
    {
        BasicClass.Instance.LevelUp();

        BasicClass.Instance.level = 10;
    }
}