using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    private MouseInput mouseInput;
    private CommandMenu commandMenu;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        commandMenu = GetComponent<CommandMenu>();
    }

    public Character selected;
    private GridObject hoverOverGridObject;
    public Character hoverOverCharacer;
    private Vector2Int positionOnGrid = new Vector2Int(-1,-1);
    [SerializeField] private Gridmap targetGrid;

    private void Update()
    {
        HoverOverObject();
        SelectInput();
        DeselectInput();
    }

    private void HoverOverObject()
    {
        if (positionOnGrid != mouseInput.positionOnGrid)
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
            commandMenu.OpenPanel();
        }
        else
        {
            commandMenu.ClosePanel();
        }
    }

    private void SelectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacer != null && selected == null)
            {
                selected = hoverOverCharacer;
            }
            UpdatePanel();
        }
    }

    public void Deselect()
    {
        selected = null;
    }
}
