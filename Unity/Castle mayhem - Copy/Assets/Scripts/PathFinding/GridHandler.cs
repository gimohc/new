using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("should match unity world gridsize")]
    [SerializeField] int worldGridSize = 10;// = UnityEditor.EditorSnapSettings.move.x;

    public int WorldGridSize {get { return worldGridSize; }}

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Node GetNode(Vector2Int key) {
        if(grid.ContainsKey(key))
            return grid[key];
        return null;
    } 
    void Awake()
    {

        CreateGrid();

    }


    // gridSize doesnt change, doesnt matter where the starting or ending point is. which might cause an issue later
    void CreateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coords = new Vector2Int(i, j);
                grid.Add(coords, new Node(coords, true));
            }
        }

    }
    public void ResetNodes() {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid) {
            entry.Value.next = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public void BlockNode(Vector2Int coordinates) {
        if(grid.ContainsKey(coordinates)) {
            grid[coordinates].isWalkable = false;

        }
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
        int x = (int)Mathf.Round(position.x / worldGridSize);
        int y = (int)Mathf.Round(position.z / worldGridSize);
        

        return new Vector2Int(x,y);
    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates) {
        return new Vector3(coordinates.x/worldGridSize, 0, coordinates.y/worldGridSize);
        
    }
}
