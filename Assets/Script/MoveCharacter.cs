using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Takes the player input and trigger the character movement if possible
/// This is going to get a path of nodes and send it to the character
/// </summary>
public class MoveCharacter : MonoBehaviour
{
    Gridmap targetGrid;

    Pathfinding pathfinding;

    private GridHighlight gridHighlight;
    
    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetGrid = stageManager.stageGrid;
        gridHighlight = stageManager.moveHighlight;
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }

    public void CheckWalkableTerrain(Character targetCharacter)
    {
        // We know the character is always attached to the same object as Gridobject
        GridObject gridObject = targetCharacter.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.Clear();
        pathfinding.CalculateWalkableNodes(
            gridObject.positionOnGrid.x,
            gridObject.positionOnGrid.y,
            targetCharacter. GetFloatValue(CharacterStats.MovementPoints),
            ref walkableNodes
            );
        gridHighlight.Hide();
        gridHighlight.Highlight(walkableNodes);
    }

    public List<PathNode> GetPath(Vector2Int from)
    {
        List<PathNode> path = pathfinding.TraceBackPath(from.x, from.y);
        
        // Early out if the path is invalid
        if (path == null)
        {
            Debug.LogWarning("MoveCharacter - Path list is null");
            return null; }
        if (path.Count == 0)
        {
            Debug.LogWarning("MoveCharacter - Path List is empty");
            return null; }
        
        path.Reverse();

        return path;
    }

    public bool CheckOccupied(Vector2Int positionOnGrid)
    {
        return targetGrid.CheckOccupied(positionOnGrid);
    }
}
