using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlaceable;

    //마우스가 올려져 있을 때
    // private void OnMouseOver() 
    
    //당연히 눌렸을 때
    private void OnMouseDown() 
    {
        if(isPlaceable)
        {
            //배치하기 1을 2에 위치하며 3 방향으로 돌린다.
            Instantiate(towerPrefab, transform.position, Quaternion.identity);

            isPlaceable = false;
        }
    }
}
