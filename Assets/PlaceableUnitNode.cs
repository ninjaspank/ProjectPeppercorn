using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableUnitNode : MonoBehaviour
{
    public Character character;
    private UnitPlacementManager manager;
    public GridObject gridObject;

    private void Awake()
    {
        manager = FindObjectOfType<UnitPlacementManager>();
        gridObject = GetComponent<GridObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        manager.AddMe(this);
    }
}
