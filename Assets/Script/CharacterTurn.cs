using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Allegiance
{
    Player,
    Opponent
}

public class CharacterTurn : MonoBehaviour
{
    public Allegiance allegiance;
    
    public bool canWalk;
    public bool canAct;

    public void Start()
    {
        AddToRoundManager();
        GrantTurn();
    }

    private void AddToRoundManager()
    {
        RoundManager.instance.AddMe(this);
    }

    public void GrantTurn()
    {
        canWalk = true;
        canAct = true;
    }
}
