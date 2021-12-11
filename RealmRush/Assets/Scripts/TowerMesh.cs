using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMesh : MonoBehaviour
{
    WayPoint wayPoint;
    // Start is called before the first frame update
    void Start()
    {
        wayPoint = GetComponentInParent<WayPoint>();
    }

    private void OnMouseDown()
    {
        wayPoint.OnMouseDown();
    }
}
