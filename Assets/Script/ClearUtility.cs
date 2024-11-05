using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUtility : MonoBehaviour
{
    [SerializeField] private Pathfinding targetPF;
    [SerializeField] private GridHighlight attackHighlight;
    [SerializeField] private GridHighlight moveHighlight;

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
