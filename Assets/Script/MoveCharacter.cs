using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Takes the player input and trigger the character movement if possible
/// This is going to get a path of nodes and send it to the character
/// </summary>
public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Gridmap targetGrid;
    [SerializeField] LayerMask terrainLayerMask;

    [SerializeField] GridObject targetCharacter;

    Pathfinding pathfinding;
    List<PathNode> path;
    private void Start()
    {
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }
    
    void Update()
    {
        // Get player left click input
        if (Input.GetMouseButtonDown(0))
        {
            // Building a ray from the cameras position twords the mouse on the screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Check the ray for any collisions with things tagged as terrain
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                // Based ont he world position that we hit, get the grid position
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                // Get a path between the characters current position and the clicked location
                path = pathfinding.FindPath(targetCharacter.positionOnGrid.x, targetCharacter.positionOnGrid.y, gridPosition.x, gridPosition.y);
                
                
                // Early out if the path is invalid
                if (path == null)
                {
                    Debug.LogWarning("MoveCharacter - Path list is null");
                    return;
                }

                if (path.Count == 0)
                {
                    Debug.LogWarning("MoveCharacter - Path List is empty");
                    return;
                }
                
                // If the path is valid, it's sent to Movement
                targetCharacter.GetComponent<Movement>().Move(path);
            }
            else Debug.LogWarning("MoveCharacter - Player clicked but we failed to raycast to the terrain");
        }
    }
}
