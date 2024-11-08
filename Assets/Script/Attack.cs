using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DamageType
{
    Physical,
    Magical
}

public class Attack : MonoBehaviour
{
    private GridObject gridObject;
    private CharacterAnimator characterAnimator;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    public void AttackGridObject(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);
        characterAnimator.Attack();
        
        //check accuracy for miss
        if (Random.value >= character.accuracy)
        {
            Debug.Log("ATTACK: Miss!"); 
            return;
        }

        Character target = targetGridObject.GetComponent<Character>();
        
        //check evasion
        if (UnityEngine.Random.value <= target.dodge)
        {
            Debug.Log("ATTACK: Dodge!");
            return;
        }

        int damage = character.damage;

        if (UnityEngine.Random.value <= character.critChance)
        {
            damage = (int)(damage * character.critDamageMulitplicator);
            Debug.Log("ATTACK: Critical Strike! Damage: " + damage + " instead of " + character.damage);
        }

        switch (character.damageType)
        {
            case DamageType.Physical:
                //apply armor value
                damage -= target.armor;
                Debug.Log("ATTACK: Physical attack received");
                break;
            case DamageType.Magical:
                //apply resistance value
                damage -= target.resistance;
                Debug.Log("ATTACK: Magical attack received");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (damage <= 0)
        {
            damage = 1;
        }

        DamageType damageType = character.damageType;
        if (damageType == DamageType.Physical)
        {
            Debug.Log("ATTACK: Target takes: " + damage.ToString() + " damage, minus " + target.armor.ToString() + " armor");
        }
        else
        {
            Debug.Log("ATTACK: Target takes: " + damage.ToString() + " damage, minus " + target.resistance.ToString() + " resistance");
        }
        target.TakeDamage(damage);
        
    }

    private void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
