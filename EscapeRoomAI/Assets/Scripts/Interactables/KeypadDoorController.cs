using UnityEngine;

public class KeypadDoorController : Interactable
{
    private Animator animator;
    private bool isOpen = false;
    private bool isUnlocked = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        promptMessage = "Solve the puzzle to open the door";
    }

    protected override void Interact()
    {
        if (isUnlocked)
        {
            isOpen = !isOpen;
            animator.SetBool("isOpen", isOpen);
        }
        else
        {
            Debug.Log("The door is locked. Enter the correct code to unlock it.");
        }
    }

    public void UnlockDoor() // Method to unlock and open the door
    {
        isUnlocked = true;
        isOpen = true;
        animator.SetBool("isOpen", true);
        Debug.Log("Door unlocked and opened!");
    }
}
