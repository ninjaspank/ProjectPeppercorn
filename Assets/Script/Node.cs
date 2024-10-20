using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This represents a space in the world that will determine if a path is available
/// </summary>
public class Node
{
    public bool passable;
    public GridObject gridObject;
    public float elevation;
}
