using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StudyLambda : MonoBehaviour
{
    public delegate void MyDelegate();
    public MyDelegate myDelegate;

    public delegate void MyDelegate2(string s);
    public MyDelegate2 myDelegate2;

    public Button buttonUI;

    public Button[] buttonUIs;

    public List<int> numbers = new List<int>();
    
    void Start()
    {
        #region Lambda

        myDelegate = () => Debug.Log("Hello Unity");
        myDelegate?.Invoke();

        // myDelegate += () =>
        // {
        //     Debug.Log("Hello Unity");
        //     Debug.Log("Hello C#");
        // };


        myDelegate2 = (s) => Debug.Log(s);
        myDelegate2("Hello C#");


        // myDelegate += delegate(string s)
        // {
        //     Debug.Log(s);
        // };
        //
        // myDelegate?.Invoke("Hello Unity");

        #endregion

        #region Button
        buttonUI.onClick.AddListener(ButtonEvent);
        
        // buttonUI.onClick.AddListener(delegate
        // {
        //     ButtonEvent2(10);
        // });

        buttonUI.onClick.AddListener(() => ButtonEvent2(10));
        
        // 클로져(Closure) 이슈
        for (int i = 0; i < buttonUIs.Length; i++)
        {
            int j = i;
            buttonUIs[i].onClick.AddListener(() => ButtonEvent2(j));
        }
        #endregion

        #region List
        for (int i = 0; i < 10; i++)
            numbers.Add(i); // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9

        numbers.Sort(); // 오름차순 정렬

        numbers.Clear(); // 초기화

        numbers.RemoveAll(num => num % 2 == 0);
        #endregion
    }

    public void ButtonEvent()
    {
        Debug.Log("버튼 눌림");
    }

    public void ButtonEvent2(int number)
    {
        Debug.Log("2 버튼 눌림");
    }
}