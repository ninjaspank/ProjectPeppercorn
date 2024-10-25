using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Gridmap gridmap;
    [SerializeField] GameObject highlightPoint;
    List<GameObject> highlightPointsGO;
    [SerializeField] GameObject container;

    // Start is called before the first frame update
    void Awake()
    {
        gridmap = GetComponentInParent<Gridmap>();
        highlightPointsGO = new List<GameObject>();
        
        ///Highlight(testTargetPosition);
    }

    private GameObject CreatePointHighlightObject()
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPointsGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetHighlightPointGO(i));
        }
    }
    
    public void Highlight(List<PathNode> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].pos_x, positions[i].pos_y, GetHighlightPointGO(i));
        }
    }

    private GameObject GetHighlightPointGO(int i)
    {
        if (highlightPointsGO.Count < i)
        {
            return highlightPointsGO[i];
        }

        GameObject newHighlightObject = CreatePointHighlightObject();
        return newHighlightObject;
    }

    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
        highlightObject.SetActive(true);
        Vector3 position = gridmap.GetWorldPosition(posX, posY, true);
        position += Vector3.up * 0.2f;
        highlightObject.transform.position = position;
    }

    public void Hide()
    {
        for (int i = 0; i < highlightPointsGO.Count; i++)
        {
            highlightPointsGO[i].SetActive(false);
        }
    }
}
