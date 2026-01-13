using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Unit unit;

    private ICommand attackCommand, moveCommand, skillCommand;

    private Queue<ICommand> commandQueues = new Queue<ICommand>();
    private Stack<ICommand> executeCommands = new Stack<ICommand>();

    void Awake()
    {
        unit = GetComponent<Unit>();

        attackCommand = new AttackCommand(unit);
        moveCommand = new MoveCommand(unit);
        skillCommand = new SkillCommand(unit, "FireBall");
    }

    void Update()
    {
        #region 즉시 실행
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attackCommand.Execute();
            executeCommands.Push(attackCommand);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            moveCommand.Execute();
            executeCommands.Push(moveCommand);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            skillCommand.Execute();
            executeCommands.Push(skillCommand);
        }
        #endregion


        #region 기능 누적
        if (Input.GetKeyDown(KeyCode.Z))
        {
            commandQueues.Enqueue(attackCommand);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            commandQueues.Enqueue(moveCommand);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            commandQueues.Enqueue(skillCommand);
        }
        #endregion


        // 한번에 실행
        if (Input.GetKeyDown(KeyCode.Return))
        {
            while (commandQueues.Count > 0)
            {
                ICommand command = commandQueues.Dequeue();
                command.Execute();
                executeCommands.Push(command);
            }
        }

        // 최근에 실행한 기능 취소
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (executeCommands.Count > 0)
            {
                ICommand lastCommand = executeCommands.Pop();
                Debug.Log($"명령 취소 : {lastCommand.GetType().Name}");

                lastCommand.Cancel();
            }
            else
            {
                Debug.Log("되돌릴 명령이 없습니다.");
            }
        }
    }
}