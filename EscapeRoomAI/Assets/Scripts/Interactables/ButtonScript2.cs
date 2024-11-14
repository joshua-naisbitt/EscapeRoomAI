using System.Collections;
using UnityEngine;

public class ButtonScript2 : Interactable
{
    public int keypadNumber = 1;
    public Keypad2 keypad;


    protected override void Interact()
    {
        // Trigger keypad interaction
        keypad.ButtonClicked(keypadNumber.ToString());
    }
}
