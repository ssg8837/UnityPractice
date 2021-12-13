using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Node currentSearchNode;
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up , Vector2Int.down};
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid;

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    private void Start() 
    {
        ExplorerNeighbors();        
    }

    private void ExplorerNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in  directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates = direction;\
            Node neighborNode= gridManager.GetNode(neighborCoords);
            if(neighborNode != null)
            {
                neighbors.Add(neighborNode);
            }
        }
    }
}
