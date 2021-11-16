using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMoves : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintWayPointName());
    }

   IEnumerator PrintWayPointName()
    {
        foreach(WayPoint wayPoint in path)
        {
          transform.position = wayPoint.transform.position;
          yield return new WaitForSeconds(1f);
        }
    }
    // // Update is called once per frame
    // void Update()
    // {
    // }
}
