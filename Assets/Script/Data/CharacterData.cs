using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public string Name = "Nameless";
    
    public CharacterAttributes attributes;
    public CharacterAttributes levelUpRates;
    public Level level;

    public Stats stats;

    public int GetDefenseValue(DamageType dt)
    {
        int def = 0;

        switch (dt)
        {
            case DamageType.Physical:
                def += attributes.defense;
                break;
            case DamageType.Magic:
                def += attributes.resistance;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dt), dt, null);
        }

        return def;
    }
    public int GetDamage(DamageType damageType)
    {
        int damage = 0;

        switch (damageType)
        {
            case DamageType.Physical:
                damage += attributes.strength;
                break;
            case DamageType.Magic:
                damage += attributes.magic;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return damage;
    }
    
    public void AddExperience(int exp)
    {
        level.AddExperience(exp);
        if (level.CheckLevelUp())
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level.LevelUp();
        LevelUpAttributes();
    }
    
    private void LevelUpAttributes()
    {
        for (int i = 0; i < CharacterAttributes.AttributeCount; i++)
        {
            int rate = levelUpRates.Get((CharacterAttributeEnum)i);
            rate += UnityEngine.Random.Range(0, 100);
            rate /= 100;
            if (rate > 0)
            {
                attributes.Sum((CharacterAttributeEnum)i, rate);
            }
        }
    }

    public int GetIntValue(CharacterStats characterStat)
    {
        return stats.GetIntValue(characterStat);
    }

    public float GetFloatValue(CharacterStats characterStat)
    {
        return stats.GetFloatValue(characterStat);
    }
}
