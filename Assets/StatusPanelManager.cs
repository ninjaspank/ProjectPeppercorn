using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPanelManager : MonoBehaviour
{
    private SelectCharacter selectCharacter;
    
    private bool isActive;
    [SerializeField] private bool fixedCharacter;

    [SerializeField] private GameObject statusPanelGameObject;
    [SerializeField] private StatusPanel statusPanel;

    [SerializeField] private Character currentCharacterStatus;
    
    private void Awake()
    {
        selectCharacter = GetComponent<SelectCharacter>();
    }

    private void Update()
    {
        if (fixedCharacter == true)
        {
            statusPanel.UpdateStatus(currentCharacterStatus);
        }
        else
        {
            MouseHoverOverObject();
        }
    }

    private void MouseHoverOverObject()
    {
        if (isActive == true)
        {
            statusPanel.UpdateStatus(currentCharacterStatus);
            if (selectCharacter.hoverOverCharacer == null)
            {
                HideStatusPanel();
                return;
            }

            if (selectCharacter.hoverOverCharacer != currentCharacterStatus)
            {
                currentCharacterStatus = selectCharacter.hoverOverCharacer;
                statusPanel.UpdateStatus(currentCharacterStatus);
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
        statusPanelGameObject.SetActive(false);
        isActive = false;
    }

    private void ShowStatusPanel()
    {
        statusPanelGameObject.SetActive(true);
        isActive = true;
        statusPanel.UpdateStatus(selectCharacter.hoverOverCharacer);
    }
}
