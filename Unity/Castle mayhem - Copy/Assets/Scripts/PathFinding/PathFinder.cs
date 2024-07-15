using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int finishCoordinates;
    Node startNode;
    Node finishNode;
    Node currentSearchNode;

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> unexplored = new Queue<Node>();

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    GridHandler gridHandler;

    Dictionary<Vector2Int, Node> grid;


    // search algorithm isnt working 
    
    void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();

        startNode = new Node(startCoordinates, true);
        finishNode = new Node(finishCoordinates, true);

    }
    void Start()
    {
        BreadthFirstSearch();
    }
    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            currentSearchNode = gridHandler.GetNode(new Vector2Int(Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x),
                Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z)));

            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            Debug.Log(neighborCoords);

            Node node = gridHandler.GetNode(neighborCoords);
            if (node == null)
                continue;
            neighbors.Add(node);


        }
        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                unexplored.Enqueue(neighbor);
                reached.Add(neighbor.coordinates, neighbor);
            }
        }
    }
    void BreadthFirstSearch()
    {
        bool isRunning = true;
        unexplored.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);
        while (unexplored.Count > 0 && isRunning)
        {
            currentSearchNode = unexplored.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == finishCoordinates)
            {
                isRunning = false;
            }
        }
    }

}
