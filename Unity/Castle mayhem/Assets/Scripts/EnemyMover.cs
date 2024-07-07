using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> path = new List<GameObject>();



    void Start()
    {
        StartCoroutine(PrintWaypoints());
    }

    // Update is called once per frame
    IEnumerator PrintWaypoints()
    {
        foreach (GameObject waypoint in path)
        {
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
