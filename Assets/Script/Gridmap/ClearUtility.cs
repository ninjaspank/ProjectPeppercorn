using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUtility : MonoBehaviour
{
    private Pathfinding targetPF;
    private GridHighlight attackHighlight;
    private GridHighlight moveHighlight;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        attackHighlight = stageManager.attackHighlight;
        moveHighlight = stageManager.moveHighlight;
        targetPF = stageManager.pathfinding;
    }

    public void ClearPathfinding()
    {
        targetPF.Clear();
    }

    public void ClearGridHighlightAttack()
    {
        attackHighlight.Hide();
    }

    public void ClearGridHighlightMove()
    {
        moveHighlight.Hide();
    }
}
