using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI characterName;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    [SerializeField] private Slider expBar;

    [SerializeField] private CharacterAttributeText strAttributeText;
    [SerializeField] private CharacterAttributeText magAttributeText;
    [SerializeField] private CharacterAttributeText sklAttributeText;
    [SerializeField] private CharacterAttributeText spdAttributeText;
    [SerializeField] private CharacterAttributeText defAttributeText;
    [SerializeField] private CharacterAttributeText resAttributeText;

    public void UpdateStatus(Character character)
    {
        hpBar.maxValue = character.hp.max;
        hpBar.value = character.hp.current;
        characterName.text = character.characterData.Name;

        expBar.maxValue = character.characterData.level.RequiredExperienceToLevelUp;
        expBar.value = character.characterData.level.experience;
        levelText.text = "LVL:" + character.characterData.level.level.ToString();
        
        strAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Strength));
        magAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Magic));
        sklAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Skill));
        spdAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Speed));
        defAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Defense));
        resAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Resistance));
    }
}
