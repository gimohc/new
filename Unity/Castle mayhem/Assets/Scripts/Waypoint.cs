using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] bool isPlacable;
    void OnMouseDown()
    {
        if (isPlacable)
        {
            Instantiate(ballistaPrefab, transform.position, Quaternion.identity);
            isPlacable = false;
        }
    }
}
