using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
HOW TO MAKE A NEW INTERACTABLE OBJECT

Step 1: 
Make a duplicate script of any object from the "Interactables" folder (such as this example "SampleInteractable" script). 
Rename it to an appropriate name for the object you are trying to create.

Step 2:
Open your new script and rename public class NameOfYourObjectHere : Interactable to an appropriate name. 
Define the desired behavior of the interactable inside the protected override void Interact()method.

Step 3:
Save your Interactable Object's script and apply it to the object(s) or prefab(s) you want to use your interactable behavior (or create a new object if it does not exist). 
This can be done with the "Add Component" button in the inspector when the target object is selected.

Step 4: 
In the Unity inspector, set your new interactable object's "Layer" property to "Interactable." 
This option is in the top right of the inspector.

Step 5: 
Configure the properties of the interactable object inside the unity inspector.

After you follow these steps, you will have a functioning interactable and you may delete this block comment. 
*/
public class SampleInteractable : Interactable
{
    private GameObject player; // Reference to the player object
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        Debug.Log("Interacted with: "+ gameObject.name);
    }
}
