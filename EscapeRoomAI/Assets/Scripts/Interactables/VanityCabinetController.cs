using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanityCabinetController : Interactable
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        // Initialize the animator and set up the prompt message if inherited
        animator = GetComponent<Animator>();
        promptMessage = "Click to open/close the vanity"; // Ensure promptMessage is accessible
    }

    protected override void Interact()
    {
        // Toggle the drawer's open state
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
