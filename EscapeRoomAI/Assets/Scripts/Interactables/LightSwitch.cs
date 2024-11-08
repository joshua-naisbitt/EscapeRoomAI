using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
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
