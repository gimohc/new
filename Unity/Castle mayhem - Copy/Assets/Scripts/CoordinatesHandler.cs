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
    [SerializeField] Color placedColor = Color.gray;
    private TextMeshPro textMeshPro;
    private Waypoint waypoint;
    Transform parent;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();

    }
    private void Update()
    {
        ToggleText();
        SetColor();
        if (Application.isPlaying) return;
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
        textMeshPro.color = ((waypoint.IsPlacable) ? defaultColor : placedColor);
        /*if (waypoint.IsPlacable) {
            textMeshPro.color = defaultColor;
        }
        else textMeshPro.color = placedColor;*/
    }
    private void updateText()
    {
        parent = this.transform.parent;
        int x = (int)Mathf.Round(parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        int y = (int)Mathf.Round(parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        textMeshPro.text = x + " , " + y;
        updateName(x, y);
    }
    private void updateName(int x, int y)
    {
        parent.name = "(" + x + "," + y + ")";
    }
}
