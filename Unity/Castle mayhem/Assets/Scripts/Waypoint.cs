using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] bool isPlacable;
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
