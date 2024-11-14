using System.Collections;
using UnityEngine;

public class ButtonScript1 : Interactable
{
    public int keypadNumber = 1;
    public Keypad keypad;


    protected override void Interact()
    {
        // Trigger keypad interaction
        keypad.ButtonClicked(keypadNumber.ToString());
    }
}
