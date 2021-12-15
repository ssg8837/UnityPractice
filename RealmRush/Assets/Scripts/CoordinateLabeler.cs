using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//언제나 실행
[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    TextMeshPro label;
    //WayPoint wayPoint;
    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        if(Application.isPlaying)
        {
            label.enabled = false;
        }
        else
        {
            label.enabled = true;
        }
        //wayPoint = GetComponentInParent<WayPoint>();
        gridManager = FindObjectOfType<GridManager>();
        DisPlayCoordinates();

    }
    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisPlayCoordinates();
            UpdateObjectNames();
        }
        SetLabelColor();
        ToggleLabels();
    }

    private void DisPlayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = string.Format("{0},{1}",coordinates.x,coordinates.y);
    }
    private void UpdateObjectNames()
    {
        transform.parent.name = string.Format("({0})",label.text);
    }

    private void SetLabelColor()
    {
        if(gridManager == null)
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates);
        
        if(node == null)
        {
            return;
        }
                
        if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else
        {
            label.color = defaultColor;
        }

        // if(wayPoint.isPlaceable)
        // {
        //     label.color = defaultColor;
        // }
        // else
        // {
        //     label.color = blockedColor;
        // }
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
}
