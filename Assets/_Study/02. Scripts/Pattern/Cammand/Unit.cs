using UnityEngine;

public class Unit : MonoBehaviour
{
    public void Attack()
    {
        Debug.Log("Attack");
    }

    public void AttackCancel()
    {
        Debug.Log("AttackCancel");
    }
    
    public void Move()
    {
        Debug.Log("Move");
    }

    public void MoveCancel()
    {
        Debug.Log("MoveCancel");
    }
    
    public void Skill()
    {
        Debug.Log("Skill");
    }

    public void SkillCancel()
    {
        Debug.Log("SkillCancel");
    }
}