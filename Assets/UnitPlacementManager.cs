using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

public class UnitPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterModel;

    private List<GameObject> characterObjects = new List<GameObject>();

    [SerializeField] private SquadData squadData;

    private List<PlaceableUnitNode> nodes;
    private MouseInput mouseInput;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        characterObjects = new List<GameObject>();

        for (int i = 0; i < squadData.charactersInTheSquad.Count; i++)
        {
            InitCharacter(squadData.charactersInTheSquad[i]);
        }
    }

    private void InitCharacter(CharacterData characterData)
    {
        GameObject newCharacterGameObject = Instantiate(characterPrefab);
        GameObject newCharacterModel = Instantiate(characterModel);

        newCharacterModel.transform.parent = newCharacterGameObject.transform;

        newCharacterGameObject.GetComponent<Character>().SetCharacterData(characterData);
        
        characterObjects.Add(newCharacterGameObject);
    }

    public void AddMe(PlaceableUnitNode placeableUnitNode)
    {
        if (nodes == null) { nodes = new List<PlaceableUnitNode>(); }
        
        nodes.Add(placeableUnitNode);
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceableUnitNode placeNode = nodes.Find(x => x.gridObject.positionOnGrid == mouseInput.positionOnGrid);
            if (placeNode != null)
            {
                Debug.Log("UNITPLACEMENTMANAGER: We are clicking on the placeable node in position: " + mouseInput.positionOnGrid);
                if (placeNode.character == null)
                {
                    PlaceCharacter(placeNode, characterObjects[0]);
                }
            }
        }
    }

    private void PlaceCharacter(PlaceableUnitNode placeNode, GameObject characterObject)
    {
        characterObject.transform.position = placeNode.transform.position;
        placeNode.character = characterObject.GetComponent<Character>();

        characterObjects.Remove(characterObject);
    }
}
