using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Gridmap targetGrid;
    [SerializeField] private GridHighlight highlight;

    private List<Vector2Int> attackPosition;

    public void CalculateAttackArea(Vector2Int characterPositionOnGrid, int attackRange, bool selfTargetable = false)
    {
        if (attackPosition == null)
        {
            attackPosition = new List<Vector2Int>();
        }
        else {
            attackPosition.Clear();
        }
        
        // We cycle through all the nodes we can reach with our AttackRange
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int y = -attackRange; y <= attackRange; y++)
            {
                // Calculate the actual distance to the node
                if(Mathf.Abs(x) + Mathf.Abs(y) > attackRange) { continue; }

                // Skip the center of the attack range if not self-targetable
                if (selfTargetable == false)
                {
                    if(x == 0 && y == 0) { continue; }
                }
                
                // If the position of the tile we are checking is inside the boundaries then add it io the list
                if (targetGrid.CheckBoundry(
                        characterPositionOnGrid.x + x, 
                        characterPositionOnGrid.y + y) == true)
                {
                    attackPosition.Add(
                        new Vector2Int(
                            characterPositionOnGrid.x + x, 
                            characterPositionOnGrid.y + y
                        )
                    );
                }
            }
        }
        // Highlight the positions in the list
        highlight.Highlight(attackPosition);
    }

    public bool Check(Vector2Int positionOnGrid)
    {
        return attackPosition.Contains(positionOnGrid);
    }

    public GridObject GetAttackTarget(Vector2Int positionOnGrid)
    {
        GridObject target = targetGrid.GetPlacedObject(positionOnGrid);
        return target;
    }
}
