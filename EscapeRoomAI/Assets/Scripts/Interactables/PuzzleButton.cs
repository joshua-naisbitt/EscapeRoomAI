using UnityEngine;

public class PuzzleButton : Interactable
{
    public ButtonPuzzleDoorController doorController;
    public bool isPressed = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component on the button
    }

    protected override void Interact()
    {
        if (!isPressed)
        {
            isPressed = true;
            animator.SetBool("isPressed", true); // Trigger the pressed animation
            doorController.VerifyButtonOrder(this);
        }
    }

    public void ResetButton()
    {
        isPressed = false;
        animator.SetBool("isPressed", false); // Reset the button animation to the released state
    }
}
