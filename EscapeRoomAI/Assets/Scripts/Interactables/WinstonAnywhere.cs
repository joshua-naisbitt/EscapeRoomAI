/*
started by Josh
Written by Keoki
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinstonAnywhere : Interactable, Dialoguer
{
    private PromptDB promptdb;
    private GameObject player;
    public Sprite dialogueIcon;
    private InputManager playerIM;
    private GameObject chatObject;
   // private GameObject gameMasterObject;
    private LLMHandler llm;
  //  private GameMaster gm;
    private InputScript inputScript;
    private GameObject inputField;

    private GameObject canvas;
    private string resp;

    void Start()
    {
        promptdb = new PromptDB();
        resp = "default";
        player = GameObject.FindGameObjectWithTag("Player");
        playerIM = player?.GetComponent<InputManager>();

        canvas = GameObject.Find("Canvas");
        if (canvas == null) Debug.Log("Canvas is null");
        inputField = canvas.transform.Find("MyInputField")?.gameObject;
        if (inputField == null) Debug.Log("InputField is null");

        inputScript = inputField?.GetComponent<InputScript>();
        if (inputScript == null) Debug.Log("InputScript is null");

        chatObject = GameObject.FindGameObjectWithTag("LLMObj");
        llm = chatObject?.GetComponent<LLMHandler>();

     //   gameMasterObject = GameObject.FindGameObjectWithTag("GameMasterObj");
      //  gm = gameMasterObject?.GetComponent<GameMaster>();
    }

    protected override void Interact()
    {
        if (playerIM.playerCanMove)
        {
            playerIM.playerCanMove = false;
            inputScript.ShowInputField();
            Dialogue.OpenDialogue(this);
        }
    }

    public async Task<List<DialogueItem>> getDialogue()
{
    string gameContext = llm.prompt;// gm?.GenerateGameContext() ?? "Default game context";
    //Debug.Log(gameContext);
    // Retrieve initial message from Winston
    string initialMessage = await (llm?.SendMessageToWinston($"Give a greeting to the person, they will ask a quastion after this")
                                  ?? Task.FromResult("Error retrieving message from LLM."));

    // Prepare the initial dialogue items
    List<DialogueItem> dialogueItems = new List<DialogueItem>()
    {
        new DialogueItem() { name = "Prof. Winston", picture = dialogueIcon },
        new DialogueItem() { text = initialMessage }, // Winston's initial message
    };

    // Add an action to wait for player input and process it
    dialogueItems.Add(new DialogueItem()
    {
        action = async () =>
        {
            // Wait for input from the player
            string playerInput = await WaitForPlayerInput();
            Debug.Log("Input from player: " + playerInput);

            // Process player input with LLM
            string responseMessage = await (llm?.SendMessageToWinston(
                $@"{promptdb.prompts[0]}
{promptdb.prompts[1]}
Respond to this: {playerInput}")
                ?? Task.FromResult("Error retrieving response from LLM."));

            Debug.Log("Input from Winston: " + responseMessage);

            // Add the response dynamically to the dialogue
            Dialogue.OpenDialogue(new DynamicDialoguer(responseMessage));
        }
    });

    // Add a final action to end the dialogue
    dialogueItems.Add(new DialogueItem()
    {
        action = () =>
        {
            playerIM.playerCanMove = true;
            inputScript.HideInputField();
        }
    });

    return dialogueItems;
}

private class DynamicDialoguer : Dialoguer
{
    private readonly string response;

    public DynamicDialoguer(string response)
    {
        this.response = response;
    }

    public async Task<List<DialogueItem>> getDialogue()
    {
        return new List<DialogueItem>()
        {
            new DialogueItem() { name = "Prof. Winston", picture = null },
            new DialogueItem() { text = response }
        };
    }
}

// Wait for the player to provide input and return the text
private async Task<string> WaitForPlayerInput()
{
    string playerInput = string.Empty;
    while (string.IsNullOrEmpty(playerInput))
    {
        playerInput = inputScript.GetInputText();
        await Task.Yield();
    }
    return playerInput;
}

}