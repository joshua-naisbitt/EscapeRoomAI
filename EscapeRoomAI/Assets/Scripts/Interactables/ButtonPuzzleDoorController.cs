/*
Written by Thrinh
Keoki added in Dialoguer function 
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ButtonPuzzleDoorController : Interactable, Dialoguer
{
    private Animator animator;
    private bool isOpen = false;
    private bool isUnlocked = false;

    public PuzzleButton[] buttonSequence; // Array to hold the buttons in the correct order
    private int currentButtonIndex = 0;   // Tracks which button in the sequence should be pressed next

    
    private GameObject player;
    public Sprite dialogueIcon;
    private InputManager playerIM;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerIM = player?.GetComponent<InputManager>();
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
            if (playerIM.playerCanMove)
            {
                playerIM.playerCanMove = false;
                Dialogue.OpenDialogue(this);
            }
          //  Debug.Log("The door is locked. Solve the button sequence to unlock it.");
        }
    }

     public async Task<List<DialogueItem>> getDialogue()
    {

        return new List<DialogueItem>()
        {
            new DialogueItem() { name = "Winston", picture = dialogueIcon },
            new DialogueItem() { text = promptMessage },
            new DialogueItem() { action = () => { playerIM.playerCanMove = true; } }
        };
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
                isOpen = true; // Set isOpen to true immediately
                animator.SetBool("isOpen", isOpen);
                promptMessage = "Button sequence solved! The door is now unlocked.";
            if (playerIM.playerCanMove)
            {
                playerIM.playerCanMove = false;
                Dialogue.OpenDialogue(this);
            }
            }
        }
        else
        {
           // Debug.Log("Wrong button! Resetting the puzzle.");
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
