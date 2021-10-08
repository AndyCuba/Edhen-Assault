using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShipController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 15f;
    [Tooltip("In ms^-1")][SerializeField] float ySpeed = 15f;
    [Tooltip("In m")][SerializeField] float xRange = 7f;
    [Tooltip("In m")][SerializeField] float yMax = 4f;
    [Tooltip("In m")][SerializeField] float yMin = -4f;

    [Header("Screen position factors")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw factors")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float yThrow, xThrow;

    bool isControlEnabled = true;

    void Update()
    {
        if (isControlEnabled)
        {
            ProcessHorizontalMovement();
            ProcessVerticalMovement();
            ProcessRotation();
        }
    }

    void OnPlayerDeath() // Called by string reference
    {
        print("Dead");
        // isControlEnabled = false;
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

    private void ProcessVerticalMovement()
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
}
