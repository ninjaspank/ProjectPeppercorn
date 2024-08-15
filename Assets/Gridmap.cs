using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmap : MonoBehaviour
{
    Node[,] gridmap;
    [SerializeField] int width = 25;
    [SerializeField] int length = 25;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private LayerMask obstacleLayer;
    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        gridmap = new Node[length, width];
        CheckPassableTerrain();
    }

    private void CheckPassableTerrain()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, obstacleLayer);
                gridmap[x, y] = new Node();
                gridmap[x, y].passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gridmap == null) { return; }
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 pos = GetWorldPosition(x, y);
                Gizmos.color = gridmap[x, y].passable ? Color.white : Color.red; 
                Gizmos.DrawCube(pos, Vector3.one/4);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(transform.position.x + (x * cellSize), 0f, transform.position.z + (y * cellSize));
    }
}
