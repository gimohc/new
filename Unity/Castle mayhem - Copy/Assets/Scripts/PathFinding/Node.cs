using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node next;

    public Node(Vector2Int coords, bool isWalkable) {
        this.coordinates = coords;
        this.isWalkable = isWalkable;

    }


}
