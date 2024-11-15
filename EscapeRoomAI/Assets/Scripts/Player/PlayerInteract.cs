using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private Interactable winston;
    // Uncommment line below to use input manager: 

    //private InputManager inputManager;


    // Start is called before the first frame update
    void Start()
    {
        winston = GameObject.FindGameObjectWithTag("Winston").GetComponent<Interactable>();
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        // Uncommment line below to use input manager: 
        // inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            winston.BaseInteract();
        }
        playerUI.UpdateText(string.Empty);  // Clear prompt each frame

        // Set up ray for detecting interactable objects in front of the player
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // Check if the object hit by the ray is interactable
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                // Display the prompt message
                playerUI.UpdateText(interactable.promptMessage);

                // Check for left mouse click to interact
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.BaseInteract();
                }
                // Uncommment block below and comment block above to use input manager
                // if (inputManager.onFoot.Interact.triggered)
                // {
                //     interactable.BaseInteract();
                // }
            }
        }
    }
}
