using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinatesHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    private TextMeshPro textMeshPro;
    Transform parent;
    GridHandler gridHandler;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.enabled = false;
        gridHandler = FindObjectOfType<GridHandler>();

    }
    private void Update()
    {
        ToggleText();
        SetColor();
        if (Application.isPlaying)
            return;
        updateText();
    }
    private void ToggleText()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            textMeshPro.enabled = !textMeshPro.IsActive();
        }
    }
    private void SetColor()
    {
        //textMeshPro.color = ((waypoint.IsPlacable) ? defaultColor : placedColor);
        if (gridHandler == null)
            return;
        Node node = gridHandler.GetNode(GetCoordinates());
        if (node == null) return;

        if (!node.isWalkable)
            textMeshPro.color = blockedColor;
        else if (node.isPath)
            textMeshPro.color = pathColor;
        else if (node.isExplored)
        {
            textMeshPro.color = exploredColor;
        }
        else
            textMeshPro.color = defaultColor;

    }
    private Vector2Int GetCoordinates()
    {
        if (gridHandler == null) return new Vector2Int();

        int x = (int)Mathf.Round(transform.parent.position.x / gridHandler.WorldGridSize);
        int y = (int)Mathf.Round(transform.parent.position.z / gridHandler.WorldGridSize);



        return new Vector2Int(x, y);
    }
    private void updateText()
    {
        parent = this.transform.parent;
        textMeshPro.text = GetCoordinates().x + "," + GetCoordinates().y;
        parent.name = "(" + GetCoordinates().x + "," + GetCoordinates().y + ")";
    }

}
