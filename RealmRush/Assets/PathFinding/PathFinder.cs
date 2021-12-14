using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    //시작노드 위치
    [SerializeField] Vector2Int startCoordinates;
    //목적지점 노드 위치
    [SerializeField] Vector2Int destinateCoordinates;

    //시작 노드
    Node startNode;
    //목적지점 노드
    Node destinationNode;
    //(검색 메소드 처리 중) 현재 노드
    Node currentSearchNode;

    //노드를 탐색하는 큐
    Queue<Node> frontier = new Queue<Node>();
    //이미 탐색한 노드
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    //2차원으로 봤을 때 방향
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up , Vector2Int.down};
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

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
        startNode = grid[startCoordinates];
        destinationNode = grid[destinateCoordinates];
        BreadthFirstSearch();        
        BuildPath();
    }

    private void ExplorerNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in  directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            
            //grid.ContainsKey(neighborCoords);[키를 가지고 있나 체크함.]  
            Node neighborNode= gridManager.GetNode(neighborCoords);
            if(neighborNode != null)
            {
                neighbors.Add(neighborNode);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectionTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }
    private void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExplorerNeighbors();

            if(currentSearchNode.coordinates == destinateCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectionTo != null)
        {
            currentNode = currentNode.connectionTo;

            path.Add(currentNode);
        }

        return path;
    }
}
