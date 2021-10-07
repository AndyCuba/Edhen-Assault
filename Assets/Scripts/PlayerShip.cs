using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 15f;
    [Tooltip("In ms^-1")][SerializeField] float ySpeed = 15f;
    [Tooltip("In m")][SerializeField] float xRange = 7f;
    [Tooltip("In m")][SerializeField] float yMax = 4f;
    [Tooltip("In m")][SerializeField] float yMin = -4f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    float yThrow, xThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessHorizontalMovement();
        HandleVerticalMovement();
        ProcessRotation();
    }

    private void ProcessHorizontalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);

        transform.localPosition = new Vector3(
            rawXPos,
            transform.localPosition.y,
            transform.localPosition.z
        );
    }

    private void HandleVerticalMovement()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPos = Mathf.Clamp(transform.localPosition.y + yOffset, yMin, yMax);

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            rawYPos,
            transform.localPosition.z
        );
    }


    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlFlow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlFlow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Dead");
    }
}
