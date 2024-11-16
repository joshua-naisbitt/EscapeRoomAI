using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    private Vector2 currentRotation;
    private Vector2 rotationVelocity;
    public float smoothTime = 0.1f;
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Hide the cursor
        Cursor.visible = false;
    }


    public void ProcessLook(Vector2 input)
    {
        // Reverse the input.y to fix the inverted vertical rotation
        float targetX = currentRotation.x - input.y * ySensitivity * Time.deltaTime; // Subtract input.y
        float targetY = currentRotation.y + input.x * ySensitivity * Time.deltaTime;

        // Smoothly interpolate rotation values
        currentRotation.x = Mathf.SmoothDamp(currentRotation.x, targetX, ref rotationVelocity.x, smoothTime);
        currentRotation.y = Mathf.SmoothDamp(currentRotation.y, targetY, ref rotationVelocity.y, smoothTime);

        // Clamp vertical rotation
        xRotation = Mathf.Clamp(currentRotation.x, -80f, 80f);

        // Apply rotations to the camera and the player
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, currentRotation.y, 0);
    }
}
