using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> path = new List<Transform>();
    [SerializeField][Range(0f, 5f)] float speed = 1.5f;

    private void Awake()
    {
        path.Clear();
        GameObject waypoints = GameObject.FindGameObjectWithTag("Path");//.GetComponentsInChildren<Waypoint>();
        foreach (Transform child in waypoints.transform)
        {
            path.Add(child);
        }
        transform.position = path[0].transform.position;
    }

    void Start()
    {
        StartCoroutine(TravelPath());

    }

    // Update is called once per frame
    IEnumerator TravelPath()
    {
        foreach (Transform waypoint in path)
        {
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = waypoint.position;

            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += (Time.deltaTime * speed);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            //yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}
