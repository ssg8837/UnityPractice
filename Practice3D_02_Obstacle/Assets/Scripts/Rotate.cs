using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]private float xSpin;
    [SerializeField]private float ySpin;
    [SerializeField]private float zSpin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xSpin, ySpin, zSpin);
    }
}
