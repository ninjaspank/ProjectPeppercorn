using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to visually debug a path on the grid
/// </summary>
public class GridmapControl : MonoBehaviour
{
    [SerializeField] Gridmap targetGrid;
    [SerializeField] private LayerMask terrainLayerMask;

    private Vector2Int currentGridPosition = new Vector2Int(-1, -1);
    
    [SerializeField] private GridObject hoveringOver;
    [SerializeField] private SelectableGridObject selectedObject;

    void Update()
    {
        HoverOverObjectCheck();

        SelectedObject();

        DeselectObject();
    }

    private void DeselectObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectedObject = null;
        }
    }

    private void SelectedObject()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            if (hoveringOver == null) { return; }
            selectedObject = hoveringOver.GetComponent<SelectableGridObject>();
        }
    }

    private void HoverOverObjectCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
        {
            Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
            if (gridPosition == currentGridPosition) { return; }
            currentGridPosition = gridPosition;
            GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
            hoveringOver = gridObject;
        }
    }
}
