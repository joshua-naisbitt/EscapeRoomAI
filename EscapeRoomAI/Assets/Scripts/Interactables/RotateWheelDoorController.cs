using System.Collections.Generic;
using UnityEngine;

public class RotateWheelDoorController : Interactable
{
    private Animator animator;
    private bool isOpen = false;
    private bool isUnlocked = false;

    [Tooltip("The target combination for each wheel")]
    public int[] targetCombination = new int[3]; // Correct number for each of the three wheels

    private Dictionary<string, int> currentWheelNumbers = new Dictionary<string, int>(); // Stores each wheel's current number by name

    // Add AudioClip and AudioSource for shared sound effects
    public AudioClip rotateSound;
    public AudioClip unlockSound;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure the AudioSource is attached to this GameObject
        promptMessage = "Solve the rotate wheel puzzle to open the door";

        // Subscribe to the rotate wheel puzzle event
        Rotate.Rotated += OnWheelRotated;
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
            Debug.Log("The door is locked. Solve the rotate wheel puzzle to unlock it.");
        }
    }

    private void OnWheelRotated(string wheelName, int numberShown)
    {
        // Play rotate sound whenever a wheel is rotated
        if (audioSource != null && rotateSound != null)
        {
            audioSource.PlayOneShot(rotateSound);
        }

        // Update the current number for this specific wheel
        currentWheelNumbers[wheelName] = numberShown;

        // Check if all wheels are set to the target combination
        CheckUnlockCondition();
    }

    private void CheckUnlockCondition()
    {
        // Assume the combination is correct until proven otherwise
        bool allCorrect = true;

        // Check each wheel's current number against the target combination
        string[] wheelNames = { "padlockwheel", "padlockwheel (1)", "padlockwheel (2)" };

        for (int i = 0; i < targetCombination.Length; i++)
        {
            string wheelName = wheelNames[i];
            if (!currentWheelNumbers.ContainsKey(wheelName) || currentWheelNumbers[wheelName] != targetCombination[i])
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            isUnlocked = true;
            isOpen = true;
            animator.SetBool("isOpen", isOpen);

            // Play unlock sound when the puzzle is solved
            if (audioSource != null && unlockSound != null)
            {
                audioSource.PlayOneShot(unlockSound);
            }

            Debug.Log("All wheels are in the correct position! The door is now unlocked.");
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= OnWheelRotated; // Unsubscribe from the event when this object is destroyed
    }
}
