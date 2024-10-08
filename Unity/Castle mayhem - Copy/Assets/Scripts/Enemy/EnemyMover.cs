using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyDamage))]
public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField][Range(0f, 5f)] float speed = 1.5f;

    //Enemy enemy;
    GridHandler gridHandler;
    PathFinder pathFinder;
    void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();
        pathFinder = FindObjectOfType<PathFinder>();
    }
    void RecalculatePath(bool reset)
    {
        //path.Clear();
        Vector2Int coordinates = new Vector2Int();
        if (reset)
            coordinates = pathFinder.StartCoordinates;
        else
            coordinates = gridHandler.GetCoordinatesFromPosition(transform.position);
        StopAllCoroutines();
        path = pathFinder.BuildNewPath(coordinates);
        StartCoroutine(TravelPath());

    }
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        //TravelPath();

    }
    private void ReturnToStart()
    {
        transform.position = gridHandler.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }


    // Update is called once per frame
    IEnumerator TravelPath()
    {
        //Debug.Log(path.Count);
        for (int i = 1; i < path.Count; i++)
        //foreach(Node element in path)
        {
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = gridHandler.GetPositionFromCoordinates(path[i].coordinates);

            //Debug.Log(path[i].coordinates);

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
        gameObject.SetActive(false);

        Player.Instance.GetDamaged(
            GetComponent<EnemyDamage>().GetPenalty()
        );
    }
}
