using UnityEngine;

public class ButtonPuzzleDoorController : Interactable
{
    private Animator animator;
    private bool isOpen = false;
    private bool isUnlocked = false;

    public PuzzleButton[] buttonSequence; // Array to hold the buttons in the correct order
    private int currentButtonIndex = 0;   // Tracks which button in the sequence should be pressed next

    void Start()
    {
        animator = GetComponent<Animator>();
        promptMessage = "Solve the button sequence to open the door";
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
            Debug.Log("The door is locked. Solve the button sequence to unlock it.");
        }
    }

    public void VerifyButtonOrder(PuzzleButton button)
    {
        // Check if the pressed button is the next correct button in the sequence
        if (buttonSequence[currentButtonIndex] == button)
        {
            currentButtonIndex++;

            // If the sequence is complete, unlock the door
            if (currentButtonIndex >= buttonSequence.Length)
            {
                isUnlocked = true;
                Debug.Log("Button sequence solved! The door is now unlocked.");
                isOpen = true; // Set isOpen to true immediately
                animator.SetBool("isOpen", isOpen);
            }
        }
        else
        {
            Debug.Log("Wrong button! Resetting the puzzle.");
            currentButtonIndex = 0;
            ResetButtons();
        }
    }

    private void ResetButtons()
    {
        foreach (var button in buttonSequence)
        {
            button.ResetButton(); // Reset each button to unpressed state
        }
    }
}
