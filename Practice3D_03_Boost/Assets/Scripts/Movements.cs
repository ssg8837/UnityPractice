using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    private Rigidbody rb;
    private Transform tr;
    private AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        aS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        aS.mute =true;
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            aS.mute =false;
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            // if(!aS.isPlaying)
            //     aS.Play();
        }
        // else
        //     aS.Stop();
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
