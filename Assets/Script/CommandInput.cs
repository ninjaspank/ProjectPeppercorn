using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    private CommandManager commandManager;
    private MouseInput mouseInput;
    private MoveCharacter moveCharacter;
    private CharacterAttack characterAttack;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
    }

    [SerializeField] private Character selectedCharacter;
    [SerializeField] private CommandType currentCommand;

    private void Start()
    {
        //HighlightWalkableTerrain();
        characterAttack.CalculateAttackArea(
            selectedCharacter.GetComponent<GridObject>().positionOnGrid, 
            selectedCharacter.attackRange
            );
    }

    private void Update()
    {
        //MoveComandInput();
        AttackCommandInput();
    }

    private void AttackCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (characterAttack.Check(mouseInput.positionOnGrid) == true)
            {
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.positionOnGrid);
                if(gridObject == null) { return; }
                commandManager.AddAttackCommand(selectedCharacter, mouseInput.positionOnGrid, gridObject);
            }
        }
    }

    private void MoveComandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<PathNode> path = moveCharacter.GetPath(mouseInput.positionOnGrid);
            if (path == null)
            {
                return;
            }

            if (path.Count == 0)
            {
                return;
            }

            commandManager.AddMoveCommand(selectedCharacter, mouseInput.positionOnGrid, path);
        }

        if (Input.GetMouseButtonDown(1))
        {
            selectedCharacter.GetComponent<Movement>().SkipAnimation();
        }
    }
    
    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectedCharacter);
    }
}
