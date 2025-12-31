using System;
using UnityEngine;

public class StudyPredicate : MonoBehaviour
{
    // A키 왼쪽 보기
    // D키 눌렀을 때 오른쪽 보기

    public Action action;
    public event Action action2;

    public Action<int> action3;
    public Action<bool> action4;
    public Action<Transform> action5;
    public Action<Transform, int, float, bool> action6;
    
    private SpriteRenderer spriteRenderer;

    public Predicate<float> myPredicate;

    void Start()
    {
        myPredicate = SetFlip;
    }
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        
        spriteRenderer.flipX = myPredicate(h);

        // if (h < 0)
        //     spriteRenderer.flipX = true;
        // else if (h > 0)
        //     spriteRenderer.flipX = false;

        // if (h < 0)
        //     transform.localScale = new Vector3(-1, 1, 1);
        // else if (h > 0)
        //     transform.localScale = new Vector3(1, 1, 1);
    }

    private bool SetFlip(float h)
    {
        if (h < 0)
            return true;
        else if (h > 0) 
            return false; 
        else
            return false;
    }
    
}