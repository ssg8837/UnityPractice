using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    private Rigidbody rb;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;
            tr.Rotate(Vector3.forward * rotateThrust * Time.deltaTime);
            rb.freezeRotation = false;
        }
        else  if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;
            tr.Rotate(Vector3.back * rotateThrust * Time.deltaTime);
            rb.freezeRotation = false;
        }
    }
}
