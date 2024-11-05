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

    private List<CharacterTurn> characters;
    private int round = 1;

    [SerializeField] private TMPro.TextMeshProUGUI turnCountText;

    private void Start()
    {
        UpdateTextOnScreen();
    }

    public void AddMe(CharacterTurn character)
    {
        if (characters == null) { characters = new List<CharacterTurn>(); }
        
        characters.Add(character);
    }

    public void NextRound()
    {
        round += 1;
        UpdateTextOnScreen();

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].GrantTurn();
        }
    }

    void UpdateTextOnScreen()
    {
        turnCountText.text = "Turn: " + round.ToString();
    }
}
