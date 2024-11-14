using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private Gridmap targetGrid;
    [SerializeField] private LayerMask terrainLayerMask;

    public Vector2Int positionOnGrid;
    public bool active;

    [SerializeField] private TMPro.TextMeshProUGUI positionOnScreen;

    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
        {
            active = true;
            Vector2Int hitPosition = targetGrid.GetGridPosition(hit.point);
            if (hitPosition != positionOnGrid)
            {
                positionOnGrid = hitPosition;
                positionOnScreen.text = "Position " + positionOnGrid.x.ToString() + ":" + positionOnGrid.y;
            }
        }
        else
        {
            active = false;
            positionOnScreen.text = "Outside";
        }
    }
}
