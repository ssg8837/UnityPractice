using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    private MeshRenderer dropperRenderer;
    private Rigidbody dropperRigidbody;
    [SerializeField]private float timeToWait = 3f;
    // Start is called before the first frame update
    void Start()
    {
        dropperRenderer = GetComponent<MeshRenderer>();
        dropperRigidbody = GetComponent<Rigidbody>();

        dropperRenderer.enabled = false;
        dropperRigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToWait)
        {
            dropperRenderer.enabled = true;
            dropperRigidbody.useGravity = true;
        }
    }
}
