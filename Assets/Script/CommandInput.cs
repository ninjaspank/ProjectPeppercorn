using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    private CommandManager commandManager;
    private MouseInput mouseInput;
    private MoveCharacter moveCharacter;
    private CharacterAttack characterAttack;
    private SelectCharacter selectCharacter;
    private ClearUtility clearUtility;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
        selectCharacter = GetComponent<SelectCharacter>();
        clearUtility = GetComponent<ClearUtility>();
    }
    
    [SerializeField] private CommandType currentCommand;
    private bool isInputCommand;

    public void SetCommandType(CommandType commandType)
    {
        currentCommand = commandType;
    }

    public void InitCommand()
    {
        isInputCommand = true;
        switch(currentCommand)
        {
            case CommandType.MoveTo:
                HighlightWalkableTerrain();
                break;
            case CommandType.Attack:
                characterAttack.CalculateAttackArea(
                    selectCharacter.selected.GetComponent<GridObject>().positionOnGrid, 
                    selectCharacter.selected.attackRange
                    );
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    

    private void Update()
    {
        if (isInputCommand == false) { return; }
        switch(currentCommand)
        {
            case CommandType.MoveTo:
                MoveCommandInput();
                break;
            case CommandType.Attack:
                AttackCommandInput();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AttackCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (characterAttack.Check(mouseInput.positionOnGrid) == true)
            {
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.positionOnGrid);
                if (gridObject == null) { return; }
                commandManager.AddAttackCommand(selectCharacter.selected, mouseInput.positionOnGrid, gridObject);
                StopCommandInput();

            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightAttack();
        }
    }

    private void MoveCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (moveCharacter.CheckOccupied(mouseInput.positionOnGrid) == true) { return; }
            List<PathNode> path = moveCharacter.GetPath(mouseInput.positionOnGrid);
            if (path == null) { return; }
            if (path.Count == 0) { return; }
            commandManager.AddMoveCommand(selectCharacter.selected, mouseInput.positionOnGrid, path);
            StopCommandInput();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightMove();
            clearUtility.ClearPathfinding();
        }
    }

    private void StopCommandInput()
    {
        selectCharacter.Deselect();
        selectCharacter.enabled = true;
        isInputCommand = false;
    }

    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectCharacter.selected);
    }
}
