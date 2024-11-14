using System;
using System.Collections;
using UnityEngine;

public class Rotate2 : Interactable
{
    public static event Action<string, int> Rotated = delegate { };

    private bool coroutineAllowed;
    private int numberShown;

    private void Start()
    {
        coroutineAllowed = true;
        numberShown = 0; // Start each wheel with 0 facing forward
    }

    protected override void Interact()
    {
        if (coroutineAllowed)
        {
            //audioSource.PlayOneShot(clickSound); // Play click sound on interaction
            StartCoroutine(RotateWheel()); // Start the rotation coroutine
        }
    }

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;

        // Rotate smoothly by -36 degrees over 12 steps to simulate rotation to the next number
        for (int i = 0; i < 12; i++)
        {
            transform.Rotate(0f, 0f, -3f); // Rotate by -3 degrees each step for smoothness
            yield return new WaitForSeconds(0.01f); // Small delay for smooth rotation
        }

        coroutineAllowed = true;

        // Update the number shown on the dial
        numberShown += 1;
        if (numberShown > 9)
        {
            numberShown = 0; // Reset to 0 if it goes above 9
        }

        // Trigger the event with the current wheel's name and number
        Rotated(name, numberShown);
    }
}
