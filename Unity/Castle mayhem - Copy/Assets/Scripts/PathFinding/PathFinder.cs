using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //Dictionary<Vector2Int, Node> grid;


    void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();

    }
    void Start()
    {
        startNode = gridHandler.GetNode(startCoordinates);
        finishNode = gridHandler.GetNode(finishCoordinates);
        BreadthFirstSearch();

        BuildPath();
    }
    void ExploreNeighbors(Node current)
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in directions)
        {

            Vector2Int neighborCoords = current.coordinates + direction;
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
                neighbor.next = currentSearchNode;

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
            ExploreNeighbors(currentSearchNode);
            if (currentSearchNode.coordinates == finishCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node current = finishNode;

        path.Add(current);
        current.isPath = true;

        while (current.next != null)
        {
            current = current.next;
            path.Add(current);
            current.isPath = true;
        }

        path.Reverse();
        return path;

    }

}
