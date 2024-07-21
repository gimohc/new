using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates
    {
        get { return startCoordinates; }
    }
    [SerializeField] Vector2Int finishCoordinates;
    public Vector2Int FinishCoordinates
    {
        get { return finishCoordinates; }
    }

    Node startNode;
    Node finishNode;
    Node currentSearchNode;

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> unexplored = new Queue<Node>();

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    GridHandler gridHandler;



    void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();
        startNode = gridHandler.GetNode(startCoordinates);
        finishNode = gridHandler.GetNode(finishCoordinates);

    }
    void Start()
    {

        BuildNewPath();


    }
    void ExploreNeighbors(Node current)
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in directions)
        {

            Vector2Int neighborCoords = current.coordinates + direction;
            //Debug.Log(neighborCoords);

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
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        finishNode.isWalkable = true;

        unexplored.Clear();
        reached.Clear();

        bool isRunning = true;
        unexplored.Enqueue(gridHandler.GetNode(coordinates));
        reached.Add(coordinates, gridHandler.GetNode(coordinates));
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
    private void ResetNodes()
    {
        gridHandler.ResetNodes();
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
    public bool WillBlockPath(Vector2Int coordinates)
    {
        Node node = gridHandler.GetNode(coordinates);
        if (node != null)
        {
            bool previousState = node.isWalkable;
            node.isWalkable = false;
            List<Node> newPath = BuildNewPath();
            node.isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                BuildNewPath();
                return true;
            }

        }
        return false;

    }
    public List<Node> BuildNewPath()
    {
        return BuildNewPath(startCoordinates);
    }
    public List<Node> BuildNewPath(Vector2Int coordinates)
    {
        ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }
    public void NotifyReceivers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }


}
