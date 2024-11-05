using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private CommandInput commandInput;

    private SelectCharacter selectCharacter;

    private void Awake()
    {
        commandInput = GetComponent<CommandInput>();
        selectCharacter = GetComponent<SelectCharacter>();
    }

    public void OpenPanel()
    {
        selectCharacter.enabled = false;
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void MoveCommandSelected()
    {
        commandInput.SetCommandType(CommandType.MoveTo);
        commandInput.InitCommand();
        ClosePanel();
    }

    public void AttackCommandSelected()
    {
        commandInput.SetCommandType(CommandType.Attack);
        commandInput.InitCommand();
        ClosePanel();
    }
}
