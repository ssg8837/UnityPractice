using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float audioVolume =0.5f;
    [SerializeField] ParticleSystem mainJetParticle;
    [SerializeField] ParticleSystem leftJetParticle;
    [SerializeField] ParticleSystem rightJetParticle;
    private Rigidbody rb;
    private Transform tr;
    private AudioSource audioSource;
    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = audioVolume;
        audioSource.clip = mainEngine;
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        audioSource.mute =true;
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            audioSource.mute =false;
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!mainJetParticle.isPlaying)
            {
                mainJetParticle.Play();
            }
        }
         else
         {
             mainJetParticle.Stop();
         }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rocketRotate(leftJetParticle, Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rocketRotate(rightJetParticle, Vector3.back);
        }
        else
        {
            leftJetParticle.Stop();
            rightJetParticle.Stop();
        }
    }

    private void rocketRotate(ParticleSystem particleSystem, Vector3 vector3)
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
        rb.freezeRotation = true;
        tr.Rotate(vector3 * rotateThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
