using UnityEngine;

public class SimpleSingleton : MonoBehaviour
{
    public static SimpleSingleton Instance { get; private set; }

    public int level;
    public string playerName;
    
    void Awake()
    {
        Instance = this;
    }

    public void LevelUp()
    {
        level++;
    }
}