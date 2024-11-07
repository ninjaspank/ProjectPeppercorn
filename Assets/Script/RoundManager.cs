using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    //private List<CharacterTurn> characters;

    [SerializeField] private ForceContainer playerForceContainer;
    [SerializeField] private ForceContainer opponentForceContainer;
    
    private int round = 1;

    [SerializeField] private TMPro.TextMeshProUGUI turnCountText;
    [SerializeField] private TMPro.TextMeshProUGUI forceRoundText;

    private void Start()
    {
        UpdateTextOnScreen();
    }

    public void AddMe(CharacterTurn character)
    {
        //if (characters == null) { characters = new List<CharacterTurn>(); }
        
        if (character.allegiance == Allegiance.Player)
        {
            playerForceContainer.AddMe(character);
        }
        
        if (character.allegiance == Allegiance.Opponent)
        {
            opponentForceContainer.AddMe(character);
        }
    }

    private Allegiance currentTurn;

    public void NextTurn()
    {
        switch (currentTurn)
        {
            case Allegiance.Player:
                currentTurn = Allegiance.Opponent;
                break;
            case Allegiance.Opponent:
                NextRound();
                currentTurn = Allegiance.Player;
                break;

        }

        GrantTurnToForce();
        
        UpdateTextOnScreen();
    }

    private void GrantTurnToForce()
    {
        switch (currentTurn)
        {
            case Allegiance.Player:
                playerForceContainer.GrantTurn();
                break;
            case Allegiance.Opponent:
                opponentForceContainer.GrantTurn();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void NextRound()
    {
        round += 1;
    }

    void UpdateTextOnScreen()
    {
        turnCountText.text = "Turn: " + round.ToString();
        forceRoundText.text = currentTurn.ToString();
    }
}
