using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheDoorController : Interactable
{
    [SerializeField] private string requiredKey; // The key required to open the door
    [SerializeField] private Animator doorAnimator; // Animator for the door opening animation

    private bool isOpen = false;
    void Start()
    {
        
        promptMessage = "This Door is locked find The Key to unlock it";
    }

    protected override void Interact()
    {
        if (isOpen)
        {
            Debug.Log("The door is already open.");
            return;
        }

        if (PlayerInventory.Instance.HasKey(requiredKey))
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("The door is locked. You need the key.");
        }
    }

    private void OpenDoor()
    {
        Debug.Log("The door is now open.");
        isOpen = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open"); // Trigger the door opening animation
        }
        else
        {
            // Fallback: Disable the door's collider or move it out of the way
            GetComponent<Collider>().enabled = false;
        }

        // Transition to the Game Over scene
        LoadGameOverScene();
    }

    private void LoadGameOverScene()
    {
        Debug.Log("Loading Game Over scene...");
        SceneManager.LoadScene(2);
    }
}
