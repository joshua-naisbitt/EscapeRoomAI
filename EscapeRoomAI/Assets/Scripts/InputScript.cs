using UnityEngine;
using TMPro;

public class InputScript : MonoBehaviour
{
    public GameObject inputFieldContainer; // The parent GameObject holding the InputField
    private TMP_InputField inputField; // Reference to the TMP InputField component

    private string finalizedText; // Stores finalized input text

    void Start()
    {
        if (inputFieldContainer != null)
        {
            // Ensure the inputFieldContainer is hidden at the start
            inputFieldContainer.SetActive(false);

            // Get the TMP_InputField component from the child
            inputField = inputFieldContainer.GetComponentInChildren<TMP_InputField>();
            if (inputField == null)
            {
                Debug.LogError("InputScript: No TMP_InputField found in the children of inputFieldContainer.");
            }
            else
            {
                // Register the OnEndEdit callback
                inputField.onEndEdit.AddListener(OnInputSubmit);
            }
        }
        else
        {
            Debug.LogError("InputScript: inputFieldContainer is not assigned.");
        }
    }

    public void ShowInputField()
    {
        if (inputFieldContainer != null)
        {
            inputFieldContainer.SetActive(true);

            // Optional: Clear the previous text and focus on the InputField
            if (inputField != null)
            {
                inputField.text = string.Empty;
                finalizedText = null; // Reset finalized text
                inputField.ActivateInputField();
            }
        }
    }

    public void HideInputField()
    {
        if (inputFieldContainer != null)
        {
            inputFieldContainer.SetActive(false);
        }
    }

    /// Gets the finalized input text from the InputField.
    public string GetInputText()
    {
        return finalizedText; // Return the finalized input
    }

    /// Called when the InputField is submitted (e.g., Enter key pressed).
    private void OnInputSubmit(string input)
    {
        finalizedText = input; // Capture the input text
      //  Debug.Log("Finalized Input: " + finalizedText);
    }
}
