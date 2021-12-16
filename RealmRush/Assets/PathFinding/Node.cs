using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour 를 사용하지 않음으로써 드래그해서 붙여넣는건 불가능해짐
[System.Serializable]
public class Node //: MonoBehaviour
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;

    public bool isPlaceable;
    public Node connectionTo;

    public Node(Vector2Int coordinates, bool isWalkabe)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkabe;

    }

}
