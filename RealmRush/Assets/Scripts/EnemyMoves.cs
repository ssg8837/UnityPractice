using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMoves : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] List<Tile> path = new List<Tile>();
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

        GameObject parent = GameObject.FindGameObjectWithTag("Paths");

        foreach(Transform child in parent.transform)
        {
            Tile wayPoint = child.GetComponent<Tile>();

            if(wayPoint != null)
            {
                path.Add(wayPoint);
            }
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

   IEnumerator FollowPath()
    {         
        foreach(Tile wayPoint in path)
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
        gameObject.SetActive(false);
    }
    // // Update is called once per frame
    // void Update()
    // {
    // }
}
