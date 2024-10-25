using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Individual node on the movement grid
/// During Pathfinding we assign these values until a final path is discovered
/// </summary>
public class PathNode
{
    public int pos_x;
    public int pos_y;

    // g value is walking cost to move to the next square: 10 for orthogonal and 14 for diagonal by default
    public float gValue;
    // h value is the heuristic (total) cost to reach final goal
    public float hValue;
    // This is a temporary value of a parent node used during Pathfinding
    public PathNode parentNode;

    public float fValue
    {
        get { return gValue + hValue; }
    }

    public PathNode(int xPos, int yPos)
    {
        pos_x = xPos;
        pos_y = yPos;
    }

    public void Clear()
    {
        gValue = 0f;
        hValue = 0f;
        parentNode = null;
    }
}


/// <summary>
/// This is the main script for finding and returning a path list
/// </summary>
[RequireComponent(typeof(Gridmap))]
public class Pathfinding : MonoBehaviour
{
    // This is a reference to the system that knows where objects live on the grid
    Gridmap gridMap;

    PathNode[,] pathNodes;
    private void Start()
    {
        Init();
    }
    
    internal void Clear()
    {
        for (int x = 0; x < gridMap.width; x++)
        {
            for (int y = 0; y < gridMap.length; y++)
            {
                pathNodes[x, y].Clear();
            }
        }
    }

    /// <summary>
    /// Creats a node list that matches 1:1 with the grid map
    /// </summary>
    private void Init()
    {
        if (gridMap == null) { gridMap = GetComponent<Gridmap>(); }

        pathNodes = new PathNode[gridMap.width, gridMap.length];

        for (int x = 0; x < gridMap.width; x++)
        {
            for (int y = 0; y < gridMap.length; y++)
            {
                pathNodes[x, y] = new PathNode(x, y);
            }
        }
    }

    /// <summary>
    /// Repurposing a lot of the code used in 'FindPath' to create a list of nodes that a character can walk to give a movement value
    /// </summary>
    public void CalculateWalkableNodes(int startX, int startY, float range, ref List<PathNode> toHighlight)
    {
        PathNode startNode = pathNodes[startX, startY];
        
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];
            
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            
            List<PathNode> neighborNodes = new List<PathNode>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) { continue; }
                    if (gridMap.CheckBoundry(currentNode.pos_x + x, currentNode.pos_y + y) == false) { continue; }
                    
                    neighborNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y + y]);
                }
            }
            
            for (int i = 0; i < neighborNodes.Count; i++)
            {
                if(closedList.Contains(neighborNodes[i])) { continue; }
                if(gridMap.CheckWalkable(neighborNodes[i].pos_x, neighborNodes[i].pos_y) == false) { continue; }
                
                float movementCost = currentNode.gValue + CalculateDistance(currentNode, neighborNodes[i]);
                
                if(movementCost > range) { continue; }
                
                if (openList.Contains(neighborNodes[i]) == false
                    || movementCost < neighborNodes[i].gValue
                   )
                {
                    neighborNodes[i].gValue = movementCost;
                    neighborNodes[i].parentNode = currentNode;

                    if (openList.Contains(neighborNodes[i]) == false)
                    {
                        openList.Add(neighborNodes[i]);
                    }
                }
            }
        }

        if (toHighlight != null)
        {
            toHighlight.AddRange(closedList);   
        }
    }
    
    /// <summary>
    /// Given a start and end location, calculate the most direct path
    /// </summary>
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        // Convert the grid pos to path nodes
        PathNode startNode = pathNodes[startX, startY];
        PathNode endNode = pathNodes[endX, endY];
        // These are the path nodes to check next
        List<PathNode> openList = new List<PathNode>();
        // These are the nodes we've already checked
        List<PathNode> closedList = new List<PathNode>();
        
        // Start by adding nodes to the open list
        openList.Add(startNode);
        
        // While we have nodes to evaluate, keep this logic running
        while (openList.Count > 0)
        {
            //Get the first node of the open list
            PathNode currentNode = openList[0];
            // Loop through the open list to find the next best node to walk to
            for (int i = 0; i < openList.Count; i++)
            {
                if (currentNode.fValue > openList[i].fValue)
                {
                    currentNode = openList[i];
                }

                if (currentNode.fValue == openList[i].fValue
                    && currentNode.hValue > openList[i].hValue)
                {
                    currentNode = openList[i];
                }
            }
            
            // Once we have the next best node, remove it from the closed list and add it to the open list
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            // If the node we're looking at is the end, then we have a path!
            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }
            
            // Based on the current node, evaluate nodes to step into
            List<PathNode> neighborNodes = new List<PathNode>();
            // Check the 3x3 grid around the node for neighbours
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    // Ignore the current node, which is in the middle of the 3x3
                    if (x == 0 && y == 0) { continue; }
                    if (gridMap.CheckBoundry(currentNode.pos_x + x, currentNode.pos_y + y) == false) { continue; }
                    
                    neighborNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y + y]);
                }
            }
            
            // Evaluate neighbour nodes to step into
            for (int i = 0; i < neighborNodes.Count; i++)
            {
                // Early out for already evaluated nodes
                if(closedList.Contains(neighborNodes[i])) { continue; }
                // If it's not walkable, ignore it
                if(gridMap.CheckWalkable(neighborNodes[i].pos_x, neighborNodes[i].pos_y) == false) { continue; }
                // Cost if we wanted to step into this neighbourNode
                float movementCost = currentNode.gValue + CalculateDistance(currentNode, neighborNodes[i]);
                // If it isn't in the open list OR it's cheaper to walk to, add it to the open list
                if (openList.Contains(neighborNodes[i]) == false
                    || movementCost < neighborNodes[i].gValue
                   )
                {
                    // Assigning a value to the neighbourNode as if we were walking to it from this current node
                    neighborNodes[i].gValue = movementCost;
                    neighborNodes[i].hValue = CalculateDistance(neighborNodes[i], endNode);
                    // Based on what we've calculated, this is the most efficient node and setting it the parent
                    neighborNodes[i].parentNode = currentNode;

                    if (openList.Contains(neighborNodes[i]) == false)
                    {
                        // Add to the open list if not already in
                        openList.Add(neighborNodes[i]);
                    }
                }
            }
            
        }
        
        return null;
    }
    
    // This technically would be 'CalculateStepCost'
    private int CalculateDistance(PathNode currentNode, PathNode target)
    {
        int distX = Mathf.Abs(currentNode.pos_x - target.pos_x);
        int distY = Mathf.Abs(currentNode.pos_y - target.pos_y);
        
        if(distX > distY) { return 14 * distY + 10 * (distX - distY); }
        return 14 * distX + 10 * (distY - distX);
    }
    /// <summary>
    /// Walking back along the path of parent nodes to return to 'start'
    /// </summary>
    private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();

        return path;
    }

    public List<PathNode> TraceBackPath(int x, int y)
    {
        if (gridMap.CheckBoundry(x, y) == false) { return null; }
        List<PathNode> path = new List<PathNode>();
        
        PathNode currentNode = pathNodes[x, y];
        while (currentNode.parentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        return path;
    }
}
