using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based on player input")]
    [SerializeField] private float moveSpeed = 10f;
    [Tooltip("Left Right Position Limit")][SerializeField] private float xRange = 5f;
    [Tooltip("Up Down Position Limit")][SerializeField] private float yRange = 4f;


    [Header("Screen Position based tuning")]
    [SerializeField] private float positionPitchFactor = -3f;
    [SerializeField] private float controlPitchFactor = -10f;

    [Header("Player input based tuning")]
    [SerializeField] private float positionYawFactor = 3f;
    [SerializeField] private float controlRawFactor = -20f;
    
    [Header("Input System")]
    [SerializeField] private InputAction fire;
    [SerializeField] private InputAction movement;
    [Header("For Paticle Check")]
    [SerializeField] private ParticleSystem[] lasers; 

    private float xThrow, yThrow;
    private void OnEnable() 
    {
        fire.Enable();
        movement.Enable();
    }

    private void OnDisable() 
    {
         fire.Disable();
         movement.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotaion();
        ProcessFiring();
    }

    private void ProcessRotaion()
    {
        float ptichDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = ptichDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor ;
        float raw = xThrow * controlRawFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, raw);
    }

    private void ProcessTranslation()
    {
        Vector2 vector2 = movement.ReadValue<Vector2>();
        xThrow = vector2.x;
        yThrow = vector2.y;

        float xOffset = xThrow * Time.deltaTime * moveSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * moveSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        // if(Input.GetButton("Fire1"))
        if(fire.ReadValue<float>() > 0.5)
        {
            ActiveLasers(true);
        }
        else
        {
            ActiveLasers(false);
        }

    }

    private void ActiveLasers(Boolean isActive)
    {
        foreach (ParticleSystem oneLaser in lasers)
        {
            var emissionModule= oneLaser.emission;
            emissionModule.enabled = isActive;
        }
    }

}
