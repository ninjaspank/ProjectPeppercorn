using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryConditionManager : MonoBehaviour
{
    [SerializeField] ForceContainer enemyForce;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private MouseInput mouseInput;

    public void CheckPlayerVictory()
    {
        if (enemyForce.CheckDefeated() == true)
        {
            Victory();
        }
    }

    private void Victory()
    {
        mouseInput.enabled = false;
        victoryPanel.SetActive(true);
        Debug.Log("Congratulation you are the winner!");
    }

    public void ReturnToWorldMap()
    {
        SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
    }
}
