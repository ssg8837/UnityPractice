using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 4f;

    [SerializeField] private float positionPitchFactor = -3f;
    [SerializeField] private float controlPitchFactor = -10f;

    [SerializeField] private float positionYawFactor = 3f;
    [SerializeField] private float controlRawFactor = -20f;
    
    private float xThrow, yThrow;
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotaion();

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
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * moveSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * moveSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
