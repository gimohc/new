using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] bool isPlacable;
    GridHandler gridHandler;
    Vector2Int coordinates = new Vector2Int();
    PathFinder pathFinder;
    void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();
        pathFinder = FindObjectOfType<PathFinder>();
    }
    void Start()
    {
        if (gridHandler != null)
        {
            coordinates = gridHandler.GetCoordinatesFromPosition(transform.position);
            if (!isPlacable)
            {
                gridHandler.BlockNode(coordinates);
            }
        }
    }
    public bool IsPlacable
    {
        get
        {
            return isPlacable;
        }
    }

    void OnMouseDown()
    {
        int price = ballistaPrefab.GetComponent<FocusEnemy>().GetPrice();
        Node current = gridHandler.GetNode(coordinates);
            
        if (current != null && current.isWalkable && Bank.Instance.Balance() >= price && !pathFinder.WillBlockPath(coordinates))
        {
            Instantiate(ballistaPrefab, transform.position, Quaternion.identity);
            Bank.Instance.ConsumeMoney(price);
            isPlacable = false;
            
            {
                current.isWalkable = false;
            }

        }
    }

}
