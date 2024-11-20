/*
Written by Thrinh
Keoki added in Dialoguer function 
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DoorController : Interactable, Dialoguer
{
    private Animator animator;
    private bool isOpen = false;
    private bool isUnlocked = true; // Door is unlocked by default

    private GameObject player;
    public Sprite dialogueIcon;
    private InputManager playerIM;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerIM = player?.GetComponent<InputManager>();

        animator = GetComponent<Animator>();
        promptMessage = "Press to open/close the door";
    }

    protected override void Interact()
    {
        if (isUnlocked)
        {
            isOpen = !isOpen;
            animator.SetBool("isOpen", isOpen);
            Debug.Log(isOpen ? "The door is now open." : "The door is now closed.");
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
}
