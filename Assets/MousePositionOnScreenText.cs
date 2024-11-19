using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionOnScreen : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI positionOnScreen;
    private MouseInput mouseInput;
    
    // Start is called before the first frame update
    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseInput.active == true)
        {
            positionOnScreen.text = "Position " + mouseInput.positionOnGrid.x.ToString() + ":" + mouseInput.positionOnGrid.y;
        }
        else
        {
            positionOnScreen.text = "Outside";
        }
    }
}
