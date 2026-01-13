using UnityEngine;

public class Orc : MonsterCore
{
    public override string Name => "오크";

    public override void Attack()
    {
        Debug.Log("오크 공격");   
    }
}