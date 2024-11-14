
/*
Josh, Wrote entire class and dialogue system
Keoki, made the dialogue work with async and the LLM
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private List<DialogueItem> dialogue = null;
    private int diaStep = -1;
    private bool canAdvanceDialogue = false;
    public static bool dialogueIsOpen = false;
    public KeyCode interactKey = KeyCode.E;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI mainText;
    private TextMeshProUGUI continueText;
    private Image characterImage;

    public float characterAdvanceTime = 0.04f;
    private string fullText;
    private float advanceTime = 0;
    private int charactersShown = 0;
    private bool lastDialogue;

    void Awake()
    {
        // Attempt to find the UI components and log errors if they are missing
        nameText = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        mainText = transform.Find("Text")?.GetComponent<TextMeshProUGUI>();
        continueText = transform.Find("ContinueText")?.GetComponent<TextMeshProUGUI>();
        characterImage = transform.Find("Image")?.GetComponent<Image>();

        if (nameText == null) Debug.LogError("Dialogue UI is missing the 'Name' TextMeshProUGUI component.");
        if (mainText == null) Debug.LogError("Dialogue UI is missing the 'Text' TextMeshProUGUI component.");
        if (continueText == null) Debug.LogError("Dialogue UI is missing the 'ContinueText' TextMeshProUGUI component.");
        if (characterImage == null) Debug.LogError("Dialogue UI is missing the 'Image' component.");
    }

    void Update()
    {
        if (canAdvanceDialogue && Input.GetKeyDown(interactKey))
        {
            bool skippedText = SkipText();
            if (!skippedText)
                AdvanceDialogue();
        }
        AdvanceText();
    }

    public static async void OpenDialogue(Dialoguer target)
    {
        var dialogueInstance = FindObjectOfType<Dialogue>(true);
        dialogueInstance.gameObject.SetActive(true);
        await dialogueInstance.StartDia(target);
    }

    private async Task StartDia(Dialoguer target)
    {
        dialogueIsOpen = true;
        dialogue = await target.getDialogue();
        diaStep = -1;
        AdvanceDialogue();
    }

    private void AdvanceDialogue()
    {
        if (dialogue == null) return;

        canAdvanceDialogue = false;
        diaStep++;
        if (diaStep >= dialogue.Count)
        {
            FinishDia();
            return;
        }

        DialogueItem item = dialogue[diaStep];

        if (nameText != null && item.name != null)
            nameText.text = item.name;
        if (characterImage != null && item.picture != null)
            characterImage.sprite = item.picture;
        item.action?.Invoke();

        if (item.text == null)
        {
            fullText = "";
            AdvanceDialogue();
            return;
        }

        lastDialogue = true;
        for (int i = diaStep + 1; i < dialogue.Count; i++)
            if (dialogue[i].text != null)
            {
                lastDialogue = false;
                break;
            }
        SetText(item.text);
    }

    private void SetText(string full)
    {
        fullText = full;
        advanceTime = 0;
        charactersShown = 0;
        canAdvanceDialogue = true;
        AdvanceText();
    }

    private bool SkipText()
    {
        if (string.IsNullOrEmpty(fullText)) return false;

        if (charactersShown < fullText.Length)
        {
            charactersShown = fullText.Length;
            return true;
        }
        return false;
    }

    private void AdvanceText()
    {
        // Ensure mainText and continueText are not null
        if (mainText == null || continueText == null || string.IsNullOrEmpty(fullText)) return;

        advanceTime += Time.deltaTime;
        charactersShown = Mathf.Clamp((int)(advanceTime / characterAdvanceTime), 0, fullText.Length);

        if (charactersShown >= fullText.Length)
        {
            mainText.text = fullText;
            continueText.text = lastDialogue ? "[E] done" : "[E] continue...";
        }
        else
        {
            mainText.text = fullText.Substring(0, charactersShown);
            continueText.text = "[E]";
        }
    }

    private void FinishDia()
    {
        dialogueIsOpen = false;
        gameObject.SetActive(false);

        dialogue = null;
        diaStep = -1;
        canAdvanceDialogue = false;
    }
}

public interface Dialoguer
{
    Task<List<DialogueItem>> getDialogue();
}

public class DialogueItem
{
    public string name;
    public string text;
    public Action action;
    public Sprite picture;
}