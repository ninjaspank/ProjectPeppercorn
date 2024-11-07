using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script tells the Unity animator what animations to play
/// </summary>
public class CharacterAnimator : MonoBehaviour
{
    /// <summary>
    /// This is the Unity component that actually plays the animations
    /// </summary>
    Animator animator;

    [SerializeField] bool move;
    [SerializeField] bool attack;
    [SerializeField] private bool defeated;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMoving()
    {
        move = true;
    }

    public void StopMoving()
    {
        move = false;
    }

    public void Defeated()
    {
        defeated = true;
    }

    public void Attack()
    {
        attack = true;
    }

    private void LateUpdate()
    {
        animator.SetBool("Move", move);
        animator.SetBool("Attack", attack);
        animator.SetBool("Defeated", defeated);
        
        if (attack == true)
        {
            attack = false;
        }
    }

    public void Flinch()
    {
        animator.SetTrigger(("Pain"));
    }
}
