using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Int2Val
{
    public int current;
    public int max;

    public bool canGoNegative;

    public Int2Val(int current, int max)
    {
        this.current = current;
        this.max = max;
    }

    public void Subtract(int amount)
    {
        current -= amount;

        if (canGoNegative == false)
        {
            if (current < 0)
            {
                current = 0;
            }
        }
    }
}

public enum CharacterAttributeEnum
{
    Strength,
    Magic,
    Skill,
    Speed,
    Defense,
    Resistance
}

[Serializable]
public class CharacterAttributes
{
    public const int AttributeCount = 6;

    public int strength;
    public int magic;
    public int skill;
    public int speed;
    public int defense;
    public int resistance;

    public CharacterAttributes()
    {
        
    }

    public void Sum(CharacterAttributeEnum attribute, int val)
    {
        switch (attribute)
        {
            case CharacterAttributeEnum.Strength:
                strength += val;
                break;
            case CharacterAttributeEnum.Magic:
                magic += val;
                break;
            case CharacterAttributeEnum.Skill:
                skill += val;
                break;
            case CharacterAttributeEnum.Speed:
                speed  += val;
                break;
            case CharacterAttributeEnum.Defense:
                defense  += val;
                break;
            case CharacterAttributeEnum.Resistance:
                resistance  += val;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attribute), attribute, null);
        }
    }

    public int Get(CharacterAttributeEnum i)
    {
        switch (i)
        {
            case CharacterAttributeEnum.Strength:
                return strength;
            case CharacterAttributeEnum.Magic:
                return magic;
            case CharacterAttributeEnum.Skill:
                return skill;
            case CharacterAttributeEnum.Speed:
                return speed;
            case CharacterAttributeEnum.Defense:
                return defense;
            case CharacterAttributeEnum.Resistance:
                return resistance;
            default:
                Debug.LogWarning("Trying to return Attribute value which was not implemented into Get method yet");
                return -1;
        }
    }
}

[Serializable]
public class Level
{
    public int RequiredExperienceToLevelUp
    {
        get
        {
            return level * 1000;
        }
    }
    
    public int level = 1;
    public int experience = 0;

    public void AddExperience(int exp)
    {
        experience += exp;
    }

    public bool CheckLevelUp()
    {
        return experience >= RequiredExperienceToLevelUp;
    }

    public void LevelUp()
    {
        experience -= RequiredExperienceToLevelUp;
        level += 1;
    }
}

public enum CharacterStats
{
    HP,
    AttackRange,
    Accuracy,
    Dodge,
    CritChance,
    CritDamageMulitplicator,
    MovementPoints
}

[Serializable]
public class Stats
{
    public int hp = 100;
    public int attackRange = 1;
    public float accuracy = 0.75f;
    public float dodge = 0.1f;
    public float critChance = 0.1f;
    public float critDamageMulitplicator = 1.5f;
    public float movementPoints = 50f;

    public float GetFloatValue(CharacterStats characterStats)
    {
        switch (characterStats)
        {
            case CharacterStats.Accuracy:
                return accuracy;
            case CharacterStats.Dodge:
                return dodge;
            case CharacterStats.CritChance:
                return critChance;
            case CharacterStats.CritDamageMulitplicator:
                return critDamageMulitplicator;
            case CharacterStats.MovementPoints:
                return movementPoints;
            default:
                Debug.LogWarning("CHARACTER: Requesting incorrect character stats type");
                throw new ArgumentOutOfRangeException(nameof(characterStats), characterStats, null);
        }
    }

    public int GetIntValue(CharacterStats characterStats)
    {
        switch (characterStats)
        {
            case CharacterStats.HP:
                break;
            case CharacterStats.AttackRange:
                break;
            default:
                Debug.LogWarning("CHARACTER: Requesting incorrect character stats type");
                throw new ArgumentOutOfRangeException(nameof(characterStats), characterStats, null);
        }
        return 0;
    }
}

public class Character : MonoBehaviour
{
    public CharacterData characterData;
    
    public Int2Val hp = new Int2Val(100,100);
    public DamageType damageType;
    public bool defeated;

    public int GetDefenseValue(DamageType dt)
    {
        int def = 0;

        return def;
    }
    public int GetDamage()
    {
        int damage = 0;
        
        return damage;
    }
    
    /*private void Start()
    {
        if (attributes == null)
        {
            Init();
        }
    }

    public void Init()
    {
        attributes = new CharacterAttributes();
        level = new Level();
    }*/

    public void TakeDamage(int damage)
    {
        hp.Subtract(damage);
        Debug.Log("CHARACTER: Applying " + damage.ToString());
        CheckDefeat();
    }

    private void CheckDefeat()
    {
        if (hp.current <= 0)
        {
            Defeated();
        }
        else
        {
            Flinch();
        }
    }
    
    private CharacterAnimator characterAnimator;

    private void Flinch()
    {
        if (characterAnimator == null) { characterAnimator = GetComponentInChildren<CharacterAnimator>(); }

        characterAnimator.Flinch();
    }

    private void Defeated()
    {
        if (characterAnimator == null) { characterAnimator = GetComponentInChildren<CharacterAnimator>(); }
        
        defeated = true;
        characterAnimator.Defeated();
    }

    public void AddExperience(int exp)
    {
        characterData.AddExperience(exp);
    }

    public int GetIntValue(CharacterStats characterStat)
    {
        return characterData.GetIntValue(characterStat);
    }

    public float GetFloatValue(CharacterStats characterStat)
    {
        return characterData.GetFloatValue(characterStat);
    }

    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;
    }
}
