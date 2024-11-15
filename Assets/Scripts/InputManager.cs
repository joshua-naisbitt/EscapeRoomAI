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
    private GameObject inventoryCanvas;
    private bool isInventoryVisible = false;

    void Awake() //special Unity MonoBehaviour method that is called when the script instance is being loaded
    {
        playerInput = new PlayerInput(); //create a new instance of the pLAYERiNPUT CLASS
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>(); // fetches the PlayerMotor component attached to the same GameObject, after getting the reference tot he PlayerMotor, you cna call methods from the PlayerMotor script, such as ProcessMove()
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();

        
        
    } // the Awake() method initializes the player's input system by creating a new instance o fthe PlayerInput, and then it stores the "OnFoot" action map in the onFoot variable. This onFoot variable contains controls related to player movement.


void Start()
    {
        // Find the inventory canvas object by tag
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory");

        if (inventoryCanvas != null)
        {
            // Initially hide the inventory canvas
            inventoryCanvas.SetActive(isInventoryVisible);
        }
        else
        {
            Debug.LogWarning("Inventory canvas not found. Make sure the canvas is tagged 'Inventory'.");
        }

         // Populate inventory items
            InventoryManager.Instance.ListItems();
    }

     void Update()
    {
        // Check if "Q" key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }
    }
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>()); // onFoot contains various input actions related to player movement; ReadValue<Vector2>(): this function reads the current value of the Movement input as a Vector2 which contains two components: x and y.
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

    }
    
    private void OnEnable() // enable the OnFoot action map when GameObject is activated
    {
        onFoot.Enable(); 
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    void ToggleInventory()
    {
    // Toggle the inventory visibility
    isInventoryVisible = !isInventoryVisible;
    inventoryCanvas.SetActive(isInventoryVisible);

        if (isInventoryVisible)
        {
            // Call ListItems to populate the inventory UI when it's visible
            InventoryManager.Instance.ListItems();
        }
        else
        {
            // Optionally, clear the item list UI when the inventory is closed
            foreach (Transform child in InventoryManager.Instance.itemContent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}


