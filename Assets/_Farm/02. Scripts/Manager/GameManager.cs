using Farm;
using UnityEngine;

public class GameManager : SingletonCore<GameManager>
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;

    protected override void Awake()
    {
        base.Awake();

        int index = DataManager.Instance.SelectCharacterIndex;
        GameObject character = Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);

        CameraManager.onSetProperty?.Invoke(character.transform);
    }
}