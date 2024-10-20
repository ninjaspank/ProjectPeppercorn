using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
/// <summary>
/// This script goes on every object that we want to live on the grid
/// We snap to the grid and check into the grid system
/// </summary>
public class GridObject : MonoBehaviour
{
    public Gridmap targetGrid;

    public Vector2Int positionOnGrid;
    
    private void Start()
    {
        Init();
    }
    /// <summary>
    /// Init logic to check into and snap to the grid from a world position
    /// </summary>
    private void Init()
    {
        // Using the objects current world pos get a grid location
        positionOnGrid = targetGrid.GetGridPosition(transform.position);
        // Check in this object to the grid using that location
        targetGrid.PlaceObject(positionOnGrid, this);
        // Using the grid position to convert back to a world pos in the center of the grid location
        Vector3 pos = targetGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
        // snap the object to that world pos
        transform.position = pos;
    }
}
