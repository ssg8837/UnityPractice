using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMesh : MonoBehaviour
{
    Tile wayPoint;
    // Start is called before the first frame update
    void Start()
    {
        wayPoint = GetComponentInParent<Tile>();
    }

    private void OnMouseDown()
    {
        wayPoint.OnMouseDown();
    }
}
