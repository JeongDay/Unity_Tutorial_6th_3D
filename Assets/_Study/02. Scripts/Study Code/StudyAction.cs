using System;
using UnityEngine;

public class StudyAction : MonoBehaviour
{
    // public StudyScore studyScore;
    
    void Start()
    {
        StudyScore.onScore += Method;
        
        StudyScore.OnScore();
    }

    private void Method()
    {
        
    }
}