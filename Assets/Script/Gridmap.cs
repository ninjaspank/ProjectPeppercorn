using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
/// <summary>
/// This keeps track of objects in the grid that can affect Pathfinding
/// </summary>
public class Gridmap : MonoBehaviour
{
    Node[,] gridmap;
    public int width = 25;
    public int length = 25;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask terrainLayer;
    private void Awake()
    {
        GenerateGrid();
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid) == true)
        {
            gridmap[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }
        else
        {
            Debug.Log("You are trying to place the object outside the boundaries!");
        }
    }

    public bool CheckBoundry(Vector2Int positionOnGrid)
    {
        if (positionOnGrid.x < 0 || positionOnGrid.x >= length)
        {
            return false;
        }
        if (positionOnGrid.y < 0 || positionOnGrid.y >= width)
        {
            return false;
        }

        return true;
    }

    internal bool CheckBoundry(int posX, int posY)
    {
        if (posX < 0 || posX >= length)
        {
            return false;
        }
        if (posY < 0 || posY >= width)
        {
            return false;
        }

        return true;
    }
    
    private void GenerateGrid()
    {
        gridmap = new Node[length, width];

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                gridmap[x, y] = new Node();
            }
        }

        CalculateElevation();
        CheckPassableTerrain();
    }
/// <summary>
/// Use the physics raycasting to hit points on the terrain and store elevation heights
/// This runs once during initialization
/// </summary>
    private void CalculateElevation()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                // Creating an array that is above the terrain facing down twords it
                Ray ray = new Ray(GetWorldPosition(x,y) + Vector3.up * 100f, Vector3.down);
                RaycastHit hit;
                // Using this ray, raycast down twords the terrain storing the location that we hit
                if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
                {
                    gridmap[x, y].elevation = hit.point.y;
                }
            }
        }
    }
    /// <summary>
    /// Using a given grid position, return the object that lives on the grid
    /// </summary>
    internal GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        if (CheckBoundry(gridPosition) == true)
        {
            GridObject gridObject = gridmap[gridPosition.x, gridPosition.y].gridObject;
            return gridObject;
        }

        return null;
    }
    /// <summary>
    /// During initialization, scan the entire grid to determine which positions are passable or blocked
    /// Runs once on init
    /// </summary>
    private void CheckPassableTerrain()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, obstacleLayer);
                gridmap[x, y].passable = passable;
            }
        }
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return gridmap[pos_x, pos_y].passable;
    }
    /// <summary>
    /// Given a world position, clamp it to the grid
    /// </summary>
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition.x += cellSize / 2;
        worldPosition.z += cellSize / 2;
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));
        return positionOnGrid;
    }
    /// <summary>
    /// Unity editor logic to draw visuals
    /// White is passable terrain, Red is unpassable terrain
    /// </summary>
    private void OnDrawGizmos()
    {
        if (gridmap == null)
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y);
                    Gizmos.DrawCube(pos, Vector3.one/4);
                }
            }
        }
        else
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y, true);
                    Gizmos.color = gridmap[x, y].passable ? Color.white : Color.red; 
                    Gizmos.DrawCube(pos, Vector3.one/4);
                }
            }
        }

    }
    /// <summary>
    /// Given a grid position, convert to world space
    /// </summary>
    public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
    {
        return new Vector3(x * cellSize, elevation == true ? gridmap[x,y].elevation : 0f, y * cellSize);
    }
    
    /// <summary>
    /// Converts a list of nodes to be a list of world positions
    /// </summary>
    public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
    {
        List<Vector3> worldPositions = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPositions.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
        }

        return worldPositions;
    }
}
