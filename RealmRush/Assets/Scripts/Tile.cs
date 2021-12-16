using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] public bool isPlaceable;
    [SerializeField] public bool isWalkale;
    [SerializeField] public float towerY= 0f;

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();    

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isWalkale)
            {
                gridManager.BlockNode(coordinates);
            }
            gridManager.PlaceableNode(coordinates, isPlaceable);
        }
    }
    //마우스가 올려져 있을 때
    // private void OnMouseOver() 
    
    //당연히 눌렸을 때
    public void OnMouseDown() 
    {
        if(gridManager.GetNode(coordinates).isPlaceable && !pathFinder.WillBlockPath(coordinates))
        {

            Vector3 vecOnTower = transform.position;
            vecOnTower.y= vecOnTower.y + towerY;
            bool isPlaced = towerPrefab.CreateTower(towerPrefab,vecOnTower);


            //배치하기 1을 2에 위치하며 3 방향으로 돌린다.
            //Instantiate(towerPrefab, transform.position, Quaternion.identity);

            isPlaceable = !isPlaced;
            if(isPlaced)
            {
                isWalkale = !isPlaced;
            
                gridManager.BlockNode(coordinates);
                BuildPath();
            }
            gridManager.PlaceableNode(coordinates, isPlaceable);
        }
    }
}
