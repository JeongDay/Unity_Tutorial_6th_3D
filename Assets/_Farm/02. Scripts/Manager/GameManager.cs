using Farm;
using UnityEngine;

public class GameManager : SingletonCore<GameManager>
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject canvas;
    
    [field:SerializeField] public PoolManager Pool { get; private set; }
    [field:SerializeField] public UIManager Ui { get; private set; }
    // [field:SerializeField] public QuestManager quest { get; private set; }
    // [field:SerializeField] public CameraManager camera { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    void Start()
    {
        CameraManager.onSetProperty?.Invoke(DataManager.Instance.Player.transform);
    }

    private void Init()
    {
        // 캐릭터 생성
        int index = DataManager.Instance.SelectCharacterIndex;
        GameObject character = Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
        DataManager.Instance.Player = character;
        
        // Object Pool 생성
        GameObject poolObj = Instantiate(Pool.gameObject);
        poolObj.transform.SetParent(transform);
        Pool = poolObj.GetComponent<PoolManager>();
        
        // UI Manager 생성
        Ui = canvas.AddComponent<UIManager>();
    }
}