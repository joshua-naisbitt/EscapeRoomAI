using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
   // new objectName = "keypad Placeholder";
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
        Debug.Log("Interacted with: "+ objectName);
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
}
