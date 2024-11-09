using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditionManager : MonoBehaviour
{
    [SerializeField] ForceContainer enemyForce;

    public void CheckPlayerVictory()
    {
        if (enemyForce.CheckDefeated() == true)
        {
            Debug.Log("Congratulation you are the winner!");
        }
    }
}
