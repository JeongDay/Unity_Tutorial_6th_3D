using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StudyScore : MonoBehaviour
{
    public static event Action onScore;

    void Start()
    {
        onScore += Score;
    }

    private void Score()
    {
        
    }

    public static void OnScore()
    {
        onScore?.Invoke();
    }
}