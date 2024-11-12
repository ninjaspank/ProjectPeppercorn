using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTestManager : MonoBehaviour
{
    public Character targetCharacter;

    public void AddExperience(int exp)
    {
        targetCharacter.AddExperience(exp);
    }
}
