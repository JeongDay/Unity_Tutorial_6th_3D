using UnityEngine;

public class MoveCommand : ICommand
{
    public Unit unit;
    
    public MoveCommand(Unit unit)
    {
        this.unit = unit;
    }
    
    public void Execute()
    {
        unit.Move();
    }

    public void Cancel()
    {
        unit.MoveCancel();
    }
}