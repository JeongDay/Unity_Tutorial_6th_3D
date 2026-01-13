using UnityEngine;

public abstract class MonsterFactory : MonoBehaviour
{
    public MonsterCore Spawn(string type)
    {
        MonsterCore monster = CreateMonster(type);
        Debug.Log($"{monster.Name} 생성");

        return monster;
    }

    protected abstract MonsterCore CreateMonster(string type);
}