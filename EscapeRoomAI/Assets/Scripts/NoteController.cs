using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NoteInteractable : Interactable
{
    [Header("Input Settings")]
    [SerializeField] private KeyCode closeKey = KeyCode.Escape; // Key to close the note

    [Header("UI Settings")]
    [SerializeField] private GameObject noteCanvas; // Canvas that displays the note
    [SerializeField] private TMP_Text noteTextArea; // Text component to display note content

    [TextArea]
    [SerializeField] private string noteText; // Text content of the note

    [Header("Player Settings")]
    [SerializeField] private MonoBehaviour[] playerScriptsToDisable; // Scripts to disable when note is open (e.g., movement, interaction)

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource; // AudioSource component
    [SerializeField] private AudioClip openNoteSound; // Sound to play when opening the note
    [SerializeField] private AudioClip closeNoteSound; // Sound to play when closing the note

    [Header("Events")]
    public UnityEvent onNoteOpen; // Actions triggered when the note is opened
    public UnityEvent onNoteClose; // Actions triggered when the note is closed

    private bool isNoteOpen = false;

    void Start()
    {
        // Set the prompt message from the Interactable base class
        promptMessage = "Click to read the note";
    }

    protected override void Interact()
    {
        if (!isNoteOpen)
        {
            ShowNote();
        }
    }

    private void ShowNote()
    {
        // Play open note sound
        if (audioSource != null && openNoteSound != null)
        {
            audioSource.PlayOneShot(openNoteSound);
        }

        // Display the note text in the UI
        if (noteTextArea != null)
            noteTextArea.text = noteText;

        // Enable the note canvas
        if (noteCanvas != null)
            noteCanvas.SetActive(true);

        // Disable player controls
        SetPlayerScriptsEnabled(false);

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Invoke the open event
        onNoteOpen?.Invoke();

        // Mark the note as open
        isNoteOpen = true;
    }

    private void CloseNote()
    {
        // Play close note sound
        if (audioSource != null && closeNoteSound != null)
        {
            audioSource.PlayOneShot(closeNoteSound);
        }

        // Disable the note canvas
        if (noteCanvas != null)
            noteCanvas.SetActive(false);

        // Re-enable player controls
        SetPlayerScriptsEnabled(true);

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Invoke the close event
        onNoteClose?.Invoke();

        // Mark the note as closed
        isNoteOpen = false;
    }

    private void SetPlayerScriptsEnabled(bool enabled)
    {
        foreach (MonoBehaviour script in playerScriptsToDisable)
        {
            if (script != null)
                script.enabled = enabled;
        }
    }

    private void Update()
    {
        // Check if the note is open and the close key is pressed
        if (isNoteOpen && Input.GetKeyDown(closeKey))
        {
            CloseNote();
        }
    }
}
