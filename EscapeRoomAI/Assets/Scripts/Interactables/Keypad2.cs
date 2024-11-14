using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Events;

public class Keypad2 : MonoBehaviour
{
    public string password = "0000";
    private string userInput = "";


    public AudioClip clickSound;
    public AudioClip openSound;
    public AudioClip accessDeniedSound;
    AudioSource audioSource;


    public UnityEvent OnEntryAllowed;

    private void Start()
    {
        userInput = "";
        audioSource = GetComponent<AudioSource>();
    }



    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        userInput += number;
        if (userInput.Length >= 4)
        {
            //check password
            if (userInput == password)
            {
                //TODO Invoke the event play a sound
                UnityEngine.Debug.Log("Entry Allowed");
                userInput = "";
                audioSource.PlayOneShot(openSound);
                OnEntryAllowed.Invoke();
            }
            else
            {
                UnityEngine.Debug.Log("Entry NOT Allowed");
                //TODO play sound
                userInput = "";
                audioSource.PlayOneShot(accessDeniedSound);
            }

        }


    }
}