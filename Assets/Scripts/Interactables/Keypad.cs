using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Keypad : Interactable
{
    
    //bool isInPocket;
    // Start is called before the first frame update
    void Start()
    {
        objectName = "Keypad 1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    { 
        if (isPickupable == true) { //& isInPocket = false){
        Debug.Log("Interacted with: "+ objectName);
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        }
        else{

        }
    }
}
