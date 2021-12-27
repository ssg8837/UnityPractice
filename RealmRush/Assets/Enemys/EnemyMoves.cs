using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMoves : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] List<Node> path = new List<Node>();
    // Start is called before the first frame update
    [SerializeField] [Range(0f, 5f)] float speed = 1f;     
    GridManager gridManager;
    PathFinder pathfinder;

    private void OnEnable() 
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    private void Awake() 
    {
        enemy = GetComponent<Enemy>();        
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();

    }
    
    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();

        path.Clear();

        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
        //StartCoroutine(FollowPath());

        // foreach(GameObject tile in tiles) 
        // {
        //     Tile wayPoint = tile.GetComponent<Tile>();

        //     if(wayPoint != null)
        //     {
        //         path.Add(wayPoint);
        //     }
        // }
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.SteaGold();
        gameObject.SetActive(false);
    }

   IEnumerator FollowPath()
    {         
        for(int i = 1; i < path.Count; i++) 
        {
            //시작점
            Vector3 startPosition = transform.position;
            //도착점
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            //보간시 사용하는 퍼센트
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            //100퍼까지
            while(travelPercent < 1f) 
            {
                travelPercent += Time.deltaTime * speed;
                //선형보간
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
    // // Update is called once per frame
    // void Update()
    // {
    // }
}
