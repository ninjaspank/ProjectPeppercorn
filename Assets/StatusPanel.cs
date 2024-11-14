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
        characterName.text = character.Name;

        expBar.maxValue = character.level.RequiredExperienceToLevelUp;
        expBar.value = character.level.experience;
        levelText.text = "LVL:" + character.level.level.ToString();
        
        strAttributeText.UpdateText(character.attributes.Get(CharacterAttributeEnum.Strength));
        magAttributeText.UpdateText(character.attributes.Get(CharacterAttributeEnum.Magic));
        sklAttributeText.UpdateText(character.attributes.Get(CharacterAttributeEnum.Skill));
        spdAttributeText.UpdateText(character.attributes.Get(CharacterAttributeEnum.Speed));
        defAttributeText.UpdateText(character.attributes.Get(CharacterAttributeEnum.Defense));
        resAttributeText.UpdateText(character.attributes.Get(CharacterAttributeEnum.Resistance));
    }
}
