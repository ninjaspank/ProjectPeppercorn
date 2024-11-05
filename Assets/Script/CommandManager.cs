using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public enum CommandType
{
    MoveTo,
    Attack
}

public class Command
{
    //Who the command is being shouted at 
    public Character character;
    //where that command should happen
    public Vector2Int selectedGrid;
    //and what the command is
    public CommandType commandType;

    public Command(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        this.character = character;
        this.selectedGrid = selectedGrid;
        this.commandType = commandType;
    }

    public List<PathNode> path;
    public GridObject target;
}
public class CommandManager : MonoBehaviour
{
    private ClearUtility clearUtility;

    private void Awake()
    {
        clearUtility = GetComponent<ClearUtility>();
    }
    
    private Command currentCommand;

    private CommandInput commandInput;

    private void Start()
    {
        commandInput = GetComponent<CommandInput>();
    }

    private void Update()
    {
        if (currentCommand != null)
        {
            ExecuteCommand();
        }
    }

    public void ExecuteCommand()
    {
        switch (currentCommand.commandType)
        {
            case CommandType.MoveTo:
                MovementCommandExecute();
                break;
            case CommandType.Attack:
                AttackCommandExecute();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AttackCommandExecute()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Attack>().AttackPosition(currentCommand.target);
        currentCommand = null;
        clearUtility.ClearGridHighlightAttack();
    }

    private void MovementCommandExecute()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Movement>().Move(currentCommand.path);
        currentCommand = null;
        clearUtility.ClearPathfinding();
        clearUtility.ClearGridHighlightMove();
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentCommand.path = path;
    }

    public void AddAttackCommand(Character attacker, Vector2Int selectGrid, GridObject target)
    {
        currentCommand = new Command(attacker, selectGrid, CommandType.Attack);
        currentCommand.target = target;
    }
}
