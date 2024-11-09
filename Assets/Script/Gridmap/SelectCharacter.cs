using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    private MouseInput mouseInput;
    private CommandMenu commandMenu;
    private GameMenu gameMenu;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        commandMenu = GetComponent<CommandMenu>();
        gameMenu = GetComponent<GameMenu>();
    }

    public Character selected;
    private bool isSelected;
    private GridObject hoverOverGridObject;
    public Character hoverOverCharacer;
    private Vector2Int positionOnGrid = new Vector2Int(-1,-1);
    [SerializeField] private Gridmap targetGrid;

    private void Update()
    {
        if (positionOnGrid != mouseInput.positionOnGrid)
        {
            HoverOverObject();
        }
        
        SelectInput();
        DeselectInput();
    }

    private void LateUpdate()
    {
        if (selected != null)
        {
            if (isSelected == false)
            {
                selected = null;
            }
        }
    }

    private void HoverOverObject()
    {
        positionOnGrid = mouseInput.positionOnGrid;
        hoverOverGridObject = targetGrid.GetPlacedObject(positionOnGrid);
        if (hoverOverGridObject != null)
        {
            hoverOverCharacer = hoverOverGridObject.GetComponent<Character>();
        }
        else
        {
            hoverOverCharacer = null;
        }
    }

    private void DeselectInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
            UpdatePanel();
        }
    }

    private void UpdatePanel()
    {
        if (selected != null)
        {
            commandMenu.OpenPanel(selected.GetComponent<CharacterTurn>());
        }
        else
        {
            commandMenu.ClosePanel();
        }
    }

    private void SelectInput()
    {
        HoverOverObject();
        if(selected != null) { return; }
        if (gameMenu.panel.activeInHierarchy == true) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacer != null && selected == null)
            {
                selected = hoverOverCharacer;
                isSelected = true;
            }
            UpdatePanel();
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
