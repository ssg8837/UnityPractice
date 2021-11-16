using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//언제나 실행
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
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
}
