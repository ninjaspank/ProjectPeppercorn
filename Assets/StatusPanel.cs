using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    private SelectCharacter selectCharacter;

    private bool isActive;

    [SerializeField] private GameObject statusPanel;
    [SerializeField] private TMPro.TextMeshProUGUI characterName;
    [SerializeField] private Slider hpBar;

    private Character currentCharacterStatus;
    
    private void Awake()
    {
        selectCharacter = GetComponent<SelectCharacter>();
    }

    private void Update()
    {
        if (isActive == true)
        {
            UpdateStatus(currentCharacterStatus);
            if (selectCharacter.hoverOverCharacer == null)
            {
                HideStatusPanel();
                return;
            }

            if (selectCharacter.hoverOverCharacer != currentCharacterStatus)
            {
                currentCharacterStatus = selectCharacter.hoverOverCharacer;
                UpdateStatus(currentCharacterStatus);
                return;
            }
        }
        else
        {
            if (selectCharacter.hoverOverCharacer != null)
            {
                currentCharacterStatus = selectCharacter.hoverOverCharacer;
                ShowStatusPanel();
                return;
            }
        }
    }

    private void HideStatusPanel()
    {
        statusPanel.SetActive(false);
        isActive = false;
    }

    private void ShowStatusPanel()
    {
        statusPanel.SetActive(true);
        isActive = true;
        UpdateStatus(selectCharacter.hoverOverCharacer);
    }

    private void UpdateStatus(Character hoverOverCharacter)
    {
        hpBar.maxValue = hoverOverCharacter.hp.max;
        hpBar.value = hoverOverCharacter.hp.current;
        characterName.text = hoverOverCharacter.Name;
    }
}
