using UnityEngine;

public class Dragon : MonsterCore
{
    public override string Name => "드래곤";
    
    public override void Attack()
    {
        Debug.Log("드래곤 공격");
    }
}