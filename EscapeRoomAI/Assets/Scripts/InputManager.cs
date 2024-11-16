using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour // MonoBehavior is a base class in Unity from which all scripts that interact with the Unity engine must inherit. it provides access to Unity's core features and allows scripts to be attacked to GameObjects in a Unity scene
{
    // Start is called before the first frame update
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    [HideInInspector] public bool playerCanMove = true; // Used to prevent player movement during interactions

    void Awake() //special Unity MonoBehaviour method that is called when the script instance is being loaded
    {
        playerInput = new PlayerInput(); //create a new instance of the pLAYERiNPUT CLASS
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>(); // fetches the PlayerMotor component attached to the same GameObject, after getting the reference to the PlayerMotor, you can call methods from the PlayerMotor script, such as ProcessMove()
        look = GetComponent<PlayerLook>();
        //onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        
    } // the Awake() method initializes the player's input system by creating a new instance o fthe PlayerInput, and then it stores the "OnFoot" action map in the onFoot variable. This onFoot variable contains controls related to player movement.

    void FixedUpdate()
    {
        if (playerCanMove){
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>()); // onFoot contains various input actions related to player movement; ReadValue<Vector2>(): this function reads the current value of the Movement input as a Vector2 which contains two components: x and y.
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerCanMove){
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
    }
    
    private void OnEnable() // enable the OnFoot action map when GameObject is activated
    {
        onFoot.Enable(); 
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}


