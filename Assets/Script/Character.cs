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

public class Character : MonoBehaviour
{
    public string Name = "Nameless";
    public float movementPoints = 50f;
    public Int2Val hp = new Int2Val(100,100);
    public int attackRange = 1;
    public int damage = 20;
    public bool defeated;

    public void TakeDamage(int damage)
    {
        hp.Subtract(damage);
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
