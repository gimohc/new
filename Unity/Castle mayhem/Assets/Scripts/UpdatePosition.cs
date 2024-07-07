using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class UpdatePosition : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshPro textMeshPro;
    Transform parent;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        //parent = this.transform.parent.transform;
        //textMeshPro.text = parent.position.x / 10 + " , " + parent.position.z / 10;
    }
    private void Update()
    {
        if (Application.isPlaying) return;
        updateText();
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
