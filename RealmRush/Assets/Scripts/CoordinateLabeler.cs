using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//언제나 실행
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.gray;
    TextMeshPro label;
    WayPoint wayPoint;
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
        wayPoint = GetComponentInParent<WayPoint>();
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = string.Format("{0},{1}",coordinates.x,coordinates.y);
    }
    private void UpdateObjectNames()
    {
        transform.parent.name = string.Format("({0})",label.text);
    }

    void SetLabelColor()
    {
        if(wayPoint.isPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
}
