/*
Class written by Nick
Minor edits by Keoki

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextPopupController : MonoBehaviour
{

    public GameObject textPopupPrefab;

    private GameObject instantiatedTextBox;

    private TextMeshProUGUI textMeshPro;


    // Start is called before the first frame update
    void Start()
    {
        if (textPopupPrefab != null)
        {
            instantiatedTextBox = Instantiate(textPopupPrefab, transform);

            instantiatedTextBox.SetActive(false);
            textMeshPro = instantiatedTextBox.GetComponentInChildren<TextMeshProUGUI>();



        }// end if statement

    } // end start()

    public void ShowTextBox(string message)
    {
        if (instantiatedTextBox == null)
        {
            instantiatedTextBox = Instantiate(textPopupPrefab, transform);

            textMeshPro = instantiatedTextBox.GetComponentInChildren<TextMeshProUGUI>();



        } //end if statement

        textMeshPro.text = message; // Sets the text content
        instantiatedTextBox.SetActive(true);

    }// end ShowTextBox()

    public void HideTextBox()
    {
        if (instantiatedTextBox != null)
        {
            instantiatedTextBox.SetActive(false);
        }// end if statement

    }// end HideTextBox()


    public void ToggleTextBox()
    {
        if (instantiatedTextBox == null)
        {
            instantiatedTextBox = Instantiate(textPopupPrefab, transform);
            textMeshPro = instantiatedTextBox.GetComponentInChildren<TextMeshProUGUI>();

        }//end if statement

        instantiatedTextBox.SetActive(!instantiatedTextBox.activeSelf);

    }//end ToggleTextBox()
}
