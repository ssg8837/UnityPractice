using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMoves : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    // Start is called before the first frame update
    [SerializeField] [Range(0f, 5f)] float speed = 1f; 
    void Start()
    {
        StartCoroutine(FollowPath());
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
    }
    // // Update is called once per frame
    // void Update()
    // {
    // }
}
