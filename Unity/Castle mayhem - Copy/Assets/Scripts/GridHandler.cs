using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        CreateGrid();

    }
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
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
