using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

[Serializable]
public class CharacterAttributes
{
    public int strength;
    public int magic;
    public int skill;
    public int speed;
    public int defense;
    public int resistance;

    public CharacterAttributes()
    {
        
    }
}

public class Character : MonoBehaviour
{
    public CharacterAttributes attributes;
    
    public string Name = "Nameless";
    public float movementPoints = 50f;
    public Int2Val hp = new Int2Val(100,100);
    public int attackRange = 1;
    public float accuracy = 0.75f;
    public float dodge = 0.1f;
    public float critChance = 0.1f;
    public float critDamageMulitplicator = 1.5f;
    public DamageType damageType;
    public bool defeated;

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
    public int GetDamage()
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
    
    private void Start()
    {
        if (attributes == null)
        {
            Init();
        }
    }

    public void Init()
    {
        attributes = new CharacterAttributes();
    }

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
}
