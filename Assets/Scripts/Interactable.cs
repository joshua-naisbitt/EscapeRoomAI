using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item Item;
    public string promptMessage; //message that displays when interacted
    public string objectName; //Name of object
    public bool isPickupable; //can be put into inventory
    public bool isInPocket; //is in your inventory

    public bool isPuzzleElement; //does this belong to a puzzle?
    public int puzzleCode; //What puzzle does this belong to?
   

    void Start() //init function
    {
        promptMessage = "Default Message for Object";
        objectName = "Default Object";
        isPickupable = false;
        isPuzzleElement = false;
        puzzleCode = 0;
        isInPocket = false;

    }

    public void BaseInteract(){
        Interact();
    }

    protected virtual void Interact()
    {

        if ( !isInPocket && isPickupable) // add to inventory
        {
            Pickup();
        }
        else if (isPuzzleElement) //Trigger puzzle
        {

        }
        else //Look at thing
        {
            Inspect();
        }
    }


    public void Inspect()
    {
        UnityEngine.Debug.Log("Inspecting " + objectName);
        // Trigger inspection screen logic here
    }



    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        
    }

    public void SolvePuzzle()
    {
        //update game master

    }
}
