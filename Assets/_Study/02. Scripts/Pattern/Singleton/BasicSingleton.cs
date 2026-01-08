using System.Collections;
using UnityEngine;

public class BasicSingleton : MonoBehaviour
{
    private static BasicSingleton instance;
    public static BasicSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindFirstObjectByType<BasicSingleton>();

                if (obj != null) // 씬 상에서 오브젝트 찾음
                {
                    instance = obj;
                }
                else // 찾지 못했으면 직접 만들어서 연결
                {
                    var newObj = new GameObject("Basic Singleton");
                    newObj.AddComponent<BasicSingleton>();
                    
                    instance = newObj.GetComponent<BasicSingleton>();
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // 싱글턴 인스턴스로 할당
            DontDestroyOnLoad(gameObject); // 씬 전환되도 파괴되지 않도록 설정
        }
        else
            Destroy(gameObject); // 싱글턴 중복 생성 방지
    }
}