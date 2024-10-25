using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform marker;

    private MouseInput mouseInput;

    private Vector2Int currentPosition;
    private bool active;
    [SerializeField] private Gridmap targetGrid;
    [SerializeField] private float elevation = 2f;

    private void Awake()
    {
        {
            mouseInput = GetComponent<MouseInput>();
        }
    }

    private void Update()
    {
        if (active != mouseInput.active)
        {
            active = mouseInput.active;
            marker.gameObject.SetActive(active);
        }
        if(active == false) {return;}
        if (currentPosition != mouseInput.positionOnGrid)
        {
            currentPosition = mouseInput.positionOnGrid;
            UpdateMarker();
        }
    }

    private void UpdateMarker()
    {
        if(targetGrid.CheckBoundry(currentPosition) == false) {return;}
        Vector3 worldPosition = targetGrid.GetWorldPosition(currentPosition.x, currentPosition.y, true);
        worldPosition.y += elevation;
        marker.position = worldPosition;
    }
}
