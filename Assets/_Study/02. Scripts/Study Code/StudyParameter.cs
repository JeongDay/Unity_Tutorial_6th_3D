using UnityEngine;

public class StudyParameter : MonoBehaviour
{
    public int number = 10;
    public int number1 = 10;
    public int number2 = 10;
    
    void Start()
    {
        // NormalParameter(number);
        // RefParameter(ref number);
        OutParameter(out number, out number1, out number2);
    }

    private void NormalParameter(int n)
    {
        Debug.Log("호출 전 : " + n);
        n = 100;
        
        Debug.Log("호출 후: " + number);
    }

    private void RefParameter(ref int n)
    {
        Debug.Log("호출 전 : " + n);
        n = 100;
        
        Debug.Log("호출 후: " + number);
    }

    private void OutParameter(out int n, out int n1, out int n2)
    {
        n = 100;
        n1 = 100;
        n2 = 100;
    }
}