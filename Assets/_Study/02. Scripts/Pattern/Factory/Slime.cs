using UnityEngine;

public class Slime : MonsterCore
{
    public override string Name => "슬라임";
    
    public override void Attack()
    {
        Debug.Log("슬라임 공격");
    }
}