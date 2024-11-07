using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        targetGridObject.GetComponent<Character>().TakeDamage(character.damage);
    }

    private void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
