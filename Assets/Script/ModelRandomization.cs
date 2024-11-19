using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ModelRandomization : MonoBehaviour
{
    [SerializeField] private List<GameObject> parts;
    [SerializeField] private List<float> probability;

    private void Start()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (probability[i] <= Random.value)
            {
                parts[i].SetActive(false);
            }
        }
    }
}
