using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] bool isPlacable;
    GridHandler gridHandler;
    Vector2Int coordinates = new Vector2Int();
    void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();
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
        if (isPlacable && Bank.Instance.Balance() >= price)
        {
            Instantiate(ballistaPrefab, transform.position, Quaternion.identity);
            Bank.Instance.ConsumeMoney(price);
            isPlacable = false;
        }
    }

}
