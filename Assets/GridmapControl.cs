using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridmapControl : MonoBehaviour
{
    [SerializeField] Gridmap targetGrid;
    [SerializeField] private LayerMask terrainLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                if (gridObject == null)
                {
                    Debug.Log("x=" + gridPosition.x + "y=" + gridPosition.y + " is empty");
                }
                else
                {
                    Debug.Log("x=" + gridPosition.x + "y=" + gridPosition.y + gridObject.GetComponent<Character>().Name);
                }
            }
        }
    }
}
