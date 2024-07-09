using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> path = new List<GameObject>();
    [SerializeField][Range(0f, 5f)] float speed = 1.5f;


    void Start()
    {
        StartCoroutine(PrintWaypoints());
    }

    // Update is called once per frame
    IEnumerator PrintWaypoints()
    {
        foreach (GameObject waypoint in path)
        {
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = waypoint.transform.position;

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
    }
}
