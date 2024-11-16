using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Interactable
{
    [SerializeField] private string keyName; // Unique identifier for the key
    void Start()
    {
        
        promptMessage = "The Key";
    }

    protected override void Interact()
    {
        // Add the key to the player's inventory
        PlayerInventory.Instance.AddKey(keyName);
        Debug.Log($"Picked up the key: {keyName}");

        // Destroy the key object after it's picked up
        Destroy(gameObject);
    }
}
