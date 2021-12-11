using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] public bool isPlaceable;
    [SerializeField] public float towerY= 0f;

    //마우스가 올려져 있을 때
    // private void OnMouseOver() 
    
    //당연히 눌렸을 때
    public void OnMouseDown() 
    {
        if(isPlaceable)
        {

            Vector3 vecOnTower = transform.position;
            vecOnTower.y= vecOnTower.y + towerY;
            bool isPlaced = towerPrefab.CreateTower(towerPrefab,vecOnTower);


            //배치하기 1을 2에 위치하며 3 방향으로 돌린다.
            //Instantiate(towerPrefab, transform.position, Quaternion.identity);

            isPlaceable = !isPlaced;
        }
    }
}
