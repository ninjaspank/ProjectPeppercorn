using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the script that moves the character in the world space
/// </summary>
public class Movement : MonoBehaviour
{
    GridObject gridObject;
    CharacterAnimator characterAnimator;

    List<Vector3> pathWorldPositions;

    [SerializeField] private float moveSpeed = 1f;

    private void Awake()
    {
        gridObject = GetComponent <GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }
    
    /// <summary>
    /// Takes a list of nodes, translates them to world positions and then moves the character along those positions
    /// Once a location has been reached, remove that location from the list
    /// </summary>
    public void Move(List<PathNode> path)
    {
        pathWorldPositions = gridObject.targetGrid.ConvertPathNodesToWorldPositions(path);

        gridObject.positionOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.positionOnGrid.y = path[path.Count - 1].pos_y;
        
        RotateCharacter();

        characterAnimator.StartMoving();
    }
    /// <summary>
    /// Gets a direction to the next node and faces the character twords that position
    /// The y axis is locked to prevent rotating when climbing or descending
    /// </summary>
    private void RotateCharacter()
    {
        Vector3 direction = (pathWorldPositions[0] - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    /// <summary>
    /// This is the method that ticks every update and actually moves the character along the path
    /// </summary>
    private void Update()
    {
        if (pathWorldPositions == null) {return;}
        if (pathWorldPositions.Count == 0) {return;}
        // Move the character twords the next node over time
        transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], moveSpeed * Time.deltaTime);
        // If we're close enough to the next node on the list node, remove the node
        if (Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f)
        {
            pathWorldPositions.RemoveAt(0);
            // If there are no nodes left, tell the character to stop moving
            if(pathWorldPositions.Count == 0) { characterAnimator.StopMoving(); }
            else {
                RotateCharacter();
            }
        }
    }
}
