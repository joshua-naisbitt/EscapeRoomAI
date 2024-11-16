using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float walkSpeed = 7f;  // Normal walking speed
    public float sprintSpeed = 12f;  // Sprinting speed
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public bool sprinting = false;
    public bool crouching = false;
    public bool lerpCrouch = false;
    public float crouchTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;// the isGrounded variable is updated every frame to reflect whether the player is currently on the ground
        // controller.isGrounded is a property of the CharacterController component that checks whether the player is touching the ground.
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        // Calculate the target direction based on player input
        Vector3 targetDirection = transform.TransformDirection(new Vector3(input.x, 0, input.y));

        // Set the target speed based on the sprinting state
        float targetSpeed = sprinting ? sprintSpeed : walkSpeed;
        Vector3 targetVelocity = targetDirection * targetSpeed;

        // Smoothly interpolate between current velocity and target velocity
        Vector3 smoothedVelocity = Vector3.Lerp(controller.velocity, targetVelocity, Time.deltaTime * 20f);

        // Apply movement
        controller.Move(smoothedVelocity * Time.deltaTime);

        // Apply gravity
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Reset gravity effect when grounded
        }
        playerVelocity.y += gravity * Time.deltaTime; // Add gravity
        controller.Move(new Vector3(0, playerVelocity.y, 0) * Time.deltaTime); // Apply vertical movement
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;

    }

    public void Sprint()
    {
        sprinting = !sprinting; // Toggle sprint state
    }

    //public void Jump()
    //{
    //    if (isGrounded) // can only jump when the player is on the ground
    //    {
    //        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    //    }
    //}
}
