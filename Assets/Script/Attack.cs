using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DamageType
{
    Physical,
    Magic
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

        int damage = character.GetDamage();

        if (UnityEngine.Random.value <= character.critChance)
        {
            damage = (int)(damage * character.critDamageMulitplicator);
            Debug.Log("ATTACK: Critical Strike! Damage: " + damage + " instead of " + character.GetDamage());
        }

        damage -= target.GetDefenseValue(character.damageType);

        if (damage <= 0)
        {
            damage = 1;
        }

        DamageType damageType = character.damageType;
        if (damageType == DamageType.Physical)
        {
            Debug.Log("ATTACK: Target takes: " + damage.ToString() + " damage, minus " + target.GetDefenseValue(character.damageType).ToString() + " armor");
        }
        else
        {
            Debug.Log("ATTACK: Target takes: " + damage.ToString() + " damage, minus " + target.GetDefenseValue(character.damageType).ToString() + " resistance");
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
