using UnityEngine;

public class SkillCommand : ICommand
{
    public Unit unit;
    public string skillName;
    
    public SkillCommand(Unit unit, string skillName)
    {
        this.unit = unit;
        this.skillName = skillName;
    }
    
    public void Execute()
    {
        unit.Skill();
    }

    public void Cancel()
    {
        unit.SkillCancel();
    }
}