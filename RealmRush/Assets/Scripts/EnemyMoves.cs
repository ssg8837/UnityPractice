using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMoves : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    // Start is called before the first frame update
    [SerializeField] [Range(0f, 5f)] float speed = 1f; 
    private void OnEnable() 
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start() 
    {
        enemy = GetComponent<Enemy>();
    }
    
    private void FindPath()
    {
        path.Clear();

        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("Paths");

        foreach(GameObject wayPoint in wayPoints)
        {
            //
            path.Add(wayPoint.GetComponent<WayPoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

   IEnumerator FollowPath()
    {         
        foreach(WayPoint wayPoint in path)
        {
            //시작점
            Vector3 startPosition = transform.position;
            //도착점
            Vector3 endPosition = wayPoint.transform.position;
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
        enemy.SteaGold();
        ReturnToStart();
        gameObject.SetActive(false);
    }
    // // Update is called once per frame
    // void Update()
    // {
    // }
}
